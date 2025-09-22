using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;

namespace WEngine.Scripts.GameLogic.TilesEditor.Tools
{
    internal class EraserTool : EditorTool
    {
        public override void Setup()
        {
            Name = "Eraser Tool";
            Description = "Allows you to erase tiles by clicking and dragging on the world.";
            Sprite = new Sprite(Core.Content.Load<Texture2D>("Assets\\Editor\\Icons\\eraser icon"));
        }

        protected override void UseTool()
        {
            if (WorldEditor.GetChunk(UserInfo.MouseChunk) == null)
            {
                WorldEditor.AddChunk(UserInfo.MouseChunk);
                UseTool();
            }
            TilesChunk tilesChunk = WorldEditor.GetChunk(UserInfo.MouseChunk);


            if (tilesChunk.GetLayers().Count == 0)
            {
                TilesLayer layer = new TilesLayer();
                tilesChunk.AddLayer(layer);

                var mouseScreenPos = Input.MousePosition;
                var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);

                WorldEditor.GetTileCordinates(mouseWorldPos, ref UserInfo.SelectedTile);

                layer.SetTile(UserInfo.SelectedTile.X, UserInfo.SelectedTile.Y, 0);
            }
            else
            {
                TilesLayer layer = tilesChunk.GetLayers()[0];
                layer.SetTile(UserInfo.SelectedTile.X, UserInfo.SelectedTile.Y, 0);
            }
        }
    }
}
