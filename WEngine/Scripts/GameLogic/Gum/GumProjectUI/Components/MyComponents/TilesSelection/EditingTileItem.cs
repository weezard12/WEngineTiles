using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum.GueDeriving;
using Nez;
using Nez.Textures;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;
using WEngine.Scripts.Main.Utils;

partial class EditingTileItem
{
    TextureSelectionWindow textureSelectionWindow;

    // The tile have to be a reference to the tile in the RenderingManager.
    // If it will be an object, then its just a pointer - when the type is changed to an animated tile the pointer changes but the tile in the rendering manager stays the same.
    private Tile _tile {
        get => GetTile();
        set => SetTile(value);
    } 
    

    private int tileId;

    TileItem TileDisplay;

    internal void SetTile(int tileId)
    {
        this.tileId = tileId;
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

        AddPropertisUI();
    }

    private void AddPropertisUI()
    {
        TileType tileType = EditorTilesManager.GetTileType(_tile);

        for (int i = 0; i < tileType.Properties.Count; i++)
        {
            EditorPropertyUI propertyUI = tileType.ShowUI(i, _tile);
            Debug.Log("Adding property: " + propertyUI.Name);
            InnerPanel.AddChild(propertyUI);
        }
    }

    private void TextureSelectionWindow_OnDialogComplete(DialogResult result)
    {
        _tile.TextureId = result.GetValue<int>();
        UpdateTileDisplay();
    }


    private void SetTile(Tile tile)
    {
        EditorScreen.Instance.RenderingManager.SetTile(tileId, tile);
    }
    private Tile GetTile()
    {
        return EditorScreen.Instance.RenderingManager.GetTile(tileId);
    }
}
