using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;
using Nez.Tweens;
using System;
using UILabel = Nez.UI.Label;
using static WEngine.Scripts.Scenes.Tiles.TilesWorld;
using WEngine.Scripts.Scenes.Tiles;
using WEngine.Scripts.GameLogic.Tiles;

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    internal class CameraInfoDisplay : Component, IUpdatable
    {
        private UICanvas _canvas;
        private UILabel _cameraLabel;
        private UILabel _mouseLabel;
        private UILabel _chunkLabel;
        private UILabel _tileLabel;
        private TilesWorld _tilesWorld;
        private TilesUserInfo _tilesUserInfo;
        private Table _table;
        private Vector2Tween _offsetTween;

        private Vector2 _currentOffset = Vector2.Zero;
        private Vector2 _targetOffset = Vector2.Zero;

        public Vector2 Offset
        {
            get => _targetOffset;
            set
            {
                _targetOffset = value;
                UpdateTablePosition();
            }
        }
        public void SetOffset(Vector2 newOffset)
        {
            AnimateToOffset(newOffset);
        }
        public float TweenDuration { get; set; } = 0.3f;
        public EaseType TweenEaseType { get; set; } = EaseType.QuadOut;

        public CameraInfoDisplay(TilesWorld tilesWorld, TilesUserInfo tilesUserInfo)
        {
            _tilesWorld = tilesWorld;
            _tilesUserInfo = tilesUserInfo;
        }

        public override void OnAddedToEntity()
        {
            _canvas = Entity.AddComponent(new UICanvas());
            _canvas.IsFullScreen = true;

            _table = _canvas.Stage.AddElement(new Table());
            _table.SetPosition(_currentOffset.X, _currentOffset.Y);
            _table.SetFillParent(true);
            _table.Top().Left();

            _cameraLabel = new UILabel("Loading Camera Info", Skin.CreateDefaultSkin());
            _cameraLabel.SetFontScale(1.5f);
            _table.Add(_cameraLabel).SetPadLeft(10).SetPadTop(10).Left();
            _table.Row();

            _mouseLabel = new UILabel("Loading Mouse Info", Skin.CreateDefaultSkin());
            _mouseLabel.SetFontScale(1.5f);
            _table.Add(_mouseLabel).SetPadLeft(10).Top().Left();
            _table.Row();

            _chunkLabel = new UILabel("Loading Chunk Info", Skin.CreateDefaultSkin());
            _chunkLabel.SetFontScale(1.5f);
            _table.Add(_chunkLabel).SetPadLeft(10).Top().Left();
            _table.Row();

            _tileLabel = new UILabel("Loading Tile Info", Skin.CreateDefaultSkin());
            _tileLabel.SetFontScale(1.5f);
            _table.Add(_tileLabel).SetPadLeft(10).Top().Left();
        }

        private void AnimateToOffset(Vector2 newOffset)
        {
            if (_offsetTween != null)
                _offsetTween.Stop();

            _offsetTween = Vector2Tween.Create();
            _offsetTween.SetFrom(_currentOffset);


            // Properly set up the tween with update and completion handlers
            var tween = PropertyTweens.Vector2PropertyTo(this, nameof(Offset), newOffset, TweenDuration)
                .SetEaseType(TweenEaseType)
                .SetCompletionHandler(t =>
                {
                    OnOffsetTweenComplete();
                });

            
            tween.Start();
            _offsetTween = (Vector2Tween)tween;
        }


        private void OnOffsetTweenComplete()
        {
            Debug.Log($"Info Tween Completed");
        }

        private void UpdateTablePosition()
        {
            _table.SetPosition(_targetOffset.X, _targetOffset.Y);
        }

        public void Update()
        {
            var cameraPos = Core.Scene.Camera.Position;
            var cameraZoom = Core.Scene.Camera.Zoom;
            _cameraLabel.SetText($"Camera Position: X={cameraPos.X:0}, Y={cameraPos.Y:0}\nCamera Zoom: {cameraZoom:0.00}");

            var mouseScreenPos = Input.MousePosition;
            var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);
            _mouseLabel.SetText($"Mouse Position:\nScreen X={mouseScreenPos.X:0}, Y={mouseScreenPos.Y:0}\nWorld X={mouseWorldPos.X:0}, Y={mouseWorldPos.Y:0}");

            _chunkLabel.SetText($"Chunk Info:\nCamera Chunk: X={_tilesUserInfo.CameraChunk.X}, Y={_tilesUserInfo.CameraChunk.Y}\nMouse Chunk: X={_tilesUserInfo.MouseChunk.X}, Y={_tilesUserInfo.MouseChunk.Y}");

            _tileLabel.SetText($"Selected Tile:\nX={_tilesUserInfo.SelectedTile.X}, Y={_tilesUserInfo.SelectedTile.Y}");

        }

        public override void OnRemovedFromEntity()
        {
            // Clean up tween when component is removed

        }
    }
}