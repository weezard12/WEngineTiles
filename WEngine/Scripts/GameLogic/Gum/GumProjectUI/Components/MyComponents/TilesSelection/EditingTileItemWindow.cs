using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;

partial class EditingTileItemWindow
{
    Tile tile;
    public EditingTileItemWindow(Tile tile)
    {
        this.tile = tile;
    }
    partial void CustomInitialize()
    {
        EditingTileItemInstance.SetTile(tile);
    }
}
