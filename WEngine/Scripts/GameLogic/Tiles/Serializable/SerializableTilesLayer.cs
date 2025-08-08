using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles.Serializable
{
    internal class SerializableTilesLayer
    {
        public int Id { get; set; }

        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public float Scale { get; set; }

        public int[,] Tiles { get; set; }

    }
}
