
using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using Nez;
using Nez.Textures;
using RenderingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;
using WEngine.Scripts.Main;
using WEngine.Scripts.Main.Utils;
using static WEngine.Scripts.Main.Game1;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        NewTileButton.Click += (_, _) =>
        {
            Debug.Log("Adding new tile to the rendering manager");
            Tile tile = new Tile();
            EditorScreen.Instance.RenderingManager.AddTile(tile);
            EditingTileItemWindow editingTileItemWindow = new EditingTileItemWindow(tile);
            Game1.CurrentGumScreen.AddChild(editingTileItemWindow);

            GumUtils.CenterToScreen(editingTileItemWindow);
        };
    }

    public void LoadTiles(RenderingManager renderingManager)
    {
        // Load tiles from the rendering manager

        SelectionPanelInstance.ClearItems();

        foreach (var item in renderingManager.GetTiles())
        {
            TileItem tile = new TileItem(item.tile);

            SelectionPanelItemHolder selectionPanelItemHolder = SelectionPanelInstance.AddItem(tile);

            // This will open the editing window when double clicking the tile item
            selectionPanelItemHolder.Visual.DoubleClick += (_, _) =>
            {
                EditingTileItemWindow editingTileItemWindow = new EditingTileItemWindow(item.tile);
                Game1.CurrentGumScreen.AddChild(editingTileItemWindow);
                GumUtils.CenterToScreen(editingTileItemWindow);
            };
        }
    }

    public void LoadTiles()
    {
        LoadTiles(EditorScreen.Instance.RenderingManager);
    }

    public Tile GetSelectedTile()
    {
        // Gets the selected tile ui item
        SelectionPanelItemHolder itemHolder = SelectionPanelInstance.SelectedItem;
        if (itemHolder == null)
            return null;

        TileItem tileItem = itemHolder.Item as TileItem;

        return tileItem.GetTile();
    }
}
