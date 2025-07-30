using Nez;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class TilesChunk : Entity
    {
        List<TilesLayerRenderer> Layers { get; set; }

        public TilesChunk(int idX, int idY) : base($"TilesChunk_{idX}_{idY}")
        {
            Layers = new List<TilesLayerRenderer>();

            // For testing purposes, we will create a single layer
            TilesLayerRenderer layerRenderer = new TilesLayerRenderer(16, 16);

            for (int y = 0; y < TilesLayerRenderer.SizeX; y++)
            {
                for (int x = 0; x < TilesLayerRenderer.SizeY; x++)
                {
                    // Randomly select a tile ID for testing
                    int tileId = Random.Range(2, 4);
                    layerRenderer.SetTile(x, y, tileId);
                }
            }

            AddComponent(layerRenderer);
            Layers.Add(layerRenderer);
        }

    }
}
