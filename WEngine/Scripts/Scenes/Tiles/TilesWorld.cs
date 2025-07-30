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
            chunk.SetPosition(new Vector2(8*16*5*x,8*16*5*y));
            Chunks.Add(chunk);

            AddEntity(chunk);
        }
    }
}
