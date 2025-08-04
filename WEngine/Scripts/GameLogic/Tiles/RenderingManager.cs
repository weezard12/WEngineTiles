using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.Tilesets;
using WEngine.Scripts.Main.Utils;

namespace WEngine.Scripts.GameLogic.Tiles
{
    // TODO mabey move the `Tiles` to the `TilesWorld` class. Since this call needs to contain only rendering stuff.
    internal class RenderingManager : Entity, IUpdatable
    {
        // Stores all the tiles textures by their ID. 0 is empty tile.
        private readonly Dictionary<int, Sprite> Sprites = new();
        private readonly Dictionary<int, Tile> Tiles = new();

        private readonly List<Tileset> Tilesets = new();

        public RenderingManager()
        {
            // Testing tilesets (TODO move this logic to TilesWorld)
            Tileset tileset = new Tileset();

            tileset.SetTiles(new int[3, 3]
            {
                { 7, 9, 8 },
                { 9, 9, 9 },
                { 5, 9, 6 }
            });

            AddTileset(tileset);
        }

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

            // Testing sprite sheet loading.
            Texture2D spriteSheet = Scene.Content.Load<Texture2D>("Assets/Tiles/water sprite sheet");

            // Get all sprites from the sheet
            Sprite[,] allSprites = SpriteSheetUtils.GetSprites(spriteSheet, 16, 16);

            foreach (var sprite in allSprites)
            {
                AddTexture(sprite);
            }

            // For testing
            AddTile(new Tile { TextureId = 0 });
            AddTile(new Tile { TextureId = 1 });
            AddTile(new Tile { TextureId = 2 });
            AddTile(new Tile { TextureId = 3 });

            AddTile(new Tile { TextureId = 4 });
            AddTile(new Tile { TextureId = 5 });
            AddTile(new Tile { TextureId = 6 });
            AddTile(new Tile { TextureId = 7 });
            AddTile(new Tile { TextureId = 8 });

            AnimatedTile animatedWaterTile = new AnimatedTile { TextureId = 2, Frames = {11, 12, 13}, FrameRate = 4 };

            AddTile(animatedWaterTile);


        }


        public Sprite GetTexture(int id)
        {
            // First we check if this tile is part of a tileset.
/*            if (Tilesets.TryGetValue(id, out var tileset))
            {
                // If it is, we get the tile from the tileset.
                
*//*                if (Tiles.TryGetValue(id, out var tile))
                {
                    if (Sprites.TryGetValue(tile.TextureId, out var sprite))
                        return sprite;
                }*//*
            }*/

            // If it is not part of a tileset, we check if it is a regular tile.
            if (Tiles.TryGetValue(id, out var tile))
            {
                if(Sprites.TryGetValue(tile.TextureId, out var sprite))
                    return sprite;
            }

            Debug.Error($"Tile with ID {id} not found.");
            return null;
        }

        internal Tileset GetTilesetForTile(int value)
        {
            foreach (var tileset in Tilesets)
            {
                if (tileset.IsTilesetContainsTile(value))
                {
                    return tileset;
                }
            }
            return null;
        }

        #region Adding Tilesets
        public void AddTileset(Tileset tileset)
        {

            Tilesets.Add(tileset);

            Debug.Log("Added: " + tileset.ToString());
        }

        #endregion

        #region Adding Tiles
        public void AddTile(Tile tile)
        {
            int tileId = GetNextAvailableTileId();
            tile.Id = tileId;
            Tiles.Add(tileId, tile);

            // If the tile has an animation it will start the animation coroutine.
            if (tile is AnimatedTile animatedTile)
            {
                var coroutine = Core.StartCoroutine(animatedTile.Animate());
            }

            Debug.Log("Added: " + tile.ToString());
        }
        private int GetNextAvailableTileId()
        {
            return GetNextAvailableIdAcrossCollections(new IDictionary<int, object>[]
            {
                Tiles.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value),

            }, 1);
        }
        #endregion

        #region Adding Textures

        protected void AddTexture(int id, Sprite sprite)
        {
            if (!Sprites.ContainsKey(id))
            {
                Sprites[id] = sprite;
            }
            else
            {
                Debug.Error($"Texture with ID {id} already exists.");
            }
        }
        protected void AddTexture(Sprite sprite)
        {
            int id = GetNextAvailableTextureId();
            AddTexture(id, sprite);
        }
        protected void AddTexture(int id, Texture2D texture)
        {
            AddTexture(id, new Sprite(texture));
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