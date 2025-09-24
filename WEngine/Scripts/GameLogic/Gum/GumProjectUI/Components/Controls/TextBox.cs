using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;
using WEngine.Scripts.Main;

partial class TextBox
{
    partial void CustomInitialize()
    {
        GotFocus += TextBox_GotFocus;
        LostFocus += TextBox_LostFocus;
    }
    
    // Those methods are custom implementation to check if the Gum UI is focused.

    private void TextBox_LostFocus(object sender, EventArgs e)
    {
        Game1.IsTextBoxFocused = false;
    }

    private void TextBox_GotFocus(object sender, System.EventArgs e)
    {
        Game1.IsTextBoxFocused = true;
    }
}
