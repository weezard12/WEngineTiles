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
using Random = Nez.Random;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {
        private TilesSelectionWindow tilesSelectionWindow;

        public override void OnStart()
        {
            base.OnStart();

            // Getting the Gum UI Elements.
            //tilesSelectionWindow = Game1.CurrentGumScreen.GetFrameworkElementByName<TilesSelectionWindow>("TilesSelectionWindowInstance");

            // Creates Camera Controller for the Editor.
            Entity entity = CreateEntity("camera-controller");
            entity.AddComponent(new EditorCameraController());

            // Displays the camera position.
            var displayEntity = CreateEntity("camera-display");
            displayEntity.AddComponent(new CameraInfoDisplay());

            // Setting up the camera
            Camera.SetPosition(new Microsoft.Xna.Framework.Vector2(0,0));


            AddTexture("Assets/Tiles/Tile");
            AddTexture("Assets/Tiles/Kaftor_Grass");
            AddTexture("Assets/Tiles/Kaftor_Grass2");
            AddTexture("Assets/Tiles/Kaftor_Bush");

            // Testing tiles rendering
            Entity testEntity = new Entity("TestEntity");

            Texture2D tileTexture = Content.LoadTexture("Assets/Tiles/Tile");

            TilesLayerRenderer layerRenderer = new TilesLayerRenderer(16, 16);
            testEntity.AddComponent(layerRenderer);

            for (int x = 0; x < TilesLayerRenderer.SizeX; x++)
            {
                for (int y = 0; y < TilesLayerRenderer.SizeY; y++)
                {
                    layerRenderer.SetTile(x, y, Random.Range(2, 4));
                }
            }

            TilesLayerRenderer grassLayerRenderer = new TilesWindLayerRenderer(16, 16);
            testEntity.AddComponent(grassLayerRenderer);
            
            for (int x = 0; x < TilesLayerRenderer.SizeX; x++)
            {
                for (int y = 0; y < TilesLayerRenderer.SizeY; y++)
                {
                    if(Random.Range(0,4) == 2)
                    grassLayerRenderer.SetTile(x, y, 4);
                }
            }

            AddEntity(testEntity);

        }
        public override void Update()
        {
            base.Update();
        }
    }
}
