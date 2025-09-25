using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles.TileTypes;

namespace WEngine.Scripts.GameLogic.TilesEditor.TilesManagment
{
    internal static class EditorTilesManager
    {
        public static Dictionary<Type, TileType> TileTypes { get; private set; } = new Dictionary<Type, TileType>();
        public static void Initialize()
        {
            AddTileType(new TileType
                (
                "Tile",
                typeof(Tile),
                "Simple Tile"
                ));

            TileType animatedTileType = new TileType(
                "Animated Tile",
                typeof(AnimatedTile),
                "Tile with animated texture"
                );
            animatedTileType.AddTileProperty(nameof(AnimatedTile.FrameRate), typeof(EditorIntPropertyUI));
            animatedTileType.AddTileProperty(nameof(AnimatedTile.Frames), typeof(EditorImageListPropertyUI));

            AddTileType(animatedTileType);

        }


        private static void AddTileType(TileType tileType)
        {
            if (!TileTypes.ContainsKey(tileType.Type))
                TileTypes.Add(tileType.Type, tileType);
            else
                Debug.Error("Tile type already exists: " + tileType.Name);
        }

        public static TileType GetTileType(Tile tile)
        {
            if(TileTypes.TryGetValue(tile.GetType(), out TileType tileType))
                return tileType;

            Debug.Log("Failed to get type of tile: " + tile.ToString());
            return null;
            
        }
        public static TileType GetTileType(string tileName)
        {
            foreach (var kvp in TileTypes)
            {
                if (kvp.Value.Name.Equals(tileName, StringComparison.OrdinalIgnoreCase))
                    return kvp.Value;
            }

            Debug.Log($"Failed to get tile type by name: {tileName}");
            return null;
        }


    }
}
