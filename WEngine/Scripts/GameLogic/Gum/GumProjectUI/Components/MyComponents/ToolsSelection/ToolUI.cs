using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Linq;
using WEngine.Scripts.GameLogic.TilesEditor.Tools;
using WEngine.Scripts.Main.Utils;

partial class ToolUI
{
    private readonly EditorTool _editorTool;
    public ToolUI(EditorTool editorTool) : base()
    {
        _editorTool = editorTool;
        GumUtils.SetGumSpriteToNezSprite(ToolIcon, editorTool.Sprite, 50, 50);

    }
    partial void CustomInitialize()
    {
    
    }

    public void Selected()
    {
        _editorTool.SetEnabled(true);
    }

    internal void UnSelected()
    {
        _editorTool.SetEnabled(false);
    }
}
