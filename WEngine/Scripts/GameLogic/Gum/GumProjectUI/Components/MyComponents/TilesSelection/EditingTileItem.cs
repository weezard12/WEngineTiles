using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum.GueDeriving;
using Nez;
using Nez.Textures;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;
using WEngine.Scripts.Main.Utils;

partial class EditingTileItem
{
    TextureSelectionWindow textureSelectionWindow;

    private Tile _tile;

    TileItem TileDisplay;


    internal void SetTile(Tile tile)
    {
        this._tile = tile;
        UpdateTileDisplay();
        ConfirmButton.Click += OnConfirmButtonClick;

        SetupChangintTileType();
    }

    private void SetupChangintTileType()
    {
        // Adds all of the tile types to the combo box
        foreach (var item in EditorTilesManager.TileTypes.Values)
            TileTypeMenu.Items.Add(item.Name);

        // Sets the selected tile type
        TileTypeMenu.SelectedObject = EditorTilesManager.GetTileType(_tile).Name;

        TileTypeMenu.SelectionChanged += (s, e) =>
        {
            string selectedTypeName = (string)TileTypeMenu.SelectedObject;
            TileType selectedTileType = EditorTilesManager.GetTileType(selectedTypeName);

            Debug.Log(selectedTileType);

            Tile newTile = (Tile)_tile.ConvertToType(selectedTileType.Type);

            // Change the pointer to the new tile.
            _tile = newTile;

            UpdateTileDisplay();
        };
    }

    private void OnConfirmButtonClick(object sender, System.EventArgs e)
    {
        EditorScreen.Instance.TilesSelectionWindowInstance.LoadTiles();
    }

    private void UpdateTileDisplay()
    {
        InnerPanel.Visual.Children.Clear();
        TileDisplay?.RemoveFromRoot();
        TileDisplay = new TileItem(_tile);

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

        
        if(_tile is AnimatedTile)
        {
            Debug.Log("Tile is animated");
        }

        InnerPanel.AddChild(TileDisplay);
        foreach (EditorProperty tileProperty in EditorTilesManager.GetTileType(_tile).Properties)
        {
            Debug.Log("Adding property: " + tileProperty.Name);
            InnerPanel.AddChild(tileProperty.ShowUI(_tile));
        }
    }

    private void TextureSelectionWindow_OnDialogComplete(DialogResult result)
    {
        _tile.TextureId = result.GetValue<int>();
        UpdateTileDisplay();
    }

    partial void CustomInitialize()
    {
        
        
    }
}
