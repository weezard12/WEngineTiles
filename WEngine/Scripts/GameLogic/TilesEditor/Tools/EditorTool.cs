using Nez;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.GameLogic.Tiles;
using WEngine.Scripts.Main;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.GameLogic.TilesEditor.Tools
{
    internal abstract class EditorTool : Component, IUpdatable
    {
        protected TilesWorldEditor WorldEditor => (TilesWorldEditor) Entity.Scene;
        public TilesUserInfo UserInfo => WorldEditor.GetUserInfo();

        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Sprite { get; set; }
        public EditorTool()
        {
            Setup();
            SetEnabled(false);
        }
        public abstract void Setup();
        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            EditorScreen.Instance.ToolSelectionWindowInstance.ToolsPanel.AddItem(new ToolUI(this));
            
        }

        public void Update()
        {
            if(Input.LeftMouseButtonDown)
            {
                if(!Game1.IsCursorOverGum)
                {
                    UseTool();
                }
                
            }
        }
        protected abstract void UseTool();
    }
}
