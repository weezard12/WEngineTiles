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

            // Setting up the user info
            TilesUserInfo tilesUserInfo = new TilesUserInfo();
            tilesUserInfo.Name = "tiles-user-info";
            AddEntity(tilesUserInfo);

            // Displays the camera position and other values.
            var displayEntity = CreateEntity("camera-display");
            displayEntity.AddComponent(new CameraInfoDisplay(this, tilesUserInfo));

            // Setting up the camera
            Camera.SetPosition(new Microsoft.Xna.Framework.Vector2(0,0));



/*            AddTexture("Assets/Tiles/Tile");
            AddTexture("Assets/Tiles/Kaftor_Grass");
            AddTexture("Assets/Tiles/Kaftor_Grass2");
            AddTexture("Assets/Tiles/Kaftor_Bush");

            AddTexture("Assets/Tiles/buttom_left");
            AddTexture("Assets/Tiles/buttom_right");
            AddTexture("Assets/Tiles/top_left");
            AddTexture("Assets/Tiles/top_right");
            AddTexture("Assets/Tiles/full");*/

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

            // Testing tilesets
            TilesChunk chunk = GetChunk(0, 0);
            TilesLayer layer = new TilesLayer();
            for (int i = 0; i < 7; i++)
            {
                layer.SetTile(0, i, i);
            }
            
            chunk.AddLayer(layer);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
