using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework.Graphics;
using Nez.Textures;

using System.Linq;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.Main.Utils;

partial class TilesSprite
{
    public int TextureId { get; private set; }
    public Sprite Sprite { get; private set; }
    public TilesSprite(int textureId, Sprite sprite) : base()
    {
        this.TextureId = textureId;
        this.Sprite = sprite;

        GumUtils.SetGumSpriteToNezSprite(SpriteInstance, sprite, 200, 200);
    }
    partial void CustomInitialize()
    {
        
    }
    
}
