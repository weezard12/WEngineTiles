using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework.Graphics;
using Nez.Textures;

using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;

partial class TilesSprite
{
    public int TextureId { get; private set; }
    public Sprite Sprite { get; private set; }
    public TilesSprite(int textureId, Sprite sprite) : base()
    {
        this.TextureId = textureId;
        this.Sprite = sprite;

        SpriteInstance.Texture = sprite.Texture2D;

        SpriteInstance.TextureAddress = TextureAddress.Custom;

        SpriteInstance.TextureTop = sprite.SourceRect.X;
        SpriteInstance.TextureLeft = sprite.SourceRect.Y;
        SpriteInstance.TextureWidth = sprite.SourceRect.Width;
        SpriteInstance.TextureHeight = sprite.SourceRect.Height;

        SpriteInstance.Width = 200;
        SpriteInstance.Height = 200;
    }
    partial void CustomInitialize()
    {
        
    }
    
}
