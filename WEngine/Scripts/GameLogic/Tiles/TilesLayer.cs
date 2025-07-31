using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    /// <summary>
    /// This class represents a layer of tiles in the game world.
    /// used to store data about the layer so it can be passed to rendering and collision detection systems.
    /// </summary>
    internal class TilesLayer : Component
    {
        // The layer ID, used to identify the layer and its the smae as the
        public int Id { get; private set; }

        public int SizeX = 8;
        public int SizeY = 8;

        public readonly int _tileWidth = 16;
        public readonly int _tileHeight = 16;

        public readonly float _scale = 4f;

        private readonly int[,] _tiles;

        public TilesLayer()
        {
            _tiles = new int[SizeY, SizeX];
        }

        public int GetTile(int x, int y)
        {
            if (x < 0 || x >= SizeX || y < 0 || y >= SizeY)
                throw new IndexOutOfRangeException("Tile coordinates are out of range.");
            return _tiles[y, x];
        }

        // This method should be called only from the World so it can update the necessary stuff.
        public void SetTile(int x, int y, int value)
        {
            _tiles[y, x] = value;
        }
    }
}
