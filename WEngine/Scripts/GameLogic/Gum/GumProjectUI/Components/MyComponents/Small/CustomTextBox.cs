using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System;
using System.Linq;

partial class CustomTextBox
{
    private string _text;
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                LabelInstance.Text = _text;
                TextBoxInstance.Text = _text;
            }
        }
    }
    public event EventHandler TextChanged;
    public event EventHandler TextConfirmed;
    partial void CustomInitialize()
    {
        Visual.Click += OnVisualClick;
        Visual.RollOn += OnVisualRollOn;

        TextBoxInstance.LostFocus += OnTextBoxInstanceLostFocus;
        TextBoxInstance.KeyDown += OnTextBoxInstanceKeyDown;

        TextBoxInstance.TextChanged += OnTextBoxInstanceTextChanged;

        // Initialize binding between the UI and property
        Text = LabelInstance.Text;
    }

    private void OnTextBoxInstanceTextChanged(object sender, EventArgs e)
    {
        // Keep property in sync
        _text = TextBoxInstance.Text;
        LabelInstance.Text = _text;

        // Raise your own event
        TextChanged?.Invoke(this, e);
    }

    private void OnTextBoxInstanceKeyDown(object sender, Gum.Forms.Controls.KeyEventArgs key)
    {
        if (key.Key == Microsoft.Xna.Framework.Input.Keys.Enter)
        {
            TextBoxInstance.IsFocused = false;
        }
    }

    private void OnTextBoxInstanceLostFocus(object sender, EventArgs e)
    {
        Debug.Log("Lost focus");
        TextBoxInstance.IsVisible = false;
        Visual.ExposeChildrenEvents = false;

        // Sync property when focus is lost
        Text = TextBoxInstance.Text;
        TextConfirmed?.Invoke(this, e);
    }

    private void OnVisualRollOn(object sender, EventArgs e)
    {

    }

    private void OnVisualClick(object sender, EventArgs e)
    {
        Visual.ExposeChildrenEvents = true;
        TextBoxInstance.IsVisible = true;
        TextBoxInstance.IsFocused = true;

        Debug.Log("Focused");
    }
}
