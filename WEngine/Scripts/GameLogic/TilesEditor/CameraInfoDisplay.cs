using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UILabel = Nez.UI.Label;

namespace WEngine.Scripts.GameLogic.TilesEditor
{
    public class CameraInfoDisplay : Component, IUpdatable
    {
        private UICanvas _canvas;
        private UILabel _cameraLabel;
        private UILabel _mouseLabel;

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
        }

        public void Update()
        {
            var cameraPos = Core.Scene.Camera.Position;
            var cameraZoom = Core.Scene.Camera.Zoom;
            _cameraLabel.SetText($"Camera Position: X={cameraPos.X:0}, Y={cameraPos.Y:0}\nCamera Zoom: {cameraZoom:0.00}");

            var mouseScreenPos = Input.MousePosition;
            var mouseWorldPos = Core.Scene.Camera.ScreenToWorldPoint(mouseScreenPos);
            _mouseLabel.SetText($"Mouse Position:\nScreen X={mouseScreenPos.X:0}, Y={mouseScreenPos.Y:0}\nWorld X={mouseWorldPos.X:0}, Y={mouseWorldPos.Y:0}");
        }
    }
}
