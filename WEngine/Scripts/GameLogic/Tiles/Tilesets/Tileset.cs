using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.Tiles.Tilesets
{
    internal class Tileset
    {
        public int Id { get; set; }

        // Tiles arranged in a 3x3 grid representing different connection patterns:
        // [0,0] [0,1] [0,2]    Top-Left    Top-Center    Top-Right
        // [1,0] [1,1] [1,2] =  Mid-Left    Mid-Center    Mid-Right
        // [2,0] [2,1] [2,2]    Bot-Left    Bot-Center    Bot-Right
        private int[,] Tiles { get; set; } = new int[3, 3];

        private HashSet<int> _tilesInSet = new HashSet<int>();

        public void SetTiles(int[,] tiles)
        {
            Tiles = tiles;
            _tilesInSet.Clear();
            foreach (var tile in tiles)
            {
                _tilesInSet.Add(tile);
            }
        }

        public bool IsTilesetContainsTile(int tileId)
        {
            return _tilesInSet.Contains(tileId);
        }

        // This method takes in a 3x3 matrix of surrounding tiles (the middle one is the current one and can be ignored) 
        // and returns the tile ID based on the surroundings.
        public int GetTileBasedOnSerroundings(int[,] serroundingTiles)
        {
            // Create a boolean pattern representing which surrounding positions contain tiles from this tileset
            bool[,] connectionPattern = new bool[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // Skip the center tile (1,1) as it's the current tile we're determining
                    if (row == 1 && col == 1)
                    {
                        connectionPattern[row, col] = false;
                        continue;
                    }

                    // Check if the surrounding tile at this position belongs to this tileset
                    connectionPattern[row, col] = IsTilesetContainsTile(serroundingTiles[row, col]);
                }
            }

            // Determine the appropriate tile based on the connection pattern
            return DetermineTileFromPattern(connectionPattern);
        }

        private int DetermineTileFromPattern(bool[,] pattern)
        {
            // Check connections in cardinal directions (up, down, left, right)
            bool hasTop = pattern[0, 1];    // Top-Center
            bool hasBottom = pattern[2, 1]; // Bottom-Center  
            bool hasLeft = pattern[1, 0];   // Mid-Left
            bool hasRight = pattern[1, 2];  // Mid-Right

            // Check diagonal connections
            bool hasTopLeft = pattern[0, 0];     // Top-Left
            bool hasTopRight = pattern[0, 2];    // Top-Right
            bool hasBottomLeft = pattern[2, 0];  // Bottom-Left
            bool hasBottomRight = pattern[2, 2]; // Bottom-Right

            // Determine tile position based on connections
            int tileRow, tileCol;

            // Determine row (vertical connections)
            if (hasTop && hasBottom)
            {
                tileRow = 1; // Middle row - connected vertically on both sides
            }
            else if (hasTop)
            {
                tileRow = 2; // Bottom row - has connection above
            }
            else if (hasBottom)
            {
                tileRow = 0; // Top row - has connection below
            }
            else
            {
                tileRow = 1; // Middle row - no vertical connections
            }

            // Determine column (horizontal connections)
            if (hasLeft && hasRight)
            {
                tileCol = 1; // Middle column - connected horizontally on both sides
            }
            else if (hasLeft)
            {
                tileCol = 2; // Right column - has connection to the left
            }
            else if (hasRight)
            {
                tileCol = 0; // Left column - has connection to the right
            }
            else
            {
                tileCol = 1; // Middle column - no horizontal connections
            }

            // Handle special cases for corners and isolated tiles
            if (!hasTop && !hasBottom && !hasLeft && !hasRight)
            {
                // No cardinal connections - check for diagonal-only connections or isolated tile
                if (hasTopLeft || hasTopRight || hasBottomLeft || hasBottomRight)
                {
                    // Has diagonal connections but no cardinal ones - still use center
                    return Tiles[1, 1];
                }
                else
                {
                    // Completely isolated - use center tile
                    return Tiles[1, 1];
                }
            }

            // Adjust for corner cases where we might need to use corner tiles
            // This happens when we have connections in perpendicular directions
            if ((hasTop || hasBottom) && (hasLeft || hasRight))
            {
                // We have both vertical and horizontal connections
                // Keep the calculated position as it represents the intersection
            }

            return Tiles[tileRow, tileCol];
        }
    }
}