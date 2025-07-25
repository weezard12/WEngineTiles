using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

partial class File
{
    private static readonly Dictionary<string, string> FileIcons = new()
    {
        { ".png", "pngIcon.png" },
        { "", "" },
    };

    public string FileName { get; set; }
    public string FileExtension => Path.GetExtension(FileName);
    public string FilePath { get; set; }
    public string ContainingDirectoryPath { get; set; }

    public bool IsFolder { get; private set; }

    private readonly FilesViewer FilesViewer;

    public File(FilesViewer filesViewer, string fullPath, bool isFolder = false)
    {
        this.IsFolder = isFolder;
        this.FileName = Path.GetFileName(fullPath);
        this.FilesViewer = filesViewer;

        this.FilePath = fullPath;

        FileNameLabel.Text = Path.GetFileName(fullPath);

        if (isFolder)
        {
            FileIcon.SourceFileName = "folderIcon.png";
            return;
        }

        if(FileIcons.TryGetValue(FileExtension, out string specialTexture))
            FileIcon.SourceFileName = specialTexture;
        else
            FileIcon.SourceFileName = "fileDefaultIcon.png";
    }
    partial void CustomInitialize()
    {
        FileClickBounds.Click += (sender, e) =>
        {
            Debug.Log($"Clicked1 on file: {FileName}");
            if (IsFolder)
            {
                FilesViewer.ToggleFolder(FilePath);
                FilesViewer.RefreshView();
            }
        };

        FileClickBounds.HoverOver += (sender, e) =>
        {

        };
    }
    
}
