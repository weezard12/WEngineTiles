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

            // For testing
            Tiles.Add(1, new Tile { TextureId = 0 });
            Tiles.Add(2, new Tile { TextureId = 2 });
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
            int id = 1;
            while (Sprites.ContainsKey(id))
                id++;
            return id;
        }
        #endregion
    }
}
