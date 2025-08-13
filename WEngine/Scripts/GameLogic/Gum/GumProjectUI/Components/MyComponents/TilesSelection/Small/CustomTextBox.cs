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
        //StatesState = States.Normal;
        Visual.Click += OnVisualClick;
        Visual.RollOn += OnVisualRollOn;
        
        TextBoxInstance.LostFocus += OnTextBoxInstanceLostFocus;
        
    }

    private void OnTextBoxInstanceLostFocus(object sender, EventArgs e)
    {
        Debug.Log("lost focuse");
        //Visual.ExposeChildrenEvents = false;
        //StatesState = States.Normal;
        TextBoxInstance.IsVisible = false;
    }

    private void OnVisualRollOn(object sender, EventArgs e)
    {
        
    }

    private void OnVisualClick(object sender, EventArgs e)
    {
        if(StatesState == null)
        {
            //StatesState = States.Focused;
            Visual.ExposeChildrenEvents = true;
            TextBoxInstance.IsVisible = true;
            TextBoxInstance.IsFocused = true;
            
            Debug.Log("focuse");
        }

            
    }
}
