
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System;
using System.Linq;
using static WEngine.Scripts.Main.Game1;

partial class TilesSelectionWindow
{
    partial void CustomInitialize()
    {
        Debug.Log("Custom Initialize");
        ImportTilesetButton.Click += (_, _) =>
        {
            CurrentGumScreen.AddChild(new SelectFileWindow().Visual);
        };
    }


    public void LoadTiles(object sender, EventArgs e)
    {

    }
}
