using System.Collections.Generic;

partial class TextureSelectionWindow
{
    
    partial void CustomInitialize()
    {
        List<(int id, Nez.Textures.Sprite sprite)> sprites = new List<(int id, Nez.Textures.Sprite sprite)> ();

        foreach (var sprite in EditorScreen.Instance.RenderingManager.GetSprites())
        {
            sprites.Add( new (sprite.id, sprite.sprite) );
            StackPanelInstance.AddChild(new TilesSprite(sprite.id, sprite.sprite));
        }
    }
}
