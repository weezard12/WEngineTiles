using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System.Collections.Generic;
using System.Linq;

partial class FileMenuItem
{
    partial void CustomInitialize()
    {
        // Create and configure the "New" submenu item
        var newItem = new MenuItem();
        newItem.Header = "New";
        newItem.Clicked += (s, e) => Debug.Log("New menu item clicked");

        //Items.Add(newItem);
    }
}
