using Gum.Converters;
using Gum.DataTypes;
using Gum.Wireframe;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using Nez;
using Nez.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Gum;
using WEngine.Scripts.GameLogic.Project;
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
        TilesUserInfo tilesUserInfo;
        CameraInfoDisplay cameraInfoDisplay;

        public override void OnStart()
        {
            base.OnStart();

            // Getting the Gum UI Elements.
            Game1.LoadGumScreen("EditorScreen");
            tilesSelectionWindow = Game1.CurrentGumScreen.GetFrameworkElementByName<TilesSelectionWindow>("TilesSelectionWindowInstance");
            Game1.CurrentGumScreen.Visible = false;
            base.RenderingManager.FinishedLoadingAssets += () =>
            {
                // Load the tileset after the assets are loaded
                tilesSelectionWindow.LoadTiles(RenderingManager);
            };

            // Creates Camera Controller for the Editor.
            Entity entity = CreateEntity("camera-controller");
            entity.AddComponent(new EditorCameraController());

            // Setting up the user info
            tilesUserInfo = new TilesUserInfo();
            tilesUserInfo.Name = "tiles-user-info";
            AddEntity(tilesUserInfo);

            // Displays the camera position and other values.
            var displayEntity = CreateEntity("camera-display");
            cameraInfoDisplay = new CameraInfoDisplay(this, tilesUserInfo);
            displayEntity.AddComponent(cameraInfoDisplay);

            // Setting up the camera
            Camera.SetPosition(new Vector2(0,0));


            // Testing chuncks rendering

            AddChunk(0, 0);
            AddChunk(0, 1);
            AddChunk(1, 1);
            AddChunk(1, 0);

            AddChunk(0, -1);
            AddChunk(-1, -1);
            AddChunk(-1, 0);

            AddChunk(-1, 1);

            AddChunk(1, -1);

            SaveWorld();

            // Testing tilesets
/*            TilesChunk chunk = GetChunk(0, 0);
            TilesLayer layer = chunk.GetLayer(0);

            for (int i = 0; i < 5; i++)
            {
                layer.SetTile(1, i + 1, i + 5);
            }
            for (int i = 0; i < 5; i++)
            {
                layer.SetTile(2, i + 1, i + 5);
            }
            for (int i = 0; i < 5; i++)
            {
                layer.SetTile(3, i + 1, i + 5);
            }

            layer.SetTile(7, 7, 10);
            layer.SetTile(7, 6, 10);
            layer.SetTile(7, 5, 10);*/

        }
        public override void Update()
        {
            base.Update();

            // 1 toggles all Editor gum ui
            if(Input.IsKeyPressed(Keys.D1))
            {
                Game1.CurrentGumScreen.Visible = !Game1.CurrentGumScreen.Visible;
                cameraInfoDisplay.SetOffset(new Vector2(0, Game1.CurrentGumScreen.Visible ? 30 : 0));
            }
        }

    }
}
