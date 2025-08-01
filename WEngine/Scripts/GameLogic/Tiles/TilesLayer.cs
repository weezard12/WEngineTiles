using Nez;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public int TileWidth { get; private set; } = 16;
        public int TileHeight { get; private set; } = 16;


        public readonly float _scale = 4f;

        private readonly int[,] _tiles;

        // TODO make it so it updates as the layer is modified, not every time the layer is rendered. (by creating a custom method for them when the properties are changed)
        public float TotalWidth => SizeX * TileWidth * _scale;
        public float TotalHeight => SizeX * TileWidth * _scale;


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
        public int GetTile(Point point)
        {
            return GetTile(point.X, point.Y);
        }

        // This method should be called only from the World so it can update the necessary stuff.
        public void SetTile(int x, int y, int value)
        {
            _tiles[y, x] = value;
        }
        public void SetTile(Point point, int value)
        {
            SetTile(point.X, point.Y, value);
        }
    }
}
