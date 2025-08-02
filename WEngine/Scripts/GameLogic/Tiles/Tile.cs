using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles
{
    internal class Tile
    {
        public int Id { get; set; }

        public int TextureId { get; set; } = 0;

        public override string ToString()
        {
            return $"Tile [Id={Id}, TextureId={TextureId}]";
        }
    }
}
