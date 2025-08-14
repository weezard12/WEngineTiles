using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Project;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.Tiles.Serializable;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorld : Scene
    {
        // Constant Values
        public const int ChunkSize = 8 * 16 * 4; // (512) Each chunk is 8x8 tiles, each tile is 16x16 pixels, and each pixel is 4x4 in the game world.

        // Manages all of the rendering in the game world.
        public RenderingManager RenderingManager { get; private set; }

        List<TilesChunk> Chunks { get; set; } = new List<TilesChunk>();

        // Adds a new empty chunk at the position
        protected void AddChunk(int x, int y)
        {
            TilesChunk chunk = new TilesChunk(x, y);
            Chunks.Add(chunk);

            AddEntity(chunk);
        }
        protected void LoadChunk(SerializableTilesChunk serializableChunk)
        {
            TilesChunk chunk = TilesChunk.FromSerializable(serializableChunk);
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
        public TilesChunk GetChunk(int x, int y)
        {
            return (TilesChunk)FindEntity($"TilesChunk_{x}_{y}");
        }
        public TilesChunk GetChunk(Point coordinates)
        {
            return GetChunk(coordinates.X, coordinates.Y);
        }

        public ReadOnlyCollection<TilesChunk> GetChunks()
        {
            return Chunks.AsReadOnly();
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


        public override void OnStart()
        {
            base.OnStart();

            RenderingManager = new RenderingManager(ProjectManager.CurrentProject.LoadTiles());
            AddEntity(RenderingManager);
        }


        #region World Saving

        public virtual void SaveWorld()
        {
            SerializableTilesWorld serializableTilesWorld = ToSerializable();
            ProjectManager.CurrentProject.SaveWorldJson(serializableTilesWorld);
        }
        public virtual void LoadWorld()
        {
            List<SerializableTilesChunk> sChunks = ProjectManager.CurrentProject.LoadChunks();
            foreach (SerializableTilesChunk chunk in sChunks)
            {
                LoadChunk(chunk);
            }
        }
        #endregion

        #region Making this object Serializable
        public SerializableTilesWorld ToSerializable()
        {
            return ToSerializable(this);
        }
        public static SerializableTilesWorld ToSerializable(TilesWorld world)
        {
            SerializableTilesWorld sWorld = new SerializableTilesWorld();
            foreach (var chunk in world.GetChunks())
            {
                sWorld.Chunks.Add(chunk.ToSerializable());
            }

            return sWorld;
        }
        #endregion
    }
}
