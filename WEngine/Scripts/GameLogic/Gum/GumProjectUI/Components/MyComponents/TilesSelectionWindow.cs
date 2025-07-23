using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        ImportTilesetButton.Click += LoadTiles;
    }

    public void LoadTiles(object sender, EventArgs e)
    {

    }
}
