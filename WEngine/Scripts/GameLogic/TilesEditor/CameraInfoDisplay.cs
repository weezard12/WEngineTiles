using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;
using System;
using UILabel = Nez.UI.Label;
using static WEngine.Scripts.Scenes.Tiles.TilesWorld;
using WEngine.Scripts.Scenes.Tiles; // for ChunkSize

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    internal class CameraInfoDisplay : Component, IUpdatable
    {
        private UICanvas _canvas;
        private UILabel _cameraLabel;
        private UILabel _mouseLabel;
        private UILabel _chunkLabel;

        private TilesWorld _tilesWorld;

        Point cameraChunk = Point.Zero;
        Point mouseChunk = Point.Zero;

        public CameraInfoDisplay(TilesWorld tilesWorld)
        {
            _tilesWorld = tilesWorld;
        }
        public override void OnAddedToEntity()
        {
            _canvas = Entity.AddComponent(new UICanvas());
            _canvas.IsFullScreen = true;

            var table = _canvas.Stage.AddElement(new Table());
            table.SetFillParent(true);
            table.Top().Left();

            _cameraLabel = new UILabel("Loading Camera Info", Nez.UI.Skin.CreateDefaultSkin());
            _cameraLabel.SetFontScale(1.5f);
            table.Add(_cameraLabel).SetPadLeft(10).SetPadTop(10).Left();
            table.Row();

            _mouseLabel = new UILabel("Loading Mouse Info", Nez.UI.Skin.CreateDefaultSkin());
            _mouseLabel.SetFontScale(1.5f);
            table.Add(_mouseLabel).SetPadLeft(10).Top().Left();
            table.Row();

            _chunkLabel = new UILabel("Loading Chunk Info", Nez.UI.Skin.CreateDefaultSkin());
            _chunkLabel.SetFontScale(1.5f);
            table.Add(_chunkLabel).SetPadLeft(10).Top().Left();
        }

        public void Update()
        {
            var cameraPos = Core.Scene.Camera.Position;
            var cameraZoom = Core.Scene.Camera.Zoom;
            _cameraLabel.SetText($"Camera Position: X={cameraPos.X:0}, Y={cameraPos.Y:0}\nCamera Zoom: {cameraZoom:0.00}");

            var mouseScreenPos = Input.MousePosition;
            var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);
            _mouseLabel.SetText($"Mouse Position:\nScreen X={mouseScreenPos.X:0}, Y={mouseScreenPos.Y:0}\nWorld X={mouseWorldPos.X:0}, Y={mouseWorldPos.Y:0}");


            _tilesWorld.GetChunkCoordinates(cameraPos, ref cameraChunk);
            
            _tilesWorld.GetChunkCoordinates(mouseWorldPos, ref mouseChunk);

            _chunkLabel.SetText($"Chunk Info:\nCamera Chunk: X={cameraChunk.X}, Y={cameraChunk.Y}\nMouse Chunk: X={mouseChunk.X}, Y={mouseChunk.Y}");
        }



    }
}
