using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.KeyCombos
{
    /// <summary>
    /// Helper class for creating common key combinations
    /// </summary>
    public static class KeyCombos
    {
        public static KeyCombo Konami => new KeyCombo("Konami", 2.0f,
            Keys.Up, Keys.Up, Keys.Down, Keys.Down,
            Keys.Left, Keys.Right, Keys.Left, Keys.Right,
            Keys.B, Keys.A);

        public static KeyCombo CtrlC => new KeyCombo("Copy")
        {
            Keys = { Keys.LeftControl, Keys.C },
            RequireExactOrder = false,
            MaxTimeBetweenKeys = 0.1f
        };

        public static KeyCombo CtrlV => new KeyCombo("Paste")
        {
            Keys = { Keys.LeftControl, Keys.V },
            RequireExactOrder = false,
            MaxTimeBetweenKeys = 0.1f
        };

        public static KeyCombo AltF4 => new KeyCombo("Alt+F4")
        {
            Keys = { Keys.LeftAlt, Keys.F4 },
            RequireExactOrder = false,
            MaxTimeBetweenKeys = 0.1f
        };
    }
}

