using Newtonsoft.Json;
using Nez.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;
using NJson = Newtonsoft.Json;

namespace WEngine.Scripts.GameLogic.Project
{
    // This is used to save and load a tiles world. *Not editor related*
    /// <summary>
    /// Handles all files operations. Separates the logic of creating, reading, modifying... files from the Tiles World.
    /// (All file operations in must be in here)
    /// </summary>
    internal class TilesProject
    {
        const string worldFileName = "World";
        public string Name { get; set; }
        public string WorldGenPath { get; set; }
        public string TilesPath { get; set; }
        public string TileSetsPath { get; set; }
        public string TexturesPath { get; set; }

        public TilesProject(string name)
        {
            this.Name = name;
            TilesPath = ProjectManager.GetFileInProject(name, "tiles.json");
        }

        public void SaveWorldJson(SerializableTilesWorld serializableTilesWorld)
        {
            // saving each chunk in a seperate file
            foreach (var chunk in serializableTilesWorld.Chunks)
            {
                string jsonFile = JsonConvert.SerializeObject(chunk);
                ProjectManager.WriteFileInProject(Name, Path.Combine(worldFileName, $"{chunk.IdX}_{chunk.IdY}.json"), jsonFile);
            }
        }

        internal List<SerializableTilesChunk> LoadChunks()
        {
            List<SerializableTilesChunk> sChunks = new List<SerializableTilesChunk>();

            string[] chunks = ProjectManager.GetFilesInProject(Name, worldFileName);

            foreach (var chunk in chunks)
                sChunks.Add(JsonConvert.DeserializeObject<SerializableTilesChunk>(System.IO.File.ReadAllText(chunk)));
            
            return sChunks;
        }

        public List<Tile> LoadTiles()
        {
            if(!File.Exists(TilesPath))
                return new List<Tile>();
            

            string json = File.ReadAllText(TilesPath);
            var settings = new NJson.JsonSerializerSettings
            {
                TypeNameHandling = NJson.TypeNameHandling.All,
                Formatting = NJson.Formatting.Indented
            };

            return NJson.JsonConvert.DeserializeObject<List<Tile>>(json, settings);
        }
    }
}
