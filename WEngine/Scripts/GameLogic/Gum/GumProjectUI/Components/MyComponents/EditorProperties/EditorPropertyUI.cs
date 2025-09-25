using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;

partial class EditorPropertyUI
{
    protected object _target;
    protected EditorProperty _editorProperty;

    partial void CustomInitialize()
    {
        
    }

    public EditorPropertyUI(object target, EditorProperty editorProperty)
    {
        this._target = target;
        this._editorProperty = editorProperty;

        PropertyNameLabel.Text = _editorProperty.Name;
    }

}
