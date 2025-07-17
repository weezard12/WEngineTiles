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

        protected override void InitializeUI(Stage stage, Container rootContainer)
        {
            base.InitializeUI(stage, rootContainer);

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
            rootContainer.AddElement(rootTable);
        }
    }

}
