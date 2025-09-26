using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using System;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;

partial class EditorIntPropertyUI
{
    private bool _isUpdatingText = false;

    partial void CustomInitialize()
    {

    }

    public override void Setup(object target, EditorProperty editorProperty)
    {
        base.Setup(target, editorProperty);
        _target = target;

        TextBoxInstance.TextChanged += OnTextChanged;

        TextBoxInstance.Text = _editorProperty.GetValue<int>(_target).ToString();
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        if (_isUpdatingText) return; // Prevent recursion

        string inputText = TextBoxInstance.Text;

        // Allow empty string (user might be clearing the field)
        if (string.IsNullOrEmpty(inputText))
        {
            return;
        }

        // Try to parse as integer
        if (int.TryParse(inputText, out int intValue))
        {
            // Valid integer - set the value
            _editorProperty.SetValue(_target, intValue);
        }
        else
        {
            // Invalid input - revert to previous valid value
            var currentValue = _editorProperty.GetValue<int>(_target);

            _isUpdatingText = true;
            TextBoxInstance.Text = currentValue.ToString();
            _isUpdatingText = false;
        }
    }
}