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
        public List<Type> PropertiesUI = new List<Type>();


        public TileType(string name, Type type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public void AddTileProperty(string propertryName, Type uiType)
        {
            Properties.Add(new EditorProperty(Type.GetProperty(propertryName)));
            PropertiesUI.Add(uiType);
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

        internal EditorPropertyUI ShowUI(int idx, object target)
        {
            EditorPropertyUI propertyUI = (EditorPropertyUI) Activator.CreateInstance(PropertiesUI[idx]);
            propertyUI.Setup(target, Properties[idx]);

            return propertyUI;

        }
    }
}

