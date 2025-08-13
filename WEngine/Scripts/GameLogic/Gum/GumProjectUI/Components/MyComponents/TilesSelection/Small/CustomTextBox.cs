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
    string Text { get; set; }
    partial void CustomInitialize()
    {
        Visual.Click += OnVisualClick;
        Visual.RollOn += OnVisualRollOn;
        
        TextBoxInstance.LostFocus += OnTextBoxInstanceLostFocus;
        TextBoxInstance.KeyDown += OnTextBoxInstanceKeyDown;
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
        Debug.Log("lost focuse");
        TextBoxInstance.IsVisible = false;
        Visual.ExposeChildrenEvents = false;
        LabelInstance.Text = TextBoxInstance.Text;
    }

    private void OnVisualRollOn(object sender, EventArgs e)
    {
        
    }

    private void OnVisualClick(object sender, EventArgs e)
    {
        Visual.ExposeChildrenEvents = true;
        TextBoxInstance.IsVisible = true;
        TextBoxInstance.IsFocused = true;
            
        Debug.Log("focuse");
    }
}
