using Gum.Wireframe;
using MonoGameGum;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEngine.Scripts.Main.Game1;

namespace WEngine.Scripts.GameLogic.Gum
{
    internal class GumCanvas : RenderableComponent, IUpdatable
    {
        public GraphicalUiElement Screen { get; private set; }

        public override float Width => Screen.Width;
        public override float Height => Screen.Height;

        public GumCanvas(string screenName)
        {
            GraphicalUiElement screenRuntime = LoadedGumProject.Screens.Find(s => s.Name == screenName).ToGraphicalUiElement();
            Screen = screenRuntime;
        }

        public override void Render(Batcher batcher, Camera camera)
        {
            
        }

        public void Update()
        {
            
        }

        public void LoadUI(string fileName)
        {

        }


        public override void OnRemovedFromEntity()
        {
            base.OnRemovedFromEntity();

            Screen?.RemoveFromRoot();

            Debug.Log("GumCanvas removed from entity: " + Entity.Name + ", Screen: " + Screen?.Name);
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            Screen?.AddToRoot();

            Debug.Log("GumCanvas added to entity: " + Entity.Name + ", Screen: " + Screen?.Name);
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            Screen?.AddToRoot();

            Debug.Log("GumCanvas enabled for entity: " + Entity.Name + ", Screen: " + Screen?.Name);
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Screen?.RemoveFromRoot();

            Debug.Log("GumCanvas disabled for entity: " + Entity.Name + ", Screen: " + Screen?.Name);
        }

    }
}
