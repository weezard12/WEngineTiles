using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.Main.Utils
{
    internal static class ColorUtils
    {
        public static Texture2D CreateColorTexture(Color color)
        {
            var tex = new Texture2D(Core.GraphicsDevice, 1, 1);
            tex.SetData(new[] { color });
            return tex;
        }
    }
}
