using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.GameLogic.Tiles.Serializable.Editor;

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
            if (!System.IO.File.Exists(tilesNamesPath))
            {
                return new List<SerializableTileName>();
            }

            return JsonSerializer.Deserialize<List<SerializableTileName>>();
        }

        public void SetTilesNames(List<SerializableTileName> tileNames)
        {
            string tilesNamesPath = ProjectManager.GetFileInProject(Name, "Editor\\tiles_names.json");
            string json = JsonSerializer.Serialize(tileNames, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(tilesNamesPath));
            System.IO.File.WriteAllText(tilesNamesPath, json);
        }
    }
}
