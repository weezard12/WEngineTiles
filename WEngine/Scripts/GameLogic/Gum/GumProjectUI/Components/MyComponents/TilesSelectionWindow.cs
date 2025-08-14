
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
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.Main;
using static WEngine.Scripts.Main.Game1;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        Debug.Log("Custom Initialize");
        ImportTilesetButton.Click += (_, _) =>
        {
            Tile tile = new Tile();
            Game1.CurrentGumScreen.AddChild(new EditingTileItemWindow(tile));
        };
    }

    public void LoadTiles(RenderingManager renderingManager)
    {
        // Load tiles from the rendering manager

        foreach (var item in renderingManager.GetTiles())
        {
            TileItem tile = new TileItem(item.tile);

            EditorWindowContentInstance.ScrollViewerInstance.AddChild(tile);
        }
    }

    public void LoadTiles()
    {
        LoadTiles(EditorScreen.Instance.RenderingManager);
    }
}
