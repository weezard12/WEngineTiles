using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Input;
using Nez;
using Nez.ImGuiTools;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using WEngine.Scripts.GameLogic.Project;
using WEngine.Scripts.Scenes;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.Main
{
    public class Game1 : Core
    {
        public static ImGuiManager ImGuiManager { get; private set; }

        public static GumProjectSave LoadedGumProject { get; private set; }
        public static GraphicalUiElement CurrentGumScreen;

        public static bool IsCursorOverGum { get; private set; }
        public static bool IsTextBoxFocused { get; set; }

        public Game1()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "WEngine - Tiles";
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            UpdateGumCanvasSize();
        }

        private void UpdateGumCanvasSize()
        {
            // TODO improve this gum resizing logic

            if(CurrentGumScreen == null)
                return;

            LoadedGumProject.DefaultCanvasWidth = Window.ClientBounds.Width;
            LoadedGumProject.DefaultCanvasHeight = Window.ClientBounds.Height;
            CurrentGumScreen.Width = Window.ClientBounds.Width;
            CurrentGumScreen.Height = Window.ClientBounds.Height;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // No FPS limit
/*            IsFixedTimeStep = false;
            Screen.SynchronizeWithVerticalRetrace = false;
            Screen.ApplyChanges();*/

            // Gum Logic Initialization
            //MonoGameGum.GumService.Default.Initialize(this);

            LoadedGumProject = GumService.Default.Initialize(this, "Gum/GumProject/GumProject.gumx");

/*            LoadGumScreen("TestScreen");
            return;*/

            // Load and configure the component

            // This assumes that your project has at least 1 screen
            //var screenRuntime = LoadedGumProject.Screens.First().ToGraphicalUiElement();
            //screenRuntime.AddToRoot();

            // Debug
            DebugRenderEnabled = true;
            //System.Reflection.Assembly.Load("Nez.ImGui");
            ImGuiManager = new ImGuiManager();
            RegisterGlobalManager(ImGuiManager);

            // toggle ImGui rendering on/off. It starts out enabled.
            ImGuiManager.SetEnabled(false);

            ProjectManager.Initialize();
            //ProjectManager.CreateProject("Test Project");
            Scene = new EditorHomeScene();
        }

        protected override void Update(GameTime gameTime)
        {
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
            GumService.Default.Update(gameTime);

            IsCursorOverGum = GumService.Default.Cursor.WindowOver != null;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Renders MonoGame + Nez
            base.Draw(gameTime);

            // Renders Gum
            GumService.Default.Draw(); 
        }


        #region Gum Logic (still not integrated with Nez rendering)
        public static GraphicalUiElement LoadGumScreen(string screenName)
        {
            // Load the Gum screen by name
            var gumScreen = LoadedGumProject.Screens.FirstOrDefault(s => s.Name == screenName);
            if (gumScreen != null)
            {
                // Clears the existing screen
                GumService.Default.Root.Children.Clear();

                // Convert to GraphicalUiElement and add to root
                var graphicalUiElement = gumScreen.ToGraphicalUiElement();
                graphicalUiElement.AddToRoot();

                CurrentGumScreen = graphicalUiElement;

                return graphicalUiElement;
            }

                Debug.Error($"Gum screen '{screenName}' not found.");
                Debug.Log($"List of all existing screens:");
                foreach (var existingScreen in LoadedGumProject.Screens)
                    Debug.Log(existingScreen.Name);

                return null;
        }
        public static void SetGumScreen(FrameworkElement gumScreen)
        {
            if (gumScreen != null)
            {
                // Clears the existing screen
                GumService.Default.Root.Children.Clear();

                // Convert to GraphicalUiElement and add to root
                var graphicalUiElement = gumScreen.Visual;
                graphicalUiElement.AddToRoot();

                CurrentGumScreen = graphicalUiElement;
            }
        }
        protected GraphicalUiElement LoadGumComponent(string componentName)
        {
            // Load the Gum component by name
            var gumComponent = LoadedGumProject.Components.FirstOrDefault(c => c.Name == componentName);
            if (gumComponent != null)
            {
                // Convert to GraphicalUiElement and add to root
                var graphicalUiElement = gumComponent.ToGraphicalUiElement();
                CurrentGumScreen.AddChild(graphicalUiElement);
                return graphicalUiElement;
            }

                Debug.Error($"Gum component '{componentName}' not found.");
                Debug.Log($"List of all existing components:");
                foreach (var existingComponent in LoadedGumProject.Components)
                    Debug.Log(existingComponent.Name);

            return null;
        }

        protected GraphicalUiElement CreateEditorWindow(string title, GraphicalUiElement content)
        {
            // Load the Gum component by name
            var gumComponent = LoadedGumProject.Components.FirstOrDefault(c => c.Name == "Controls/WindowStandard");
            if (gumComponent != null)
            {
                // Convert to GraphicalUiElement and add to root
                var graphicalUiElement = gumComponent.ToGraphicalUiElement();
                CurrentGumScreen.AddChild(graphicalUiElement);
                return graphicalUiElement;
            }


            Debug.Log($"List of all existing components:");
            foreach (var existingComponent in LoadedGumProject.Components)
                Debug.Log(existingComponent.Name);

            return null;
        }

        

        #endregion

        // This replaces Program.cs
        [STAThread]
        public static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
