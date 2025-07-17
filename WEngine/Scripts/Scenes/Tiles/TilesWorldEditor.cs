using ImGuiNET;
using Microsoft.Xna.Framework;
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
            var canvas = CreateEntity("ui-canvas").AddComponent<UICanvas>();
            canvas.IsFullScreen = true;

            SetupTileSelectionUI(canvas.Stage);
           //AddEntity(new TilesSelector());
        }
        public override void Update()
        {
            base.Update();
        }


        private void SetupTileSelectionUI(Stage stage)
        {


            var rootTable = new Table();
            stage.AddElement(rootTable);
            rootTable.SetFillParent(true);
            rootTable.Top().Right().Pad(10);

            var container = new Container();
            container.SetBackground(new PrimitiveDrawable(Color.DarkSlateGray, 4)); // Optional background


            var innerTable = new Table();

            // Add your UI elements to the inner table
            innerTable.Add(new Label("Tile Selector")).Pad(10);
            innerTable.Row(); // Move to the next row
            innerTable.Add(new TextButton("Tile 1", new TextButtonStyle())).Pad(10);
            innerTable.Row(); // Move to the next row
            innerTable.Add(new TextButton("Tile 2", new TextButtonStyle())).Pad(10);

            // Add the table into the container
            container.SetElement(innerTable);  // Not AddElement — this sets the wrapped widget

            rootTable.Add(container).Top().Right().Pad(10);
        }
    }
}
