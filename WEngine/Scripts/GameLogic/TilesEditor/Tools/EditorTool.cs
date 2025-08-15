using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.Tools
{
    internal abstract class EditorTool : Component
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Sprite { get; set; }
        public EditorTool()
        {
            Setup();
        }
        public abstract void Setup();
        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            EditorScreen.Instance.ToolSelectionWindowInstance.ToolsPanel.AddChild(new ToolUI(this));
        }
    }
}
