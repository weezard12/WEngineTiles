using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;

namespace WEngine.Scripts.Main.Utils
{
    internal static class SpriteSheetUtils
    {
        /// <summary>
        /// Creates a Sprite from a sprite sheet texture at the specified grid coordinates
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="x">Grid X coordinate (0-based)</param>
        /// <param name="y">Grid Y coordinate (0-based)</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <returns>A Nez Sprite object</returns>
        public static Sprite GetSprite(Texture2D texture, int x, int y, int spriteWidth, int spriteHeight)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (x < 0 || y < 0)
                throw new ArgumentException("Grid coordinates must be non-negative");

            if (spriteWidth <= 0 || spriteHeight <= 0)
                throw new ArgumentException("Sprite dimensions must be positive");

            // Calculate pixel coordinates
            int pixelX = x * spriteWidth;
            int pixelY = y * spriteHeight;

            // Validate bounds
            if (pixelX + spriteWidth > texture.Width || pixelY + spriteHeight > texture.Height)
                throw new ArgumentException($"Sprite at grid position ({x}, {y}) exceeds texture bounds");

            // Create source rectangle
            Rectangle sourceRect = new Rectangle(pixelX, pixelY, spriteWidth, spriteHeight);

            return new Sprite(texture, sourceRect);
        }

        /// <summary>
        /// Creates a Sprite from a sprite sheet texture using pixel coordinates
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="sourceRect">Source rectangle in pixel coordinates</param>
        /// <returns>A Nez Sprite object</returns>
        public static Sprite GetSprite(Texture2D texture, Rectangle sourceRect)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (sourceRect.X < 0 || sourceRect.Y < 0 ||
                sourceRect.X + sourceRect.Width > texture.Width ||
                sourceRect.Y + sourceRect.Height > texture.Height)
                throw new ArgumentException("Source rectangle exceeds texture bounds");

            return new Sprite(texture, sourceRect);
        }

        /// <summary>
        /// Creates multiple sprites from a sprite sheet in a grid pattern
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <param name="startX">Starting grid X coordinate (default: 0)</param>
        /// <param name="startY">Starting grid Y coordinate (default: 0)</param>
        /// <param name="countX">Number of sprites to extract horizontally (default: all)</param>
        /// <param name="countY">Number of sprites to extract vertically (default: all)</param>
        /// <returns>A 2D array of Sprite objects [y][x]</returns>
        public static Sprite[,] GetSprites(Texture2D texture, int spriteWidth, int spriteHeight,
            int startX = 0, int startY = 0, int? countX = null, int? countY = null)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (spriteWidth <= 0 || spriteHeight <= 0)
                throw new ArgumentException("Sprite dimensions must be positive");

            // Calculate grid dimensions
            int maxGridWidth = (texture.Width - startX * spriteWidth) / spriteWidth;
            int maxGridHeight = (texture.Height - startY * spriteHeight) / spriteHeight;

            int gridWidth = countX ?? maxGridWidth;
            int gridHeight = countY ?? maxGridHeight;

            if (gridWidth <= 0 || gridHeight <= 0)
                throw new ArgumentException("Invalid grid dimensions calculated");

            Sprite[,] sprites = new Sprite[gridHeight, gridWidth];

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    sprites[y, x] = GetSprite(texture, startX + x, startY + y, spriteWidth, spriteHeight);
                }
            }

            return sprites;
        }

        /// <summary>
        /// Creates a list of sprites from a sprite sheet row
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="row">The row index (0-based)</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <param name="startColumn">Starting column (default: 0)</param>
        /// <param name="count">Number of sprites to extract (default: all in row)</param>
        /// <returns>A list of Sprite objects</returns>
        public static List<Sprite> GetSpriteRow(Texture2D texture, int row, int spriteWidth, int spriteHeight,
            int startColumn = 0, int? count = null)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            int maxSprites = (texture.Width - startColumn * spriteWidth) / spriteWidth;
            int spritesToExtract = count ?? maxSprites;

            List<Sprite> sprites = new List<Sprite>();

            for (int x = startColumn; x < startColumn + spritesToExtract; x++)
            {
                sprites.Add(GetSprite(texture, x, row, spriteWidth, spriteHeight));
            }

            return sprites;
        }

        /// <summary>
        /// Creates a list of sprites from a sprite sheet column
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="column">The column index (0-based)</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <param name="startRow">Starting row (default: 0)</param>
        /// <param name="count">Number of sprites to extract (default: all in column)</param>
        /// <returns>A list of Sprite objects</returns>
        public static List<Sprite> GetSpriteColumn(Texture2D texture, int column, int spriteWidth, int spriteHeight,
            int startRow = 0, int? count = null)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            int maxSprites = (texture.Height - startRow * spriteHeight) / spriteHeight;
            int spritesToExtract = count ?? maxSprites;

            List<Sprite> sprites = new List<Sprite>();

            for (int y = startRow; y < startRow + spritesToExtract; y++)
            {
                sprites.Add(GetSprite(texture, column, y, spriteWidth, spriteHeight));
            }

            return sprites;
        }

        /// <summary>
        /// Creates sprites from a sprite sheet using a linear index (left-to-right, top-to-bottom)
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="index">Linear index of the sprite</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <returns>A Nez Sprite object</returns>
        public static Sprite GetSpriteByIndex(Texture2D texture, int index, int spriteWidth, int spriteHeight)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (index < 0)
                throw new ArgumentException("Index must be non-negative");

            int spritesPerRow = texture.Width / spriteWidth;
            int x = index % spritesPerRow;
            int y = index / spritesPerRow;

            return GetSprite(texture, x, y, spriteWidth, spriteHeight);
        }

        /// <summary>
        /// Gets the grid dimensions of a sprite sheet
        /// </summary>
        /// <param name="texture">The sprite sheet texture</param>
        /// <param name="spriteWidth">Width of each sprite in pixels</param>
        /// <param name="spriteHeight">Height of each sprite in pixels</param>
        /// <returns>A tuple containing (columns, rows)</returns>
        public static (int columns, int rows) GetGridDimensions(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            if (spriteWidth <= 0 || spriteHeight <= 0)
                throw new ArgumentException("Sprite dimensions must be positive");

            int columns = texture.Width / spriteWidth;
            int rows = texture.Height / spriteHeight;

            return (columns, rows);
        }
    }
}