using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.Main;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.TilesEditor.Components
{
    // TODO add a nice wteening (easing) oprion when zooming in and out

    internal class EditorCameraController : Component, IUpdatable
    {
        float moveSpeed = 300f;
        float zoomSpeed = 0.2f;
        float defaultZoom = 0f;

        public override void OnAddedToEntity()
        {
            // Ensure this is only added to a scene once
            if (Entity.Scene.Camera == null)
                Debug.Error("No camera found in the scene!");
        }

        public void Update()
        {
            var camera = Entity.Scene.Camera;
            var deltaTime = Time.DeltaTime;
            var move = Vector2.Zero;

            // Prevents moving when the mouse is over GumUI
            if (Game1.IsTextBoxFocused)
                return;

            // WASD controls
            if (Input.IsKeyDown(Keys.W))
                move.Y -= 1;
            if (Input.IsKeyDown(Keys.S))
                move.Y += 1;
            if (Input.IsKeyDown(Keys.A))
                move.X -= 1;
            if (Input.IsKeyDown(Keys.D))
                move.X += 1;

            camera.Position += move * moveSpeed * deltaTime;

            // Reset position and zoom with Space key
            if (Input.IsKeyPressed(Keys.Space))
            {
                camera.Position = Vector2.Zero;
                camera.Zoom = defaultZoom;
            }

            // Prevents zooming when the mouse is over GumUI
            if(Game1.IsCursorOverGum)
                return;

            // Mouse wheel zoom
            var mouseState = Input.CurrentMouseState;
            if (mouseState.ScrollWheelValue > Input.PreviousMouseState.ScrollWheelValue)
                camera.Zoom += zoomSpeed;
            else if (mouseState.ScrollWheelValue < Input.PreviousMouseState.ScrollWheelValue)
                camera.Zoom -= zoomSpeed;

        }
    }
}