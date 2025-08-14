using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.KeyCombos
{
    /// <summary>
    /// Event arguments for key combo activation
    /// </summary>
    public class KeyComboActivatedEventArgs : EventArgs
    {
        public KeyCombo Combo { get; }
        public float TimeTaken { get; }

        public KeyComboActivatedEventArgs(KeyCombo combo, float timeTaken)
        {
            Combo = combo;
            TimeTaken = timeTaken;
        }
    }

}
