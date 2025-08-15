using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.Tools
{
    internal class EraserTool : EditorTool
    {
        public override void Setup()
        {
            Name = "Eraser Tool";
            Description = "Allows you to erase tiles by clicking and dragging on the world.";
            Sprite = new Sprite(Core.Content.Load<Texture2D>("Assets\\Editor\\Icons\\eraser icon"));
        }
    }
}
