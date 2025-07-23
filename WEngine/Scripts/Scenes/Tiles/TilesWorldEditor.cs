using Gum.Converters;
using Gum.DataTypes;
using Gum.Wireframe;
using ImGuiNET;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Gum;
using WEngine.Scripts.GameLogic.TilesEditor;
using WEngine.Scripts.Main;
using Button = MonoGameGum.Forms.Controls.Button;
using Label = MonoGameGum.Forms.Controls.Label;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {
        private TilesSelectionWindow tilesSelectionWindow;

        public override void OnStart()
        {
            base.OnStart();

            // Getting the Gum UI Elements.
            tilesSelectionWindow = Game1.CurrentGumScreen.GetFrameworkElementByName<TilesSelectionWindow>("TilesSelectionWindowInstance");

            // Enable UI Canvas
            //var canvas = CreateEntity("ui-canvas").AddComponent<UICanvas>();

            //var gumCanvas = CreateEntity("gum-canvas").AddComponent<GumCanvas>( new GumCanvas("MyComponents/EditorWindow") );

            //var gumCanvas2 = CreateEntity("gum-canvas").AddComponent<GumCanvas>( new GumCanvas("TestScreen") );

            //canvas.IsFullScreen = true;

            //SetupTileSelectionUI(canvas.Stage);

            //TilesSelector tilesSelector = new TilesSelector();

            //AddEntity(tilesSelector);
            //tilesSelector.SetPosition(Screen.Center);


            // Ensure UI renders in screen space
            //AddRenderer(new ScreenSpaceRenderer(1, null));

            // Create a panel to hold UI controls
            // Root UI panel


        }
        public override void Update()
        {
            base.Update();
        }
    }
}
