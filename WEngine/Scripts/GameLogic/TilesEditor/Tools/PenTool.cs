using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    internal class PenTool : EditorTool
    {
        public PenTool()
        {

        }

        public override void Setup()
        {
            Name = "Pen Tool";
            Description = "Allows you to paint or modify tiles by clicking and dragging on the world.";
            Sprite = new Sprite(Core.Content.Load<Texture2D>("Assets\\Editor\\Icons\\pen icon"));
        }

        protected override void UseTool()
        {
            
            if(WorldEditor.GetChunk(UserInfo.MouseChunk) == null)
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

                layer.SetTile(UserInfo.SelectedTile.X, UserInfo.SelectedTile.Y, 1);
            }
            else
            {
                TilesLayer layer = tilesChunk.GetLayers()[0];
                layer.SetTile(UserInfo.SelectedTile.X, UserInfo.SelectedTile.Y, 1);
            }
        }
    }
}
