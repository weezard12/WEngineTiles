using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;

partial class EditingTileItemWindow
{
    Tile tile;
    public EditingTileItemWindow(Tile tile)
    {
        this.tile = tile;
        EditingTileItemInstance.SetTile(tile);
    }
    partial void CustomInitialize()
    {
        
    }
}
