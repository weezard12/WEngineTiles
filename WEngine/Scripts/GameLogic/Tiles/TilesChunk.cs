using Nez;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEngine.Scripts.Scenes.Tiles.TilesWorld;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesChunk : Entity
    {
        // List of all the layers as DATA objects. Saving the tiles inside of them.
        public List<TilesLayer> Layers { get; private set; } = new List<TilesLayer>();

        public List<TilesLayerRenderer> RenderLayers { get; private set; } = new List<TilesLayerRenderer>();


        public TilesChunk(int idX, int idY) : base($"TilesChunk_{idX}_{idY}")
        {
            // sets the position of the chunk in the game world
            Transform.SetPosition(ChunkSize * idX, ChunkSize * idY);

            AddLayer(new TilesLayer());
;
/*            TilesLayer tilesLayer = GetLayers()[0];
            // For testing purposes, we will create a single layer

            for (int y = 0; y < tilesLayer.SizeX; y++)
            {
                for (int x = 0; x < tilesLayer.SizeY; x++)
                {
                    // Randomly select a tile ID for testing
                    int tileId = Random.Range(1, 3);
                    tilesLayer.SetTile(x, y, tileId);
                }
            }*/


        }


        public void AddLayer(TilesLayer layer)
        {
            Layers.Add(layer);
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
    }
}
