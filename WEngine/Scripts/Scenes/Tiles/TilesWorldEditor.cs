using Gum.Converters;
using Gum.DataTypes;
using Gum.Wireframe;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using Nez;
using Nez.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Gum;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.TilesEditor;
using WEngine.Scripts.Main;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {
        private TilesSelectionWindow tilesSelectionWindow;

        public override void OnStart()
        {
            base.OnStart();

            // Getting the Gum UI Elements.
            tilesSelectionWindow = Game1.CurrentGumScreen.GetFrameworkElementByName<TilesSelectionWindow>("TilesSelectionWindowInstance");

            // Testing tiles rendering
            Entity testEntity = new Entity("TestEntity");
            testEntity.SetPosition(Screen.Center);

            Texture2D tileTexture = Content.LoadTexture("Assets/Tiles/Tile");

            TilesLayerRenderer layerRenderer = new TilesLayerRenderer(tileTexture);
            testEntity.AddComponent(layerRenderer);

            AddEntity(testEntity);

        }
        public override void Update()
        {
            base.Update();
        }
    }
}
