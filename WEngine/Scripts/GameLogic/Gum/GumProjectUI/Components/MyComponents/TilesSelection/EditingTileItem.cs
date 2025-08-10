using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditingTileItem
{
    partial void CustomInitialize()
    {
        InnerPanel.AddChild(new TileItem());
    }
}
