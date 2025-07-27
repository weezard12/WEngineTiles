using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;

partial class DialogEditorWindow
{
    public event Action<string> OnDialogComplete;
    partial void CustomInitialize()
    {
        OnClosed += () =>
        {
            OnDialogComplete?.Invoke(String.Empty);
        };
    }
    protected void RaiseDialogComplete(string result)
    {
        OnDialogComplete?.Invoke(result);
    }
}
