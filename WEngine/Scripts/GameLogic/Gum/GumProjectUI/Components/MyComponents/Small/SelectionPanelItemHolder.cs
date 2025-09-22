using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class SelectionPanelItemHolder
{
    public FrameworkElement Item { get; private set; }

    public SelectionPanelItemHolder(FrameworkElement item)
    {
        this.Item = item;
    }

    partial void CustomInitialize()
    {
        
    }
}
