using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.KeyCombos
{
    /// <summary>
    /// Manages key combinations and triggers events when they are activated
    /// </summary>
    public class KeyComboManager : Component, IUpdatable
    {
        public event EventHandler<KeyComboActivatedEventArgs> ComboActivated;

        private readonly List<KeyCombo> _combos = new List<KeyCombo>();
        private readonly List<ComboProgress> _activeProgresses = new List<ComboProgress>();
        private float _currentTime;

        public bool Enabled { get; set; } = true;
        public bool LogComboAttempts { get; set; } = false; // For debugging

        // Performance caches
        private readonly Dictionary<Keys, List<KeyCombo>> _combosByFirstKey = new Dictionary<Keys, List<KeyCombo>>();
        private readonly HashSet<Keys> _relevantKeysCache = new HashSet<Keys>();

        public override void OnAddedToEntity()
        {
            // No initialization needed when using Nez Input
        }

        public void Update()
        {
            if (!Enabled) return;

            _currentTime = Time.TotalTime;

            // Get newly pressed keys
            var newlyPressedKeys = GetNewlyPressedKeys();

            // Process each newly pressed key
            foreach (var key in newlyPressedKeys)
            {
                ProcessKeyPress(key);
            }

            // Clean up expired combo attempts
            CleanupExpiredProgresses();
        }

        /// <summary>
        /// Adds a key combination to monitor
        /// </summary>
        public void AddCombo(KeyCombo combo)
        {
            if (combo == null) throw new ArgumentNullException(nameof(combo));
            if (combo.Keys == null || combo.Keys.Count == 0)
                throw new ArgumentException("Combo must have at least one key");

            _combos.Add(combo);
            IndexCombo(combo);
        }

        /// <summary>
        /// Adds a key combination to monitor and attaches an action to run when it activates.
        /// </summary>
        public void AddCombo(KeyCombo combo, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            combo.Activated += (s, e) => action();
            AddCombo(combo);
        }

        /// <summary>
        /// Adds a key combination to monitor and attaches a handler to run when it activates.
        /// </summary>
        public void AddCombo(KeyCombo combo, EventHandler<KeyComboActivatedEventArgs> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            combo.Activated += handler;
            AddCombo(combo);
        }

        /// <summary>
        /// Convenience overload to create and add a combo by name.
        /// </summary>
        public KeyCombo AddCombo(string name, params Keys[] keys)
        {
            var combo = new KeyCombo(name, keys);
            AddCombo(combo);
            return combo;
        }

        /// <summary>
        /// Convenience overload to create and add a combo by name with max time between keys.
        /// </summary>
        public KeyCombo AddCombo(string name, float maxTimeBetweenKeys, params Keys[] keys)
        {
            var combo = new KeyCombo(name, maxTimeBetweenKeys, keys);
            AddCombo(combo);
            return combo;
        }

        /// <summary>
        /// Convenience overload to create and add a combo by name and attach an action to run when it activates.
        /// </summary>
        public KeyCombo AddCombo(string name, Action action, params Keys[] keys)
        {
            var combo = new KeyCombo(name, keys);
            AddCombo(combo, action);
            return combo;
        }

        /// <summary>
        /// Convenience overload to create and add a combo by name with max time and attach an action to run when it activates.
        /// </summary>
        public KeyCombo AddCombo(string name, float maxTimeBetweenKeys, Action action, params Keys[] keys)
        {
            var combo = new KeyCombo(name, maxTimeBetweenKeys, keys);
            AddCombo(combo, action);
            return combo;
        }

        /// <summary>
        /// Removes a key combination by name
        /// </summary>
        public bool RemoveCombo(string name)
        {
            var combo = _combos.FirstOrDefault(c => c.Name == name);
            if (combo != null)
            {
                _combos.Remove(combo);
                // Also remove any active progress for this combo
                _activeProgresses.RemoveAll(p => p.Combo == combo);
                DeindexCombo(combo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to subscribe an action to a combo by name.
        /// </summary>
        public bool TrySubscribe(string name, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            var combo = _combos.FirstOrDefault(c => c.Name == name);
            if (combo == null) return false;
            combo.Activated += (s, e) => action();
            return true;
        }

        /// <summary>
        /// Tries to subscribe a handler to a combo by name.
        /// </summary>
        public bool TrySubscribe(string name, EventHandler<KeyComboActivatedEventArgs> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            var combo = _combos.FirstOrDefault(c => c.Name == name);
            if (combo == null) return false;
            combo.Activated += handler;
            return true;
        }

        /// <summary>
        /// Removes all combos
        /// </summary>
        public void ClearCombos()
        {
            _combos.Clear();
            _activeProgresses.Clear();
            _combosByFirstKey.Clear();
            _relevantKeysCache.Clear();
        }

        /// <summary>
        /// Gets all registered combo names
        /// </summary>
        public IEnumerable<string> GetComboNames()
        {
            return _combos.Select(c => c.Name);
        }

        /// <summary>
        /// Checks if a combo with the given name exists
        /// </summary>
        public bool HasCombo(string name)
        {
            return _combos.Any(c => c.Name == name);
        }

        private List<Keys> GetNewlyPressedKeys()
        {
            var newKeys = new List<Keys>();

            // Check only the keys that matter for our combos (cached)
            foreach (var key in _relevantKeysCache)
            {
                if (Input.IsKeyPressed(key))
                {
                    newKeys.Add(key);
                }
            }

            return newKeys;
        }

        private void ProcessKeyPress(Keys pressedKey)
        {
            if (LogComboAttempts)
            {
                Debug.Log($"Key pressed: {pressedKey}");
            }

            // Track which combos we have already processed to avoid duplicate work
            var processedCombos = new HashSet<KeyCombo>();

            // Check combos that start with this key first
            if (_combosByFirstKey.TryGetValue(pressedKey, out var startingCombos))
            {
                for (int i = 0; i < startingCombos.Count; i++)
                {
                    processedCombos.Add(startingCombos[i]);
                    ProcessComboForKey(startingCombos[i], pressedKey);
                }
            }

            // Then attempt to continue existing progresses that expect or accept this key
            // Iterate over a copy because we may remove progresses
            if (_activeProgresses.Count > 0)
            {
                var snapshot = new List<ComboProgress>(_activeProgresses);
                for (int i = 0; i < snapshot.Count; i++)
                {
                    var progress = snapshot[i];
                    // Skip expired here; cleanup will remove later
                    if (progress.IsExpired(_currentTime)) continue;

                    var combo = progress.Combo;
                    if (processedCombos.Contains(combo))
                        continue;
                    if (combo.RequireExactOrder)
                    {
                        if (progress.CurrentIndex < combo.Keys.Count && combo.Keys[progress.CurrentIndex] == pressedKey)
                        {
                            ProcessComboForKey(combo, pressedKey);
                        }
                    }
                    else
                    {
                        if (progress.RemainingAnyOrderKeys != null && progress.RemainingAnyOrderKeys.Contains(pressedKey))
                        {
                            ProcessComboForKey(combo, pressedKey);
                        }
                    }
                }
            }
        }

        private void ProcessComboForKey(KeyCombo combo, Keys pressedKey)
        {
            // Find existing progress for this combo (linear search kept due to typically small active set)
            var progress = _activeProgresses.FirstOrDefault(p => p.Combo == combo);

            // Prefer continuing an existing, non-expired progress if present
            if (progress != null && !progress.IsExpired(_currentTime))
            {
                if (combo.RequireExactOrder)
                {
                    // Check if it's the next expected key
                    if (progress.CurrentIndex < combo.Keys.Count &&
                        combo.Keys[progress.CurrentIndex] == pressedKey)
                    {
                        progress.CurrentIndex++;
                        progress.LastKeyTime = _currentTime;
                        progress.PressedKeys.Add(pressedKey);

                        if (LogComboAttempts)
                        {
                            Debug.Log($"Continued combo '{combo.Name}': {progress.CurrentIndex}/{combo.Keys.Count}");
                        }

                        // Check if combo is complete
                        if (progress.CurrentIndex >= combo.Keys.Count)
                        {
                            float timeTaken = _currentTime - progress.StartTime;
                            OnComboActivated(combo, timeTaken);
                            _activeProgresses.Remove(progress);
                        }
                    }
                    else
                    {
                        // Wrong key, reset if it could start the combo again
                        if (combo.Keys[0] == pressedKey)
                        {
                            progress.Reset(_currentTime);
                            progress.CurrentIndex = 1;
                            progress.PressedKeys.Add(pressedKey);
                            if (progress.RemainingAnyOrderKeys != null)
                                progress.RemainingAnyOrderKeys.Remove(pressedKey);
                        }
                        else
                        {
                            if (LogComboAttempts)
                            {
                                Debug.Log($"Wrong key for combo '{combo.Name}', expected {combo.Keys[progress.CurrentIndex]}, got {pressedKey}");
                            }
                            _activeProgresses.Remove(progress);
                        }
                    }
                }
                else
                {
                    // Any order allowed - check if key is in remaining keys
                    if (progress.RemainingAnyOrderKeys == null)
                        progress.RemainingAnyOrderKeys = new HashSet<Keys>(combo.Keys.Except(progress.PressedKeys));

                    if (progress.RemainingAnyOrderKeys.Contains(pressedKey))
                    {
                        progress.PressedKeys.Add(pressedKey);
                        progress.LastKeyTime = _currentTime;
                        progress.RemainingAnyOrderKeys.Remove(pressedKey);

                        if (LogComboAttempts)
                        {
                            Debug.Log($"Added key to any-order combo '{combo.Name}': {progress.PressedKeys.Count}/{combo.Keys.Count}");
                        }

                        // Check if combo is complete
                        if (progress.PressedKeys.Count >= combo.Keys.Count)
                        {
                            float timeTaken = _currentTime - progress.StartTime;
                            OnComboActivated(combo, timeTaken);
                            _activeProgresses.Remove(progress);
                        }
                    }
                    else if (combo.Keys[0] == pressedKey)
                    {
                        // Restart any-order combo if first key is hit
                        progress.Reset(_currentTime);
                        progress.CurrentIndex = 1;
                        progress.PressedKeys.Add(pressedKey);
                        if (progress.RemainingAnyOrderKeys != null)
                            progress.RemainingAnyOrderKeys.Remove(pressedKey);
                    }
                }
            }
            // No active progress or expired: check if this key starts the combo
            else if (combo.Keys[0] == pressedKey)
            {
                // Start new progress
                progress = new ComboProgress
                {
                    Combo = combo,
                    CurrentIndex = 1,
                    LastKeyTime = _currentTime,
                    StartTime = _currentTime,
                    RemainingAnyOrderKeys = combo.RequireExactOrder ? null : new HashSet<Keys>(combo.Keys)
                };
                progress.PressedKeys.Add(pressedKey);
                if (progress.RemainingAnyOrderKeys != null)
                    progress.RemainingAnyOrderKeys.Remove(pressedKey);
                _activeProgresses.Add(progress);

                if (LogComboAttempts)
                {
                    Debug.Log($"Started combo '{combo.Name}' with key {pressedKey}");
                }

                // Check if combo is complete (single key combo)
                if (combo.Keys.Count == 1)
                {
                    OnComboActivated(combo, 0f);
                    _activeProgresses.Remove(progress);
                }
            }
        }

        private void CleanupExpiredProgresses()
        {
            for (int i = _activeProgresses.Count - 1; i >= 0; i--)
            {
                var progress = _activeProgresses[i];
                if (progress.IsExpired(_currentTime))
                {
                    if (LogComboAttempts)
                    {
                        Debug.Log($"Combo '{progress.Combo.Name}' expired");
                    }
                    _activeProgresses.RemoveAt(i);
                }
            }
        }

        private void OnComboActivated(KeyCombo combo, float timeTaken)
        {
            if (LogComboAttempts)
            {
                Debug.Log($"Combo '{combo.Name}' activated! Time taken: {timeTaken:F2}s");
            }

            var args = new KeyComboActivatedEventArgs(combo, timeTaken);
            // Raise manager-wide event first
            ComboActivated?.Invoke(this, args);
            // Raise per-combo event
            combo.InvokeActivated(this, args);
        }

        private void IndexCombo(KeyCombo combo)
        {
            if (combo.Keys.Count == 0) return;
            var first = combo.Keys[0];
            if (!_combosByFirstKey.TryGetValue(first, out var list))
            {
                list = new List<KeyCombo>();
                _combosByFirstKey[first] = list;
            }
            list.Add(combo);

            foreach (var key in combo.Keys)
                _relevantKeysCache.Add(key);
        }

        private void DeindexCombo(KeyCombo combo)
        {
            if (combo.Keys.Count == 0) return;
            var first = combo.Keys[0];
            if (_combosByFirstKey.TryGetValue(first, out var list))
            {
                list.Remove(combo);
                if (list.Count == 0)
                    _combosByFirstKey.Remove(first);
            }

            // Rebuild relevant keys cache conservatively when removing
            _relevantKeysCache.Clear();
            for (int i = 0; i < _combos.Count; i++)
            {
                var c = _combos[i];
                foreach (var key in c.Keys)
                    _relevantKeysCache.Add(key);
            }
        }
    }
}
