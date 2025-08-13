using Newtonsoft.Json;
using Nez.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.GameLogic.Tiles.Serializable;

namespace WEngine.Scripts.GameLogic.Project
{
    // This is used to save and load a tiles world. *Not editor related*
    internal class TilesProject
    {
        const string worldFileName = "World";
        public string Name { get; set; }
        public string WorldGenPath { get; set; }
        public string TilesPath { get; set; }
        public string TileSetsPath { get; set; }
        public string TexturesPath { get; set; }

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

    }
}
