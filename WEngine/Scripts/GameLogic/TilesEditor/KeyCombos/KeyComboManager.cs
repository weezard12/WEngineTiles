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

        private List<KeyCombo> _combos = new List<KeyCombo>();
        private List<ComboProgress> _activeProgresses = new List<ComboProgress>();
        private float _currentTime;

        public bool Enabled { get; set; } = true;
        public bool LogComboAttempts { get; set; } = false; // For debugging

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

            // Optimized: Only check keys that are actually used in combos
            var relevantKeys = new HashSet<Keys>();
            foreach (var combo in _combos)
            {
                foreach (var key in combo.Keys)
                {
                    relevantKeys.Add(key);
                }
            }

            // Check only the keys that matter for our combos
            foreach (var key in relevantKeys)
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

            // Check each combo for potential matches
            foreach (var combo in _combos)
            {
                ProcessComboForKey(combo, pressedKey);
            }
        }

        private void ProcessComboForKey(KeyCombo combo, Keys pressedKey)
        {
            // Find existing progress for this combo
            var progress = _activeProgresses.FirstOrDefault(p => p.Combo == combo);

            // Check if this key starts the combo
            if (combo.Keys[0] == pressedKey)
            {
                if (progress == null)
                {
                    // Start new progress
                    progress = new ComboProgress
                    {
                        Combo = combo,
                        CurrentIndex = 1,
                        LastKeyTime = _currentTime,
                        StartTime = _currentTime
                    };
                    progress.PressedKeys.Add(pressedKey);
                    _activeProgresses.Add(progress);

                    if (LogComboAttempts)
                    {
                        Debug.Log($"Started combo '{combo.Name}' with key {pressedKey}");
                    }
                }
                else
                {
                    // Reset existing progress
                    progress.Reset(_currentTime);
                    progress.CurrentIndex = 1;
                    progress.PressedKeys.Add(pressedKey);

                    if (LogComboAttempts)
                    {
                        Debug.Log($"Restarted combo '{combo.Name}' with key {pressedKey}");
                    }
                }

                // Check if combo is complete (single key combo)
                if (combo.Keys.Count == 1)
                {
                    OnComboActivated(combo, 0f);
                    _activeProgresses.Remove(progress);
                }
            }
            // Check if this key continues an existing combo
            else if (progress != null && !progress.IsExpired(_currentTime))
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
                    var remainingKeys = combo.Keys.Skip(progress.PressedKeys.Count).ToList();
                    if (remainingKeys.Contains(pressedKey))
                    {
                        progress.PressedKeys.Add(pressedKey);
                        progress.LastKeyTime = _currentTime;

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
    }
}
