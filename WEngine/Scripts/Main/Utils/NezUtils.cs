using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.Main.Utils
{
    internal class NezUtils
    {
        /// <summary>
        /// Adds entities to the given scene, each with a sprite from the list.
        /// Entities will be placed in a row from left to right.
        /// </summary>
        /// <param name="scene">The Nez scene to add entities to.</param>
        /// <param name="sprites">List of sprites to use.</param>
        /// <param name="startPos">Starting position for the first sprite (default: (0,0)).</param>
        /// <param name="spacing">Extra horizontal spacing between sprites (default: 0).</param>
        public static void AddSpritesRow(Scene scene, List<Sprite> sprites, Vector2? startPos = null, float spacing = 0f)
        {
            if (scene == null || sprites == null || sprites.Count == 0)
                return;

            Vector2 pos = startPos ?? Vector2.Zero;

            foreach (var sprite in sprites)
            {
                // Create entity
                var entity = scene.CreateEntity("spriteEntity");

                // Attach sprite renderer
                entity.AddComponent(new SpriteRenderer(sprite));

                // Place entity
                entity.Transform.Position = pos;

                // Move to the right for the next sprite
                pos.X += sprite.Texture2D.Width + spacing;
            }
        }
    }
}
