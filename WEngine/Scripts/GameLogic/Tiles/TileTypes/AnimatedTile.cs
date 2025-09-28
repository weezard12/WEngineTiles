using Nez;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles.TileTypes
{
    internal class AnimatedTile : Tile
    {
        public int FrameRate { get; set; } = 1; // Frames per second
        public List<int> Frames { get; set; } = new List<int>();
        public int CurrentFrameIndex { get; set; } = 0;

        public IEnumerator Animate()
        {
            while (true)
            {
                yield return Coroutine.WaitForSeconds(1f / FrameRate);

                // In case there are no frames, skip updating the texture. (This state shouldnt happen. but when using the editor it can.)
                if (Frames.Count == 0)
                {
                    Debug.Error(ToString() + " has no frames. Animation skipped.");
                    continue;
                }
                    
                TextureId = Frames[CurrentFrameIndex % Frames.Count];
                CurrentFrameIndex++;
            }
        }
    }
}
