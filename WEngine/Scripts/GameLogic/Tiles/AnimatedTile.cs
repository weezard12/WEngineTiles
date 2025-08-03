using Nez;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class AnimatedTile : Tile
    {
        public int FrameRate = 1; // Frames per second
        public List<int> Frames = new List<int>();
        public int CurrentFrameIndex = 0;

        public IEnumerator Animate()
        {
            while (true)
            {
                yield return Coroutine.WaitForSeconds(1f / FrameRate);
                TextureId = Frames[CurrentFrameIndex % Frames.Count];
                CurrentFrameIndex++;
            }
        }
    }
}
