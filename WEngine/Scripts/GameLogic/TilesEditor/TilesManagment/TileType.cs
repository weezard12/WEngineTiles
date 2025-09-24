using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.TilesManagment
{
    internal class TileType
    {
        public string Name;
        public Type Type;
        public string Description;
        public TileType(string name, Type type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
