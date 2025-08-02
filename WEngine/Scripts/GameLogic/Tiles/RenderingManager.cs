using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class RenderingManager : Entity, IUpdatable
    {
        // Stores all the tiles textures by their ID. 0 is empty tile.
        private readonly Dictionary<int, Sprite> Sprites = new();
        private readonly Dictionary<int, Tile> Tiles = new();

        private readonly Dictionary<int, Tileset> Tilesets = new();

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            AddTexture("Assets/Tiles/Tile");
            AddTexture("Assets/Tiles/Kaftor_Grass");
            AddTexture("Assets/Tiles/Kaftor_Grass2");
            AddTexture("Assets/Tiles/Kaftor_Bush");

            AddTexture("Assets/Tiles/buttom_left");
            AddTexture("Assets/Tiles/buttom_right");
            AddTexture("Assets/Tiles/top_left");
            AddTexture("Assets/Tiles/top_right");
            AddTexture("Assets/Tiles/full");

            // For testing
            AddTile(new Tile { TextureId = 0 });
            AddTile(new Tile { TextureId = 1 });
            AddTile(new Tile { TextureId = 2 });
            AddTile(new Tile { TextureId = 3 });
            AddTile(new Tile { TextureId = 4 });

            AddTile(new AnimatedTile { TextureId = 4 });
        }


        public Sprite GetTexture(int id)
        {
            // First we check if this tile is part of a tileset.
            if (Tilesets.TryGetValue(id, out var tileset))
            {
                // If it is, we get the tile from the tileset.

            }
            // If it is not part of a tileset, we check if it is a regular tile.


            if (Tiles.TryGetValue(id, out var tile))
            {
                tile.TextureId = id;
                if(Sprites.TryGetValue(tile.TextureId, out var sprite))
                    return sprite;
            }

            Debug.Error($"Tile with ID {id} not found.");
            return null;
        }

        #region Adding Tiles
        public void AddTile(Tile tile)
        {
            Tiles.Add(GetNextAvailableTileId(), tile);
        }
        private int GetNextAvailableTileId()
        {
            return GetNextAvailableIdAcrossCollections(new IDictionary<int, object>[]
            {
                Tiles.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value),
                Tilesets.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value)
            }, 0);
        }
        #endregion

        #region Adding Textures
        protected void AddTexture(int id, Texture2D texture)
        {
            if (!Sprites.ContainsKey(id))
            {
                Sprites[id] = new Sprite(texture);
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
                AddTexture(id, Scene.Content.LoadTexture(texturePath));
            }
            catch (Exception ex)
            {
                Debug.Error($"Error loading texture:{texturePath}\n{ex.Message}");
            }
        }


        private int GetNextAvailableTextureId()
        {
            return GetNextAvailableId(Sprites, 0);
        }
        #endregion

        #region Collections utils (for getting free IDs)
        /// <summary>
        /// Gets the next available ID for any collection that uses integer keys
        /// </summary>
        /// <typeparam name="T">The value type of the collection</typeparam>
        /// <param name="collection">The collection to check for available IDs</param>
        /// <param name="startId">The starting ID to begin searching from (default: 0)</param>
        /// <returns>The next available integer ID</returns>
        private int GetNextAvailableId<T>(IDictionary<int, T> collection, int startId = 0)
        {
            int id = startId;
            while (collection.ContainsKey(id))
                id++;
            return id;
        }
        /// <summary>
        /// Gets the next available ID from multiple collections simultaneously
        /// Ensures the returned ID is not used in any of the provided collections
        /// </summary>
        /// <param name="collections">Array of collections to check</param>
        /// <param name="startId">The starting ID to begin searching from (default: 0)</param>
        /// <returns>The next available integer ID across all collections</returns>
        private int GetNextAvailableIdAcrossCollections(IDictionary<int, object>[] collections, int startId = 0)
        {
            int id = startId;
            bool idExists;

            do
            {
                idExists = false;
                foreach (var collection in collections)
                {
                    if (collection.ContainsKey(id))
                    {
                        idExists = true;
                        break;
                    }
                }
                if (idExists) id++;
            } while (idExists);

            return id;
        }
        #endregion
    }
}
