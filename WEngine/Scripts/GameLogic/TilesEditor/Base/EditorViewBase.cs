using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Main;

namespace WEngine.Scripts.GameLogic.TilesEditor.Base
{
    internal class EditorViewBase : Entity
    {
        public string ViewName { get; set; }

        public UICanvas Canvas { get; private set; }
        protected Window UIWindow { get; private set; }

        public EditorViewBase(string viewName)
        {
            ViewName = viewName;

            Name = viewName;
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            // Create a full-screen UI canvas for this view
            Canvas = AddComponent<UICanvas>();
            Canvas.IsFullScreen = true;

            var skin = Skin.CreateDefaultSkin();
            UIWindow = new Window(ViewName, skin);

            UIWindow.SetBackground(new PrimitiveDrawable(Color.Black * 0.5f, 2f)); // Semi-transparent with 2px borde

            // Initialize the UI for this view
            InitializeUI(Canvas.Stage, UIWindow);
        }

        protected virtual void InitializeUI(Stage stage, Window window)
        {
            stage.AddElement(window);
        }
    }
}