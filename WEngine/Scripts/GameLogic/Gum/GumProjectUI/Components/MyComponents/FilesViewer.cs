using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToolsUtilities;

partial class FilesViewer
{
    public string AbsolutePath { get; private set; }

    private readonly HashSet<string> OpenedFolders = new HashSet<string>();

    partial void CustomInitialize()
    {
        SetAbsolutePath("C:\\Users\\User1\\Downloads\\test");

        PathTextBox.Text = AbsolutePath;


        PathTextBox.LostFocus += (sender, e) =>
        {
            if(Directory.Exists(PathTextBox.Text))
                SetAbsolutePath(PathTextBox.Text);
            else
                PathTextBox.Text = AbsolutePath; // Reset to the last valid path
        };
        PathTextBox.KeyDown += (sender, e) =>
        {
            if (e.Key == Microsoft.Xna.Framework.Input.Keys.Enter)
            {
                if(Directory.Exists(PathTextBox.Text))
                    SetAbsolutePath(PathTextBox.Text);
                
                else
                    PathTextBox.IsFocused = false; // Remove focus to trigger LostFocus
            }
        };
    }

    public void SetAbsolutePath(string path)
    {

        if(path.EndsWith('\\'))
            path = path.Substring(0, path.Length - 1); // Remove trailing backslash if present

        AbsolutePath = path;
        PathTextBox.Text = AbsolutePath;
        RefreshView();
    }
    public void RefreshView()
    {
        ScrollViewerInstance.InnerPanel.Children.Clear();
        RefreshView(AbsolutePath, 0);
    }
    private void RefreshView(string path, int depth)
    {
        foreach (var file in Directory.GetFiles(path))
        {
            ScrollViewerInstance.AddChild(new File(this, file) { X = depth * 20 });
        }

        foreach (var file in Directory.GetDirectories(path))
        {
            ScrollViewerInstance.AddChild(new File(this, file, true) { X = depth * 20});
            if (OpenedFolders.Contains(file))
            {
                RefreshView(file, ++depth);
            }
        }
    }

    // If the folder in the path is open it will colse it. if its closed it will open it.
    public void ToggleFolder(string path)
    {
        if (!OpenedFolders.Remove(path))
            OpenedFolders.Add(path);


    }
}
