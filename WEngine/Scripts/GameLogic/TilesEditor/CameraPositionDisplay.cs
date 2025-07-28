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
    public class CameraPositionDisplay : Component, IUpdatable
    {
        private UICanvas _canvas;
        private UILabel _label;

        public override void OnAddedToEntity()
        {
            _canvas = Entity.AddComponent(new UICanvas());
            _canvas.IsFullScreen = true;

            var table = _canvas.Stage.AddElement(new Table());
            table.SetFillParent(true);
            table.Top().Left();

            _label = new UILabel("Loading Camera Position", Nez.UI.Skin.CreateDefaultSkin());
            _label.SetFontScale(1.5f);
            table.Add(_label).SetPadLeft(10).SetPadTop(10);
        }

        public void Update()
        {
            var cameraPos = Core.Scene.Camera.Position;
            _label.SetText($"Camera Position: X={cameraPos.X:0}, Y={cameraPos.Y:0}");
        }

    }
}
