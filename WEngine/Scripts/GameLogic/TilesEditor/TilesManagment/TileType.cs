using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.TilesManagment
{
    internal class TileType
    {
        public string Name;
        public Type Type;
        public string Description;

        public List<EditorProperty> Properties = new List<EditorProperty>();

        public TileType(string name, Type type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public void AddTileProperty<T>(string propertryName)
        {
            Properties.Add(new EditorProperty(Type.GetProperty(propertryName)));
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"TileType: {Name ?? "Unnamed"}");
            sb.AppendLine($"  Type: {Type?.Name ?? "null"}");
            sb.AppendLine($"  Description: {Description ?? "No description"}");

            if (Properties != null && Properties.Count > 0)
            {
                sb.AppendLine($"  Properties ({Properties.Count}):");
                foreach (var property in Properties)
                {
                    sb.AppendLine($"    - {property?.ToString() ?? "null"}");
                }
            }
            else
            {
                sb.AppendLine("  Properties: None");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

