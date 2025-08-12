using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;

partial class DialogEditorWindow
{
    public event Action<DialogResult> OnDialogComplete;

    partial void CustomInitialize()
    {
    }

    protected void RaiseDialogComplete(object result)
    {
        OnDialogComplete?.Invoke(new DialogResult(result));
    }
}

