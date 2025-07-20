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
using Button = MonoGameGum.Forms.Controls.Button;
using Label = MonoGameGum.Forms.Controls.Label;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {

        public override void OnStart()
        {
            base.OnStart();

            // Enable UI Canvas
            //var canvas = CreateEntity("ui-canvas").AddComponent<UICanvas>();

            var gumCanvas = CreateEntity("gum-canvas").AddComponent<GumCanvas>( new GumCanvas("MyComponents/EditorWindow") );

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


            

            return;
            var window = new Window();
            window.Anchor(Gum.Wireframe.Anchor.Center);
            window.Width = 300;
            window.Height = 200;
            window.AddToRoot();

            var textInstance = new Label();
            textInstance.Dock(Gum.Wireframe.Dock.Top);
            textInstance.Y = 24;
            textInstance.Text = "Hello I am a message box";
            window.AddChild(textInstance);

            var button = new Button();
            button.Anchor(Gum.Wireframe.Anchor.Bottom);
            button.Y = -10;
            button.Text = "Close";
            window.AddChild(button.Visual);
            button.Click += (_, _) =>
            {
                window.RemoveFromRoot();
            };
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
