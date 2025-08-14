using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.KeyCombos
{
    /// <summary>
    /// Represents a key combination with optional timing constraints
    /// </summary>
    public class KeyCombo
    {
        public List<Keys> Keys { get; set; }
        public float MaxTimeBetweenKeys { get; set; } = 1.0f; // seconds
        public bool RequireExactOrder { get; set; } = true;
        public string Name { get; set; }
        public object Tag { get; set; } // Optional tag for additional data
        
        /// <summary>
        /// Fired when this specific combo is activated.
        /// Subscribe to handle activation without going through the manager-wide event.
        /// </summary>
        public event EventHandler<KeyComboActivatedEventArgs> Activated;

        public KeyCombo(string name, params Keys[] keys)
        {
            Name = name;
            Keys = keys.ToList();
        }

        public KeyCombo(string name, float maxTime, params Keys[] keys)
        {
            Name = name;
            Keys = keys.ToList();
            MaxTimeBetweenKeys = maxTime;
        }

        /// <summary>
        /// Helper to allow the manager to raise the per-combo activation event.
        /// </summary>
        internal void InvokeActivated(object sender, KeyComboActivatedEventArgs args)
        {
            Activated?.Invoke(sender, args);
        }
    }

}
