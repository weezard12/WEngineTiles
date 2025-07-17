using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;
using System;

namespace WEngine.Scripts.GameLogic.TilesEditor.Base
{
    internal class EditorViewBase : Entity
    {
        public string ViewName { get; protected set; }
        public UICanvas Canvas { get; private set; }
        protected Container RootContainer { get; private set; }

        public EditorViewBase(string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
                throw new ArgumentException("ViewName cannot be null or empty.", nameof(viewName));

            ViewName = viewName;
            Name = viewName; // Set Entity name for easier debugging in Nez
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            // Create a full-screen UI canvas
            Canvas = AddComponent<UICanvas>();
            Canvas.IsFullScreen = true;

            // Create root container with a border and background
            RootContainer = new Container();

            RootContainer.SetBackground(new PrimitiveDrawable(new Color(0.1f, 0.1f, 0.1f, 0.8f))); // Dark, semi-transparent background
            RootContainer.SetPad(5f); // Padding for border spacing

            // Add a label for the view name
            var label = new Label(ViewName, new LabelStyle(new BitmapFont(), Color.White));

            label.SetAlignment(Align.TopLeft);
            RootContainer.AddElement(label);

            // Initialize the UI for this view
            InitializeUI(Canvas.Stage, RootContainer);

            // Add the root container to the stage
            Canvas.Stage.AddElement(RootContainer);
        }

        protected virtual void InitializeUI(Stage stage, Container rootContainer)
        {
            // Derived classes can override to add custom UI elements to rootContainer
        }

        public override void OnRemovedFromScene()
        {
            base.OnRemovedFromScene();
            Canvas?.Stage.Dispose(); // Clear UI elements to prevent memory leaks
        }
    }
}