using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;

partial class EditingTileItem
{
    Tile tile;

    TileItem TileDisplay;
    internal void SetTile(Tile tile)
    {
        this.tile = tile;

    }
    private void UpdateTileDisplay()
    {
        TileDisplay?.RemoveFromRoot();
        TileDisplay = new TileItem();

/*        Sprite sprite = renderingManager.GetSprite(tile);

        TileDisplay.TileSprite.Texture = sprite.Texture2D;

        TileDisplay.TileSprite.TextureAddress = TextureAddress.Custom;

        TileDisplay.TileSprite.TextureTop = sprite.SourceRect.X;
        TileDisplay.TileSprite.TextureLeft = sprite.SourceRect.Y;
        TileDisplay.TileSprite.TextureWidth = sprite.SourceRect.Width;
        TileDisplay.TileSprite.TextureHeight = sprite.SourceRect.Height;
        
        TileDisplay.TileType.Text = item.tile.GetType().Name;
        
        TileDisplay.TileSprite.Width = 200;
        TileDisplay.TileSprite.Height = 200;*/

        InnerPanel.AddChild(new TileItem());
    }

    partial void CustomInitialize()
    {
        
        
    }
}
