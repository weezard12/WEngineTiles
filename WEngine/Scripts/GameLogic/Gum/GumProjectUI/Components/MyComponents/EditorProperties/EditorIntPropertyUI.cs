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
    public EditorIntPropertyUI(object target, EditorProperty editorProperty) : base(target, editorProperty)
    {
        TextBoxInstance.Text = _editorProperty.GetValue<int>(target).ToString();


        TextBoxInstance.TextChanged += (s, e) =>
        {
            _editorProperty.SetValue(target, TextBoxInstance.Text);
        };
        
    }
}
