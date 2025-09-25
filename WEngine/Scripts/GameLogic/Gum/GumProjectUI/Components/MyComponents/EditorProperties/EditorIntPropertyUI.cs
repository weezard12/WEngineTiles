using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;

partial class EditorIntPropertyUI
{
    partial void CustomInitialize()
    {
    
    }
    public override void Setup(object target, EditorProperty editorProperty)
    {
        base.Setup(target, editorProperty);

        TextBoxInstance.TextChanged += (s, e) =>
        {
            _editorProperty.SetValue(target, TextBoxInstance.Text);
        };
    }
}
