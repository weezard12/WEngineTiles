using Microsoft.Xna.Framework.Graphics;
using Nez;
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
        private readonly Dictionary<int, Texture2D> textures = new();

        public Texture2D GetTexture(int id)
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
                textures[id] = texture;
            }
            else
            {
                Debug.Error($"Texture with ID {id} already exists.");
            }
        }

        List<TilesChunk> Chunks { get; set; }

    }
}
