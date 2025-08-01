using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.Tiles
{
    /// <summary>
    /// This class is for keeping the info that the user provides about the world.
    /// for example the tile that the mouse is curentlly hovering over, the chunk that the camera is over, etc.
    /// </summary>
    internal class TilesUserInfo : Entity, IUpdatable
    {
        TilesWorld _tilesWorld;
        public Point MouseChunk;
        public Point CameraChunk;
        public Point SelectedTile;

        public TilesUserInfo()
        {
            // This should make it update first, before the other entities.
            UpdateOrder = -100;
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            _tilesWorld = (TilesWorld) Scene;
        }

        // Every frame, this will update the user info based on the camera and mouse position.
        // Then this info can be accesed in other components or systems.
        public override void Update()
        {
            var cameraPos = Core.Scene.Camera.Position;
            var mouseScreenPos = Input.MousePosition;
            var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);

            _tilesWorld.GetChunkCoordinates(cameraPos, ref CameraChunk);
            _tilesWorld.GetChunkCoordinates(mouseWorldPos, ref MouseChunk);
            _tilesWorld.GetTileCordinates(mouseWorldPos, ref SelectedTile);
        }
    }
}
