
using Gum.DataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.Forms;
using Nez;
using Nez.ImGuiTools;
using RenderingLibrary;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using WEngine.Scripts.Scenes;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.Main
{
    public class Game1 : Core
    {
        public static GumProjectSave LoadedGumProject { get; private set; }

        public Game1()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // TODO: Add your initialization logic here
            //MonoGameGum.GumService.Default.Initialize(this);

            LoadedGumProject = GumService.Default.Initialize(this, "Gum/GumProject/GumProject.gumx");


            // This assumes that your project has at least 1 screen
            //var screenRuntime = LoadedGumProject.Screens.First().ToGraphicalUiElement();
            //screenRuntime.AddToRoot();

            //debug
            DebugRenderEnabled = true;
            //System.Reflection.Assembly.Load("Nez.ImGui");
            //var imGuiManager = new ImGuiManager();
            //RegisterGlobalManager(imGuiManager);

            // toggle ImGui rendering on/off. It starts out enabled.
            //imGuiManager.SetEnabled(true);

            Scene = new TilesWorldEditor();
        }

        protected override void Update(GameTime gameTime)
        {
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
            MonoGameGum.GumService.Default.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // TODO: Add your drawing code here

            MonoGameGum.GumService.Default.Draw();
        }


        // This replaces Program.cs
        [STAThread]
        public static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
