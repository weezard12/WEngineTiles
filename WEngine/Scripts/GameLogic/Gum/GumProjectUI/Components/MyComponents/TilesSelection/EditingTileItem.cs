using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using Nez.Textures;

using WEngine.Scripts.GameLogic.Tiles;

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
    }

    private void OnConfirmButtonClick(object sender, System.EventArgs e)
    {
        EditorScreen.Instance.RenderingManager.AddTile(tile);
        EditorScreen.Instance.TilesSelectionWindowInstance.LoadTiles();
    }

    private void UpdateTileDisplay()
    {
        TileDisplay?.RemoveFromRoot();
        TileDisplay = new TileItem();

        Sprite sprite = EditorScreen.Instance.WorldEditor.RenderingManager.GetSprite(tile);

        TileDisplay.TileSprite.Texture = sprite.Texture2D;

        TileDisplay.TileSprite.TextureAddress = TextureAddress.Custom;

        TileDisplay.TileSprite.TextureTop = sprite.SourceRect.X;
        TileDisplay.TileSprite.TextureLeft = sprite.SourceRect.Y;
        TileDisplay.TileSprite.TextureWidth = sprite.SourceRect.Width;
        TileDisplay.TileSprite.TextureHeight = sprite.SourceRect.Height;
        
        TileDisplay.TileType.Text = tile.GetType().Name;
        
        TileDisplay.TileSprite.Width = 200;
        TileDisplay.TileSprite.Height = 200;

        TileDisplay.SpriteContainer.Click += (s, e) =>
        {
            if (textureSelectionWindow == null)
            {
                Debug.Log("Select a texture");
                textureSelectionWindow = new TextureSelectionWindow();
                textureSelectionWindow.OnDialogComplete += TextureSelectionWindow_OnDialogComplete;
                textureSelectionWindow.OnClosed += () =>
                {
                    textureSelectionWindow = null;
                };
            }
            EditorScreen.Instance.AddChild(textureSelectionWindow);
        };

        InnerPanel.AddChild(TileDisplay);
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
