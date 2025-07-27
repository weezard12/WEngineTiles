using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using Nez;
using System;

partial class EditorWindow
{
    public event Action OnClosed;
    public event Action<SizeStyles?> OnMinimized;
    partial void CustomInitialize()
    {
        CloseButton.Click += (sender, e) =>
        {
            Debug.Log("Close button clicked");
            RemoveFromRoot();
            OnClosed?.Invoke();
        };
        MinimizeButton.Click += (sender, e) =>
        {
            Debug.Log("Minimize button clicked");
            SizeStylesState = SizeStyles.Windowed == SizeStylesState ? SizeStyles.Minimized : SizeStyles.Windowed;
            OnMinimized?.Invoke(SizeStylesState);
        };
    }

    public void RemoveFromRoot()
    {
        Visual.RemoveFromRoot();
        Visual.RemoveFromManagers();
    }
}
