using Nez;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.Serializable;
using static WEngine.Scripts.Scenes.Tiles.TilesWorld;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesChunk : Entity
    {
        // List of all the layers as DATA objects. Saving the tiles inside of them.
        public List<TilesLayer> Layers { get; private set; } = new List<TilesLayer>();

        public List<TilesLayerRenderer> RenderLayers { get; private set; } = new List<TilesLayerRenderer>();
        
        public readonly int idX;
        public readonly int idY;

        public TilesChunk(int idX, int idY)
        {
            // sets the position of the chunk in the game world
            Transform.SetPosition(ChunkSize * idX, ChunkSize * idY);

            this.idX = idX;
            this.idY = idY;

            Name = GetChunkEntityName(idX, idY);
        }


        public void AddLayer(TilesLayer layer)
        {
            Layers.Add(layer);

            // Sets the layer id. TODO change this to actually work better
            layer.Id = Layers.Count - 1;

            AddComponent(layer);

            TilesLayerRenderer renderLayer = new TilesLayerRenderer(layer);
            RenderLayers.Add(renderLayer);
            AddComponent(renderLayer);
        }
        public TilesLayerRenderer GetLayerRenderer(int renderLayer)
        {
/*            if (index < 0 || index >= RenderLayers.Count)
                throw new IndexOutOfRangeException("Layer index is out of range.");
            return RenderLayers[index];*/
            return null;
        }

        public List<TilesLayer> GetLayers()
        {
            return Layers;
        }

        public TilesLayer GetLayer(int layerId)
        {
            foreach (var layer in Layers)
            {
                if (layer.Id == layerId)
                    return layer;
            }
            return null;
        }

        public static string GetChunkEntityName(int x, int y)
        {
            return $"TilesChunk_{x}_{y}";
        }


        #region Making this object Serializable
        public SerializableTilesChunk ToSerializable()
        {
            return ToSerializable(this);
        }
        public static SerializableTilesChunk ToSerializable(TilesChunk chunk)
        {
            SerializableTilesChunk sChunk = new SerializableTilesChunk() { IdX = chunk.idX, IdY = chunk.idY };
            foreach (var layer in chunk.Layers)
            {
                sChunk.Layers.Add(layer.ToSerializable());
            }
            return sChunk;
        }

        public static TilesChunk FromSerializable(SerializableTilesChunk sChunk)
        {
            TilesChunk chunk = new TilesChunk(sChunk.IdX, sChunk.IdY);
            foreach (var sLayer in sChunk.Layers)
            {
                chunk.Layers.Add(TilesLayer.FromSerializable(sLayer));
            }
            return chunk;
        }
        #endregion
    }
}
