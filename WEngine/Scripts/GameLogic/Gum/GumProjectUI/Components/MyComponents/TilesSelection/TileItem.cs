using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez.Textures;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.Main.Utils;

partial class TileItem
{
    Tile _tile;

    public TileItem(Tile tile) : base()
    {
        _tile = tile;


        // Setting up the tile Tile Display texture
        Sprite sprite = EditorScreen.Instance.WorldEditor.RenderingManager.GetSprite(_tile);

        GumUtils.SetGumSpriteToNezSprite(TileSprite, sprite, 200, 200);

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
