using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.KeyCombos
{
    /// <summary>
    /// Tracks the progress of a key combination being entered
    /// </summary>
    internal class ComboProgress
    {
        public KeyCombo Combo { get; set; }
        public int CurrentIndex { get; set; } = 0;
        public float LastKeyTime { get; set; }
        public float StartTime { get; set; }
        public List<Keys> PressedKeys { get; set; } = new List<Keys>();

        public bool IsExpired(float currentTime)
        {
            return currentTime - LastKeyTime > Combo.MaxTimeBetweenKeys;
        }

        public void Reset(float currentTime)
        {
            CurrentIndex = 0;
            LastKeyTime = currentTime;
            StartTime = currentTime;
            PressedKeys.Clear();
        }
    }
}
