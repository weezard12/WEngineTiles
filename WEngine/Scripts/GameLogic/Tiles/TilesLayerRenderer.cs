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
        public const int SizeX = 8;
        public const int SizeY = 8;
        private readonly Sprite _sprite;
        private readonly int _tileWidth;
        private readonly int _tileHeight;

        private readonly float _scale = 5f;

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

            var totalWidth = _tileWidth * SizeX * _scale;
            var totalHeight = _tileHeight * SizeY * _scale;

            var startX = position.X - totalWidth / 2f + (_tileWidth * _scale) / 2f;
            var startY = position.Y - totalHeight / 2f + (_tileHeight * _scale) / 2f;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    var tilePos = new Vector2(
                        startX + x * _tileWidth * _scale,
                        startY + y * _tileHeight * _scale
                    );

                    batcher.Draw(
                        _sprite,
                        tilePos,
                        Color.White,
                        rotation: 0f,
                        origin: new Vector2(_tileWidth / 2f, _tileHeight / 2f),
                        scale: _scale,
                        effects: SpriteEffects.None,
                        layerDepth: 0f
                    );
                }
            }
        }


        public void Update()
        {
            // Update logic if needed
        }
    }
}
