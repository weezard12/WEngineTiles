using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class ToolSelectionWindow
{
    partial void CustomInitialize()
    {
        ToolsPanel.SelectionChanged += ToolsPanel_SelectionChanged;
    }

    private void ToolsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ((ToolUI)e.NewSelection.Item).Selected();
        if(e.PreviousSelection != null)
            ((ToolUI)e.PreviousSelection.Item).UnSelected();
        
    }
}
