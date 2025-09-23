using Gum.Forms.Controls;
using Gum.Managers;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.Main.Utils
{
    internal static class GumUtils
    {
        public static void CenterToScreen(FrameworkElement frameworkElement)
        {
            frameworkElement.Visual.X = Screen.Center.X - frameworkElement.Visual.Width / 2;
            frameworkElement.Visual.Y = Screen.Center.Y - frameworkElement.Visual.Height / 2;
        }

        /// <summary>
        /// This method will set the texture and source rectangle of a Gum SpriteRuntime to the Nez Sprite provided.
        /// </summary>
        /// <param name="gumSprite"></param>
        /// <param name="nezSprite"></param>
        public static void SetGumSpriteToNezSprite(MonoGameGum.GueDeriving.SpriteRuntime gumSprite, Nez.Textures.Sprite nezSprite, int width, int height)
        {
            gumSprite.Texture = nezSprite.Texture2D;

            gumSprite.TextureAddress = TextureAddress.Custom;

            gumSprite.TextureTop = nezSprite.SourceRect.Y;
            gumSprite.TextureLeft = nezSprite.SourceRect.X;
            gumSprite.TextureWidth = nezSprite.SourceRect.Width;
            gumSprite.TextureHeight = nezSprite.SourceRect.Height;

            gumSprite.Width = width;
            gumSprite.Height = height;
        }
    }
}
