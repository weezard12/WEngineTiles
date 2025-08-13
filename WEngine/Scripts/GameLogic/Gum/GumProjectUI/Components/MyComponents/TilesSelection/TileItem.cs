using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;

partial class TileItem
{
    Tile _tile;

    public TileItem(Tile tile)
    {
        _tile = tile;
    }

    partial void CustomInitialize()
    {
    }
}
