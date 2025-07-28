using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesLayerRenderer : RenderableComponent, IUpdatable
    {
        public const int SizeX = 3;
        public const int SizeY = 3;
        private readonly Texture2D _texture;
        private readonly Sprite _sprite;
        private readonly int _tileWidth;
        private readonly int _tileHeight;

        private float _scale = 5f;

        public override float Width => _tileWidth * SizeX * _scale;
        public override float Height => _tileHeight * SizeY * _scale;
        public override RectangleF Bounds
        {
            get
            {
                var position = Entity.Transform.Position;
                return new RectangleF(
                    position.X - Width / 2f,
                    position.Y - Height / 2f,
                    Width,
                    Height
                );
            }
        }

        public TilesLayerRenderer(Texture2D texture)
        {
            this._texture = texture;
            _sprite = new Sprite(texture);
            _tileWidth = texture.Width;
            _tileHeight = texture.Height;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
        }

        public override void Render(Batcher batcher, Camera camera)
        {
            var position = Entity.Transform.Position;

            // Offset to center the tile grid on the entity's position
            var centeredPosition = position - new Vector2(Width / 2f, Height / 2f);

            // Draw grid tiles centered around the entity
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    var tilePos = new Vector2(
                        centeredPosition.X + x * _tileWidth * _scale,
                        centeredPosition.Y + y * _tileHeight * _scale
                    );
                    batcher.Draw(_sprite, tilePos, Color.White, 0, new Vector2(_tileWidth, _tileHeight), _scale, SpriteEffects.None, 0);
                }
            }
        }


        public void Update()
        {
            // Update logic if needed
        }
    }
}
