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
using static System.Formats.Asn1.AsnWriter;

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

            // Creates Camera Controller for the Editor.
            Entity entity = CreateEntity("camera-controller");
            entity.AddComponent(new EditorCameraController());

            // Displays the camera position.
            var displayEntity = CreateEntity("camera-display");
            displayEntity.AddComponent(new CameraInfoDisplay());


            AddTexture("Assets/Tiles/Tile");

            // Testing tiles rendering
            Entity testEntity = new Entity("TestEntity");
            testEntity.SetPosition(Screen.Center);

            Texture2D tileTexture = Content.LoadTexture("Assets/Tiles/Tile");

            TilesLayerRenderer layerRenderer = new TilesLayerRenderer(16, 16);
            testEntity.AddComponent(layerRenderer);

            layerRenderer.SetTile(0, 0, 1);
            layerRenderer.SetTile(1, 1, 1);
            layerRenderer.SetTile(2, 2, 1);
            layerRenderer.SetTile(3, 3, 1);
            AddEntity(testEntity);

        }
        public override void Update()
        {
            base.Update();
        }
    }
}
