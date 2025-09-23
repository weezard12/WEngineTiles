using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez.Textures;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;

partial class TileItem
{
    Tile _tile;

    public TileItem(Tile tile) : base()
    {
        _tile = tile;


        // Setting up the tile Tile Display texture
        Sprite sprite = EditorScreen.Instance.WorldEditor.RenderingManager.GetSprite(_tile);

        TileSprite.Texture = sprite.Texture2D;

        TileSprite.TextureAddress = TextureAddress.Custom;

        TileSprite.TextureTop = sprite.SourceRect.X;
        TileSprite.TextureLeft = sprite.SourceRect.Y;
        TileSprite.TextureWidth = sprite.SourceRect.Width;
        TileSprite.TextureHeight = sprite.SourceRect.Height;

        TileSprite.Width = 200;
        TileSprite.Height = 200;

        // Tile Type
        TileType.Text = _tile.GetType().Name;
        
        // Tile Name
        TileName.Text = EditorScreen.Instance.WorldEditor.GetTileName(_tile);
        TileName.TextConfirmed += OnTileNameTextConfirmed; ;
    }

    private void OnTileNameTextConfirmed(object sender, System.EventArgs e)
    {
        EditorScreen.Instance.WorldEditor.SetTileName(_tile, TileName.Text);
    }


    partial void CustomInitialize()
    {


    }

    public Tile GetTile() => _tile;
}
