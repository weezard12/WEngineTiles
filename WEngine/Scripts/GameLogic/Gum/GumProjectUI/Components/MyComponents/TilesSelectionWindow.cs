
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using Nez;
using Nez.Textures;
using System;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;
using static WEngine.Scripts.Main.Game1;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        Debug.Log("Custom Initialize");
        ImportTilesetButton.Click += (_, _) =>
        {

        };
    }

    public void LoadTiles(RenderingManager renderingManager)
    {
        // Load tiles from the rendering manager

        foreach (var item in renderingManager.GetSprites())
        {
            TileItem tile = new TileItem();
            Sprite sprite = item.sprite;

            tile.TileSprite.Texture = sprite.Texture2D;
            tile.TileSprite.SourceRectangle = new Microsoft.Xna.Framework.Rectangle(50,5,50,5);
/*            tile.TileSprite.Width = 200;
            tile.TileSprite.Height = 200;*/
            EditorWindowContentInstance.ScrollViewerInstance.AddChild(tile);

        }
        

    }

    public void LoadTiles(string path)
    {

    }
}
