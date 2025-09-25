using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using Nez.Textures;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;
using WEngine.Scripts.Main.Utils;

partial class EditingTileItem
{
    TextureSelectionWindow textureSelectionWindow;

    Tile tile;

    TileItem TileDisplay;
    internal void SetTile(Tile tile)
    {
        this.tile = tile;
        UpdateTileDisplay();
        ConfirmButton.Click += OnConfirmButtonClick;

        // Adds all of the tile types to the combo box
        foreach (var item in EditorTilesManager.TileTypes.Values)
            TileTypeMenu.Items.Add(item.Name);
        
        // Sets the selected tile type
        TileTypeMenu.SelectedObject = EditorTilesManager.GetTileType(tile).Name;

        TileTypeMenu.SelectionChanged += (s, e) =>
        {
            string selectedTypeName = (string) TileTypeMenu.SelectedObject;
            TileType selectedTileType = EditorTilesManager.GetTileType(selectedTypeName);


            Tile newTile = (Tile) tile.ConvertToType(selectedTileType.Type);

            // Change the pointer to the new tile.
            tile = newTile;

            UpdateTileDisplay();
        };
    }

    private void OnConfirmButtonClick(object sender, System.EventArgs e)
    {
        EditorScreen.Instance.TilesSelectionWindowInstance.LoadTiles();
    }

    private void UpdateTileDisplay()
    {
        TileDisplay?.RemoveFromRoot();
        TileDisplay = new TileItem(tile);

        // Making the sprite editable
        TileDisplay.SpriteContainer.Click += (s, e) =>
        {
            if (textureSelectionWindow == null)
            {
                Debug.Log("Select a texture");
                textureSelectionWindow = new TextureSelectionWindow();
                GumUtils.CenterToScreen(textureSelectionWindow);
                textureSelectionWindow.OnDialogComplete += TextureSelectionWindow_OnDialogComplete;
                textureSelectionWindow.OnClosed += () =>
                {
                    textureSelectionWindow = null;
                };
            }
            EditorScreen.Instance.AddChild(textureSelectionWindow);
        };

        
        InnerPanel.AddChild(TileDisplay);
        foreach (EditorProperty tileProperty in EditorTilesManager.GetTileType(tile).Properties)
        {
            tileProperty.ShowUI(tile);
        }
    }

    private void TextureSelectionWindow_OnDialogComplete(DialogResult result)
    {
        tile.TextureId = result.GetValue<int>();
        UpdateTileDisplay();
    }

    partial void CustomInitialize()
    {
        
        
    }
}
