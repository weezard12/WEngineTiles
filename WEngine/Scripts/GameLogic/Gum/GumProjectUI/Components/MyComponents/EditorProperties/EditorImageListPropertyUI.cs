using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using WEngine.Scripts.GameLogic.TilesEditor.TilesManagment;
using WEngine.Scripts.Main.Utils;

partial class EditorImageListPropertyUI
{
    TextureSelectionWindow textureSelectionWindow;

    // quick access to the list of texture ids. (since its not a generic i cant do that)
    List<int> texturesIds;

    public override void Setup(object target, EditorProperty editorProperty)
    {
        base.Setup(target, editorProperty);

        texturesIds = _editorProperty.GetValue<List<int>>(_target);
        RefreshFramesPanel();
    }
    partial void CustomInitialize()
    {
        AddFrameButton.Visual.Click += AddFrameButtonClick;
    }

    private void AddFrameButtonClick(object sender, EventArgs e)
    {
        if(textureSelectionWindow != null)
        {
            textureSelectionWindow.Close();
        }
            
        textureSelectionWindow = new TextureSelectionWindow();
        GumUtils.CenterToScreen(textureSelectionWindow);
        textureSelectionWindow.OnDialogComplete += TextureSelectionWindow_OnDialogComplete;
        textureSelectionWindow.OnClosed += () =>
        {
            textureSelectionWindow = null;
        };

        EditorScreen.Instance.AddChild(textureSelectionWindow);
    }

    private void TextureSelectionWindow_OnDialogComplete(DialogResult result)
    {
        int newTextureId = result.GetValue<int>();
        texturesIds.Add(newTextureId);

        RefreshFramesPanel();
    }

    private void RefreshFramesPanel()
    {
        FramesSelectionPanel.ClearItems();

        foreach (var textureId in texturesIds)
        {
            Nez.Textures.Sprite nezSprite = EditorScreen.Instance.WorldEditor.RenderingManager.GetSprite(textureId);

            SpriteRuntime gumSprite = new SpriteRuntime();
            GumUtils.SetGumSpriteToNezSprite(gumSprite, nezSprite, 200, 200);

            
            SelectionPanelItemHolder itemHolder = FramesSelectionPanel.AddItem(gumSprite);

            itemHolder.Width = 20;
            itemHolder.Height = 20;
        }
    }
}
