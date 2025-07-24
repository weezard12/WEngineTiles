using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System.Collections.Generic;
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

    public File(string fullPath)
    {
        string spriteName = "fileDefaultIcon.png";
        if (FileIcons.TryGetValue(fullPath, out spriteName))
        {

        }
        FileIcon.SpriteInstance.SourceFileName = "fileDefaultIcon.png";
    }
    partial void CustomInitialize()
    {
        
    }
}
