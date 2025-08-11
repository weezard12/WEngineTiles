using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.Scenes.Tiles;

partial class EditorScreen
{
    public static EditorScreen Instance { get; private set; }
    public TilesWorldEditor WorldEditor { get; private set; }


    public EditorScreen(TilesWorldEditor tilesWorld)
    {
        Instance = this;

        WorldEditor = tilesWorld;

        WorldEditor.RenderingManager.FinishedLoadingAssets += () =>
        {
            // Load the tileset after the assets are loaded
            TilesSelectionWindowInstance.LoadTiles(WorldEditor.RenderingManager);
        };
    }
    partial void CustomInitialize()
    {
        TilesSelectionWindowInstance.IsVisible = true;


    }
}
