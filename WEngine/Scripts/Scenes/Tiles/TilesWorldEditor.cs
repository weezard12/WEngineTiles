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
using Newtonsoft.Json;
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
using WEngine.Scripts.GameLogic.Tiles.Serializable.Editor;
using WEngine.Scripts.GameLogic.TilesEditor;
using WEngine.Scripts.GameLogic.TilesEditor.KeyCombos;
using WEngine.Scripts.GameLogic.TilesEditor.Tools;
using WEngine.Scripts.Main;
using KeyCombo = WEngine.Scripts.GameLogic.TilesEditor.KeyCombos.KeyCombo;

namespace WEngine.Scripts.Scenes.Tiles
{
    internal class TilesWorldEditor : TilesWorld
    {
        private Dictionary<int, string> _tilesNames = new Dictionary<int, string>();

        TilesUserInfo tilesUserInfo;
        CameraInfoDisplay cameraInfoDisplay;

        EditorScreen EditorScreen; // same as EditorScreen.Instance
        TilesEditorProject project;
        public TilesWorldEditor(bool loadProject = false)
        {
            project = (TilesEditorProject) ProjectManager.CurrentProject;
            LoadTilesNames();
            if (loadProject)
            {
                LoadWorld();
            }
        }

        public override void OnStart()
        {
            base.OnStart();
            // Input Manager for shortcuts
            var _inputEntity = CreateEntity("input-manager");
            var manager = _inputEntity.AddComponent<KeyComboManager>();
            manager.AddCombo(new KeyCombo("Save", Keys.LeftControl, Keys.S), () =>
            {
                SaveWorld();
            });


            // Creates Camera Controller for the Editor.
            Entity entity = CreateEntity("camera-controller");
            entity.AddComponent(new EditorCameraController());

            // Setting up the user info (no ui)
            tilesUserInfo = new TilesUserInfo();
            tilesUserInfo.Name = "tiles-user-info";
            AddEntity(tilesUserInfo);

            // Displays the camera position and other values.
            var displayEntity = CreateEntity("camera-display");
            cameraInfoDisplay = new CameraInfoDisplay(this, tilesUserInfo);
            displayEntity.AddComponent(cameraInfoDisplay);

            // Setting up the camera
            Camera.SetPosition(new Vector2(0,0));

            // Setting up the Gum UI Elements.
            EditorScreen = new EditorScreen(this);
            EditorScreen.IsVisible = false;
            Game1.SetGumScreen(EditorScreen);

            // Setup Tools
            var toolsEntity = CreateEntity("tools-entity");
            PenTool penTool = new PenTool();
            toolsEntity.AddComponent(penTool);
            EraserTool eraserTool = new EraserTool();
            toolsEntity.AddComponent(eraserTool);


            // Testing chuncks rendering

            /*            AddChunk(0, 0);
                        AddChunk(0, 1);
                        AddChunk(1, 1);
                        AddChunk(1, 0);

                        AddChunk(0, -1);
                        AddChunk(-1, -1);
                        AddChunk(-1, 0);

                        AddChunk(-1, 1);

                        AddChunk(1, -1);

                        SaveWorld();*/

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
                EditorScreen.IsVisible = !EditorScreen.IsVisible;
                cameraInfoDisplay.SetOffset(new Vector2(0, EditorScreen.IsVisible ? 30 : 0));
            }

            if (Input.IsKeyPressed(Keys.F1))
            {
                Game1.ImGuiManager.SetEnabled(!Game1.ImGuiManager.Enabled);
            }
        }

        public TilesUserInfo GetUserInfo()
        {
            return tilesUserInfo;
        }

        #region Managing Tiles Names



        public string GetTileName(Tile tile)
        {
            return GetTileName(tile.Id);
        }
        public string GetTileName(int tileId)
        {
            if(_tilesNames.TryGetValue(tileId, out string tileName)){
                return tileName;
            }
            Debug.Error($"Tile with id {tileId} doesnt have a name.");
            return "Unnamed";
        }
        public void SetTileName(Tile tile, string name)
        {
            SetTileName(tile.Id, name); 
        }
        public void SetTileName(int tileId, string name)
        {
            _tilesNames[tileId] = name;

            project.SetTilesNames(
            _tilesNames.Select(kv => new SerializableTileName
            {
                ID = kv.Key,
                Name = kv.Value
            })
            .ToList());
        }

        protected void LoadTilesNames()
        {
            project.GetTilesNames();
            foreach (SerializableTileName tileName in project.GetTilesNames())
                _tilesNames.Add(tileName.ID, tileName.Name);
        }

        public override void SaveWorld()
        {
            base.SaveWorld();
            // The editor also save the Tiles from the rendering manager.
            // (Regular world doesnt need to save them since for it they are read only)

            List<Tile> tiles = new List<Tile>();
            foreach (var tile in RenderingManager.GetTiles())
            {
                tiles.Add(tile.tile);
            }

            project.SaveTiles(tiles);
        }
        #endregion

    }
}