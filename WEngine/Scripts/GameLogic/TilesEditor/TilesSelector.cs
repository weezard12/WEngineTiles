using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.TilesEditor.Base;

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    internal class TilesSelector : EditorViewBase
    {
        public TilesSelector() : base("Tiles Selector")
        {
        }

        protected override void InitializeUI(Stage stage, Window window)
        {
            base.InitializeUI(stage, window);

            window.SetPosition(Screen.Center.X, Screen.Center.Y); // Set position of the window


            var rootTable = new Table();
            rootTable.SetFillParent(true);
            rootTable.Top().Right();

            ScrollPane scrollPane = new ScrollPane(rootTable, Skin.CreateDefaultSkin());

            // Right-align the panel
            

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
                tilePanel.Row(); // Move to the next row after each button
            }

            rootTable.Add(tilePanel);
            window.Add(rootTable);
        }
    }

}
