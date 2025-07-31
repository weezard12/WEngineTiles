using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesWindLayerRenderer : TilesLayerRenderer
    {
        private Effect _windEffect;
        private float _time;

        public TilesWindLayerRenderer(TilesLayer tilesLayer) : base(tilesLayer)
        {
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            return;
            _windEffect = Core.Content.Load<Effect>("Assets/Shaders/WindShader");
            int width = Core.GraphicsDevice.Viewport.Width;
            int height = Core.GraphicsDevice.Viewport.Height;
            foreach (var item in _windEffect.Parameters)
            {
                Debug.Log(item?.Name);
            }

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
            _windEffect.Parameters["View_projection"].SetValue(Matrix.Identity * projection);
            
            Material = new Material(_windEffect);
        }
        public override void Render(Batcher batcher, Camera camera)
        {
            //batcher.Begin(_windEffect);
            //_windEffect.Parameters["Time"].SetValue(_time);
            base.Render(batcher, camera);
            //batcher.End();
        }
    }
}
