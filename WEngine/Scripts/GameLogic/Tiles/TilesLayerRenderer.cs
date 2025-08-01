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
        private TilesLayer _tilesLayer;


        public override float Width => _tilesLayer.TileWidth * _tilesLayer.SizeX * _tilesLayer._scale;
        public override float Height => _tilesLayer.TileHeight * _tilesLayer.SizeY * _tilesLayer._scale;
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

        public TilesLayerRenderer(TilesLayer tilesLayer)
        {
            _tilesLayer = tilesLayer;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
        }

        public override void Render(Batcher batcher, Camera camera)
        {
            var position = Entity.Transform.Position;

            var totalWidth = _tilesLayer.TotalWidth;
            var totalHeight = _tilesLayer.TotalHeight;

            var startX = position.X - totalWidth / 2f + (_tilesLayer.TileWidth * _tilesLayer._scale) / 2f;
            var startY = position.Y - totalHeight / 2f + (_tilesLayer.TileHeight * _tilesLayer._scale) / 2f;

            for (int y = 0; y < _tilesLayer.SizeY; y++)
            {
                for (int x = 0; x < _tilesLayer.SizeX; x++)
                {
                    if (_tilesLayer.GetTile(x, y) == 0)
                        continue;
                    
                    var tilePos = new Vector2(
                        startX + x * _tilesLayer.TileWidth * _tilesLayer._scale,
                        startY + y * _tilesLayer.TileHeight * _tilesLayer._scale
                    );

                    batcher.Draw(
                        ((TilesWorld)Entity.Scene).GetTexture(_tilesLayer.GetTile(x, y)),
                        tilePos,
                        Color.White,
                        rotation: 0f,
                        origin: new Vector2(_tilesLayer.TileWidth / 2f, _tilesLayer.TileHeight / 2f),
                        scale: _tilesLayer._scale,
                        effects: SpriteEffects.None,
                        layerDepth: 0f
                    );
                }
            }
        }

        public override void DebugRender(Batcher batcher)
        {
            

        }

        public void Update()
        {
            // Update logic if needed
        }


    }
}
