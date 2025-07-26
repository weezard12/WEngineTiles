using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using Nez;
using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorWindow
{
    partial void CustomInitialize()
    {
        CloseButton.Click += (sender, e) =>
        {
            Debug.Log("Close button clicked");
            Visual.RemoveFromRoot();
            Visual.RemoveFromManagers();
        };
        MinimizeButton.Click += (sender, e) =>
        {
            Debug.Log("Minimize button clicked");
            SizeStylesState = SizeStyles.Windowed == SizeStylesState ? SizeStyles.Minimized : SizeStyles.Windowed;
        };
    }
}
