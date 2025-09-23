using Gum.Managers;
using Nez;
using System.Collections.Generic;

partial class TextureSelectionWindow
{
    
    partial void CustomInitialize()
    {
        List<(int id, Nez.Textures.Sprite sprite)> sprites = new List<(int id, Nez.Textures.Sprite sprite)> ();

        var innerPanel = ScrollViewerInstance.InnerPanel;
        innerPanel.ChildrenLayout = ChildrenLayout.LeftToRightStack;
        innerPanel.WrapsChildren = true;
        innerPanel.StackSpacing = 4;

        foreach (var sprite in EditorScreen.Instance.RenderingManager.GetSprites())
        {
            sprites.Add( new (sprite.id, sprite.sprite));
            Debug.Log($"Loaded sprite with ID: {sprite.id}");
            TilesSprite tileSprite = new TilesSprite(sprite.id, sprite.sprite);
            tileSprite.Visual.Click += (s, e) =>
            {
                Debug.Log(tileSprite.TextureId);
                RaiseDialogComplete(tileSprite.TextureId);
            };
            ScrollViewerInstance.AddChild(tileSprite);
        }
    }
}
