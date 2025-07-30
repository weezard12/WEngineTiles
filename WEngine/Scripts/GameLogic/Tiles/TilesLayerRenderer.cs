using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesLayerRenderer : RenderableComponent, IUpdatable
    {
        public const int SizeX = 8;
        public const int SizeY = 8;
        private readonly int _tileWidth;
        private readonly int _tileHeight;

        private readonly float _scale = 4f;

        private readonly int[,] _tiles = new int[SizeX, SizeY];

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

        public TilesLayerRenderer(int tileWidth, int tileHeight)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
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
                    if (_tiles[y,x] == 0)
                        continue;
                    
                    var tilePos = new Vector2(
                        startX + x * _tileWidth * _scale,
                        startY + y * _tileHeight * _scale
                    );

                    batcher.Draw(
                        ((TilesWorld)Entity.Scene).GetTexture(_tiles[y,x]),
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


        public void SetTile(int x, int y, int tileId)
        {
            if (x < 0 || x >= SizeX || y < 0 || y >= SizeY)
                throw new ArgumentOutOfRangeException("Tile coordinates are out of bounds.");
            _tiles[y, x] = tileId;
        }
    }
}
