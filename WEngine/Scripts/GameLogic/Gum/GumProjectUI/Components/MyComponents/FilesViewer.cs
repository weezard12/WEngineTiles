using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System.IO;
using System.Linq;

partial class FilesViewer
{
    public string AbsolutePath { get; set; }

    partial void CustomInitialize()
    {
        SetAbsolutePath("C:\\");
    }

    public void SetAbsolutePath(string path)
    {
        AbsolutePath = path;
        foreach (var file in Directory.GetFiles(path))
        {
            ScrollViewerInstance.AddChild(new File(file));
        }
        foreach (var file in Directory.GetDirectories(path))
        {
            ScrollViewerInstance.AddChild(new File(file));
        }

    }
}
