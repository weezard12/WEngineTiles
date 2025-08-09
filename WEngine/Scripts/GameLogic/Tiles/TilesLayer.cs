using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.GameLogic.Tiles.Tilesets;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.Tiles
{
    /// <summary>
    /// This class represents a layer of tiles in the game world.
    /// used to store data about the layer so it can be passed to rendering and collision detection systems.
    /// </summary>
    internal class TilesLayer : Component
    {
        // The layer ID, used to identify the layer, and its the smae as the render layer
        public int Id { get; set; }

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

        #region Get Tiles
        public int GetTile(int x, int y)
        {
            if (x < 0 || x >= SizeX || y < 0 || y >= SizeY)
            {
                Debug.Error($"Tile coordinates [{x}, {y}] are out of range for layer size [{SizeX}, {SizeY}].");

                // If the tile is out of this layer bounds we will try to get if from the adjacent layer
                TilesChunk chunk = (TilesChunk) Entity;
                TilesWorld world = (TilesWorld) Entity.Scene;

                int offsetX = x < 0 ? -1 : (x >= SizeX ? 1 : 0);
                int offsetY = y < 0 ? -1 : (y >= SizeY ? 1 : 0);

                TilesChunk adjacentChunk = world.GetChunk(chunk.idX + offsetX, chunk.idY + offsetY);
                if (adjacentChunk == null)
                    return 0;

                TilesLayer adjacentLayer = adjacentChunk.GetLayer(Id);
                if (adjacentLayer == null)
                    return 0;

                if (!AreLayersWithSameProperties(this, adjacentLayer))
                    return 0;
                
                int wrappedX = (x + SizeX) % SizeX;
                int wrappedY = (y + SizeY) % SizeY;

                Debug.Warn($"Trying to fetch adjacent layer coordinates [{wrappedX}, {wrappedY}] in chunk [{adjacentChunk.idX}, {adjacentChunk.idY}].");

                // Recursive call to safely fetch from wrapped adjacent layer
                return adjacentLayer.GetTile(wrappedX, wrappedY);
            }
                
            return _tiles[y, x];
        }
        public int GetTile(Point point)
        {
            return GetTile(point.X, point.Y);
        }

        private int[,] GetSurroundingTiles(int x, int y)
        {
            int[,] serroundingTiles = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    serroundingTiles[i, j] = GetTile(x + j - 1, y + i - 1);
                }
            }
            return serroundingTiles;
        }
        #endregion

        #region Set Tile
        public void SetTile(int x, int y, int value, bool checkForTileset = true)
        {
            if (checkForTileset)
            {
                Tileset tileset = ((TilesWorld)Entity.Scene).RenderingManager.GetTilesetForTile(value);
                if (tileset != null)
                {
                    // Set the current tile based on the tileset
                    int[,] surroundingTiles = GetSurroundingTiles(x, y);
                    int tilesetValue = tileset.GetTileBasedOnSerroundings(surroundingTiles);
                    SetTile(x, y, tilesetValue, false);
                    
                    // Define relative positions for 8 surrounding tiles
                    (int dx, int dy)[] directions = new (int, int)[]
                    {
                        (-1, -1), // Top-Left
                        ( 0, -1), // Top
                        ( 1, -1), // Top-Right
                        (-1,  0), // Left
                        ( 1,  0), // Right
                        (-1,  1), // Bottom-Left
                        ( 0,  1), // Bottom
                        ( 1,  1)  // Bottom-Right
                    };

                    foreach (var (dx, dy) in directions)
                    {
                        int nx = x + dx;
                        int ny = y + dy;


                        if (tileset.IsTilesetContainsTile(GetTile(nx, ny)))
                        {
                            surroundingTiles = GetSurroundingTiles(nx, ny);
                            tilesetValue = tileset.GetTileBasedOnSerroundings(surroundingTiles);
                            SetTile(nx, ny, tilesetValue, false);
                        }
                        
                    }


                    return;
                }
            }

            if(!InLayerBounds(x, y))
            {
                Debug.Error($"Tile coordinates [{x}, {y}] are out of range for layer size [{SizeX}, {SizeY}].");

                // If the tile is out of this layer bounds we will try to get if from the adjacent layer
                TilesChunk chunk = (TilesChunk)Entity;
                TilesWorld world = (TilesWorld)Entity.Scene;

                int offsetX = x < 0 ? -1 : (x >= SizeX ? 1 : 0);
                int offsetY = y < 0 ? -1 : (y >= SizeY ? 1 : 0);

                TilesChunk adjacentChunk = world.GetChunk(chunk.idX + offsetX, chunk.idY + offsetY);
                if (adjacentChunk == null)
                    return;

                TilesLayer adjacentLayer = adjacentChunk.GetLayer(Id);
                if (adjacentLayer == null)
                    return;

                if (!AreLayersWithSameProperties(this, adjacentLayer))
                    return;

                int wrappedX = (x + SizeX) % SizeX;
                int wrappedY = (y + SizeY) % SizeY;

                Debug.Warn($"Trying to fetch adjacent layer coordinates [{wrappedX}, {wrappedY}] in chunk [{adjacentChunk.idX}, {adjacentChunk.idY}].");

                // Recursive call to safely fetch from wrapped adjacent layer
                adjacentLayer.SetTile(wrappedX, wrappedY, value, checkForTileset);
                return;
            }
            _tiles[y, x] = value;
        }
        public void SetTile(Point point, int value)
        {
            SetTile(point.X, point.Y, value);
        }
        #endregion


        #region Checks
        public static bool AreLayersWithSameProperties(TilesLayer layer1, TilesLayer layer2)
        {
            if(layer1.SizeX != layer2.SizeX)
                return false;
            if(layer1.SizeY != layer2.SizeY)
                return false;
            if(layer1.TileWidth != layer2.TileWidth)
                return false;
            if(layer1.TileHeight != layer2.TileHeight)
                return false;
            if(layer1._scale != layer2._scale)
                return false;

            return true;
        }
        public bool InLayerBounds(int x, int y)
        {
            return x >= 0 && x < SizeX && y >= 0 && y < SizeY;
        }

        #endregion


        #region Making this object Serializable
        public SerializableTilesLayer ToSerializable()
        {
            return ToSerializable(this);
        }
        public static SerializableTilesLayer ToSerializable(TilesLayer layer)
        {
            SerializableTilesLayer sLayer = new SerializableTilesLayer()
            {
                Id = layer.Id,
                SizeX = layer.SizeX,
                SizeY = layer.SizeY,
                TileWidth = layer.TileWidth,
                TileHeight = layer.TileHeight,
                Scale = layer._scale,
                Tiles = layer._tiles,
            };

            return sLayer;
        }

        public static TilesLayer FromSerializable(SerializableTilesLayer sLayer)
        {
            if (sLayer == null)
                throw new ArgumentNullException(nameof(sLayer));

            // Create new TilesLayer
            var layer = new TilesLayer
            {
                Id = sLayer.Id,
                SizeX = sLayer.SizeX,
                SizeY = sLayer.SizeY,
                TileWidth = sLayer.TileWidth,
                TileHeight = sLayer.TileHeight
            };

            // Copy tiles data
            for (int y = 0; y < sLayer.SizeY; y++)
                for (int x = 0; x < sLayer.SizeX; x++)
                    layer._tiles[y, x] = sLayer.Tiles[y, x];
                
            return layer;
        }

        #endregion
    }
}
