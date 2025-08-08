using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.Tiles.Serializable
{
    internal class SerializableTilesWorld
    {
        public List<SerializableTilesChunk> Chunks = new List<SerializableTilesChunk>();
    }
}
