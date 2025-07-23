
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System;
using System.Linq;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        Debug.Log("Custom Initialize");
        ImportTilesetButton.Click += (_,_) => Debug.Log("Import Tileset Button Clicked");
    }


    public void LoadTiles(object sender, EventArgs e)
    {

    }
}
