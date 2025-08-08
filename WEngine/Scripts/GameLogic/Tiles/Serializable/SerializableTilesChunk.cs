using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.Tiles.Serializable
{
    internal class SerializableTilesChunk
    {
        public int IdX { get; set; }
        public int IdY { get; set; }
        public List<SerializableTilesLayer> Layers { get; set; }


    }
}
