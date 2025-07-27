
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
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
            SelectFileWindow selectFileWindow = new SelectFileWindow();
            CurrentGumScreen.AddChild(selectFileWindow);
        };
    }


    public void LoadTiles(object sender, EventArgs e)
    {

    }
}
