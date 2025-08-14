using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToolsUtilities;

partial class FilesViewer
{
    public string AbsolutePath { get; private set; }

    private readonly HashSet<string> OpenedFolders = new HashSet<string>();

    private readonly HashSet<string> SelectedFiles = new HashSet<string>();

    public int SelectFilesAmount { get; set; } = 1;

    public event Action<string> OnFileSelected;
    public event Action<string> OnFileUnselected;

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
            ScrollViewerInstance.AddChild(new FileComponent(this, file) { X = depth * 20 });
        }

        foreach (var file in Directory.GetDirectories(path))
        {
            ScrollViewerInstance.AddChild(new FileComponent(this, file, true) { X = depth * 20});
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

    public bool SelectFile(string filePath)
    {
        // can select infinite files if SelectFilesAmount is negative
        if (SelectFilesAmount < 0)
        {
            SelectedFiles.Add(filePath);
            OnFileSelected?.Invoke(filePath);
            return true;
        }

        if (SelectedFiles.Count < SelectFilesAmount)
        {
            SelectedFiles.Add(filePath);
            OnFileSelected?.Invoke(filePath);
            return true;
        }

        return false;
    }
    public bool SelectFile(FileComponent file)
    {
        return SelectFile(file.FilePath);
    }

    public void UnselectFile(string filePath)
    {
        SelectedFiles.Remove(filePath);
        OnFileUnselected?.Invoke(filePath);
    }
    public void UnselectFile(FileComponent file)
    {
        UnselectFile(file.FilePath);
    }


    public FileComponent GetFile(string path)
    {
        foreach (var child in ScrollViewerInstance.InnerPanel.Children)
            if (child is FileComponent file && file.FilePath == path)
                return file;

        return null;
    }
}
