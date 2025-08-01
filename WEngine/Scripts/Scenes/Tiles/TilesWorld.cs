using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorld : Scene
    {
        // Constant Values
        public const int ChunkSize = 8 * 16 * 4; // (512) Each chunk is 8x8 tiles, each tile is 16x16 pixels, and each pixel is 4x4 in the game world.

        // Stores all the tiles textures by their ID. 0 is empty tile.
        private readonly Dictionary<int, Sprite> textures = new();

        public Sprite GetTexture(int id)
        {
            if(textures.TryGetValue(id, out var texture))
            {
                return texture;
            }
            Debug.Error($"Texture with ID {id} not found.");
            return null;
        }

        protected void AddTexture(int id, Texture2D texture)
        {
            if(!textures.ContainsKey(id))
            {
                textures[id] = new Sprite(texture);
            }
            else
            {
                Debug.Error($"Texture with ID {id} already exists.");
            }
        }
        protected void AddTexture(string texturePath)
        {
            int id = GetNextAvailableTextureId();
            try
            {
                AddTexture(id, Content.LoadTexture(texturePath));
            }
            catch(Exception ex)
            {
                Debug.Error($"Error loading texture:{texturePath}\n{ex.Message}");
            }
        }

        private int GetNextAvailableTextureId()
        {
            int id = 1;
            while (textures.ContainsKey(id))
                id++;
            return id;
        }


        List<TilesChunk> Chunks { get; set; } = new List<TilesChunk>();

        protected void AddChunk(int x, int y)
        {
            TilesChunk chunk = new TilesChunk(x, y);
            Chunks.Add(chunk);

            AddEntity(chunk);
        }


        // TODO improve this method to be more efficient
        public void GetChunkCoordinates(Vector2 worldPosition, ref Point chunkPos)
        {
            int GetCoord(float value)
            {
                float halfChunk = ChunkSize / 2f;

                if (value >= -halfChunk && value < halfChunk)
                    return 0;

                if (value >= 0)
                    return 1 + (int)((value - halfChunk) / ChunkSize);
                else
                    return -1 + (int)((value + halfChunk) / ChunkSize);
            }

            chunkPos.X = GetCoord(worldPosition.X);
            chunkPos.Y = GetCoord(worldPosition.Y);
        }

        // TODO improve this method to be more efficient (maby add all tiles to a set, this method of finding the entity is not efficient)
        public TilesChunk GetChunk(Point coordinates)
        {
            
            return (TilesChunk) FindEntity($"TilesChunk_{coordinates.X}_{coordinates.Y}");
        }

        public void GetTileCordinates(Vector2 worldPosition, ref Point tilePos)
        {
            Point chunkPos = Point.Zero;
            GetChunkCoordinates(worldPosition, ref chunkPos);
            TilesChunk chunk = GetChunk(chunkPos);
            
            if(chunk == null)
            {
                Debug.Error(String.Format("Chunk at coordinates [{0}, {1}] not found.", chunkPos.X, chunkPos.Y));
                tilePos = new Point(-1, -1);
                return;
            }

            foreach (var layer in chunk.GetLayers())
            {
                var layerEntity = layer.Entity;
                var layerPos = layerEntity.Transform.Position;

                float totalWidth = layer.TotalWidth;
                float totalHeight = layer.TotalWidth;

                // Convert world position to local tile-space relative to top-left
                var topLeft = new Vector2(
                    layerPos.X - totalWidth / 2f,
                    layerPos.Y - totalHeight / 2f
                );

                var localPos = worldPosition - topLeft;

                int tileX = (int)(localPos.X / (layer.TileWidth * layer._scale));
                int tileY = (int)(localPos.Y / (layer.TileHeight * layer._scale));

                if (tileX >= 0 && tileX < layer.SizeX &&
                    tileY >= 0 && tileY < layer.SizeY)
                {
                    tilePos.X = tileX;
                    tilePos.Y = tileY;
                    return;
                }
            }

            // Not found in any layer
            tilePos = new Point(-1, -1);
        }

    }
}
