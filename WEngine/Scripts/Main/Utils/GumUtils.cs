using Gum.Forms.Controls;
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
    }
}
