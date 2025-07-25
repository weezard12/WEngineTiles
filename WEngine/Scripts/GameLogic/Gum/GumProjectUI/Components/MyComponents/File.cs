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
        { "png", "pngIcon.png" },
        { "", "" },

    };

    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string FilePath { get; set; }
    public string ContainingDirectoryPath { get; set; }

    public bool IsFolder { get; private set; }

    public File(string fullPath, bool isFolder = false)
    {
        this.IsFolder = isFolder;
        this.FileName = Path.GetFileName(fullPath);

        FileNameLabel.Text = Path.GetFileName(fullPath);

        if (isFolder)
        {
            FileIcon.SpriteInstance.SourceFileName = "folderIcon.png";
            return;
        }
        string spriteName = "fileDefaultIcon.png";
        if (FileIcons.TryGetValue(fullPath, out spriteName))
        {
            
        }
        FileIcon.SpriteInstance.SourceFileName = "fileDefaultIcon.png";
    }
    partial void CustomInitialize()
    {
        ContainerInstance.Click += (sender, e) =>
        {
            Debug.Log($"Clicked on file: {FileName}");
        };
        
    }
    
}
