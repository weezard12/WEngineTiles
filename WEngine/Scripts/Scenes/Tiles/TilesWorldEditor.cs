using ImGuiNET;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.TilesEditor;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {

        public override void OnStart()
        {
            base.OnStart();

            // Enable UI Canvas
            //var canvas = CreateEntity("ui-canvas").AddComponent<UICanvas>();
            //canvas.IsFullScreen = true;

            ///SetupTileSelectionUI(canvas.Stage);
           AddEntity(new TilesSelector());
        }
        public override void Update()
        {
            base.Update();
        }


        private void SetupTileSelectionUI(Stage stage)
        {
            var rootTable = new Table();
            rootTable.SetFillParent(true);

            // Right-align the panel
            rootTable.Top().Right();

            var tilePanel = new Table();
            tilePanel.Defaults().Pad(4).Size(64); // padding and button size

            // Example: adding 4 tile buttons
            for (int i = 0; i < 4; i++)
            {
                var tileButton = new TextButton($"Tile {i}", Skin.CreateDefaultSkin());

                int tileIndex = i; // capture index for closure
                tileButton.OnClicked += button =>
                {
                    // Replace this with your tile selection logic
                    Debug.Log($"Selected tile {tileIndex}");
                };

                tilePanel.Add(tileButton);
            }

            rootTable.Add(tilePanel);
            stage.AddElement(rootTable);
        }
    }
}
