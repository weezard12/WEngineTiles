using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Project
{
    internal class TilesEditorProject : TilesProject
    {
        public TilesEditorProject(string name) : base(name)
        {
        }
        public List<(int, string)> GetTilesNames()
        {
            string tilesNamesPath = ProjectManager.GetFileInProject(Name, "Editor\\tiles_names.json");
            if (!System.IO.File.Exists(tilesNamesPath))
            {
                return new List<(int, string)>();
            }

            return JsonSerializer.Deserialize<List<(int, string)>>(tilesNamesPath);
        }
    }
}
