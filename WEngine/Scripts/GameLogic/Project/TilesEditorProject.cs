using Nez.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.GameLogic.Tiles.Serializable.Editor;
using NJson = Newtonsoft.Json;

namespace WEngine.Scripts.GameLogic.Project
{
    internal class TilesEditorProject : TilesProject
    {
        public TilesEditorProject(string name) : base(name)
        {
        }

        public List<SerializableTileName> GetTilesNames()
        {
            string tilesNamesPath = ProjectManager.GetFileInProject(Name, "Editor\\tiles_names.json");
            if (!File.Exists(tilesNamesPath))
            {
                return new List<SerializableTileName>();
            }

            return JsonSerializer.Deserialize<List<SerializableTileName>>(File.ReadAllText(tilesNamesPath));
        }

        public void SetTilesNames(List<SerializableTileName> tileNames)
        {
            string tilesNamesPath = ProjectManager.GetFileInProject(Name, "Editor\\tiles_names.json");
            string json = JsonSerializer.Serialize(tileNames, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(tilesNamesPath));
            File.WriteAllText(tilesNamesPath, json);
        }

        public void SaveTiles(List<Tile> tiles)
        {
            var settings = new NJson.JsonSerializerSettings
            {
                TypeNameHandling = NJson.TypeNameHandling.All,
                Formatting = NJson.Formatting.Indented
            };

            string json = NJson.JsonConvert.SerializeObject(tiles, settings);
            File.WriteAllText(ProjectManager.GetFileInProject(Name,"tiles.json"), json);
        }

    }
}
