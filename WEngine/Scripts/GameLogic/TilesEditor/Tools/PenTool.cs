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
            // Gets the selected tile for painting
            Tile tile = EditorScreen.Instance.TilesSelectionWindowInstance.GetSelectedTile();
            if(tile == null)
            {
                Debug.Warn("No tile selected");
                return;
            }

            if (WorldEditor.GetChunk(UserInfo.MouseChunk) == null)
            {
                WorldEditor.AddChunk(UserInfo.MouseChunk);
                UseTool();
            }
            TilesChunk tilesChunk = WorldEditor.GetChunk(UserInfo.MouseChunk);

            TilesLayer layer;
            if (tilesChunk.GetLayers().Count == 0)
            {
                layer = new TilesLayer();
                tilesChunk.AddLayer(layer);

                var mouseScreenPos = Input.MousePosition;
                var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);

                WorldEditor.GetTileCordinates(mouseWorldPos, ref UserInfo.SelectedTile);
            }
            else
            {
                layer = tilesChunk.GetLayers()[0];
            }
            layer.SetTile(UserInfo.SelectedTile.X, UserInfo.SelectedTile.Y, tile.Id);
        }
    }
}
