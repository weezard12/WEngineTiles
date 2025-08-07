using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    internal class TilesPainter : Entity, IUpdatable
    {
        public TilesPainter()
        {
            Name = "tiles-painter";
        }

        public override void Update()
        {
            base.Update();
            // just for testing
            if (Input.LeftMouseButtonDown)
            {
                TilesWorld world = (TilesWorld) Scene;
                TilesUserInfo userInfo = (TilesUserInfo)world.FindEntity("tiles-user-info");

                TilesChunk chunk = world.GetChunk(userInfo.MouseChunk);
                chunk?.GetLayers().Last().SetTile(userInfo.SelectedTile, 7);
            }
        }
    }
}
