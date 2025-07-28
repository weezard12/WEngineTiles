using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    internal class EditorCameraController : Component, IUpdatable
    {
        float moveSpeed = 300f;

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

            // Reset to (0, 0) with Space key
            if (Input.IsKeyDown(Keys.Space))
            {
                camera.Position = Vector2.Zero;
            }
        }
    }
}
