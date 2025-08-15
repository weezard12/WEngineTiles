using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using WEngine.Scripts.GameLogic.TilesEditor.Tools;

partial class ToolUI
{
    private readonly EditorTool _editorTool;
    public ToolUI(EditorTool editorTool) : base()
    {
        _editorTool = editorTool;
        ToolIcon.Texture = editorTool.Sprite.Texture2D;
        ToolIcon.TextureAddress = TextureAddress.Custom;

        ToolIcon.TextureTop = editorTool.Sprite.SourceRect.X;
        ToolIcon.TextureLeft = editorTool.Sprite.SourceRect.Y;
        ToolIcon.TextureWidth = editorTool.Sprite.SourceRect.Width;
        ToolIcon.TextureHeight = editorTool.Sprite.SourceRect.Height;

        ToolIcon.Width = 200;
        ToolIcon.Height = 200;
    }
    partial void CustomInitialize()
    {
    
    }
}
