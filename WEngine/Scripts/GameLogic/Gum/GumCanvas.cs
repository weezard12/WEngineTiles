using Gum.Wireframe;
using MonoGameGum;
using Nez;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEngine.Scripts.Main.Game1;
using Camera = Nez.Camera;
using Renderer = RenderingLibrary.Graphics.Renderer;

namespace WEngine.Scripts.GameLogic.Gum
{
    internal class GumCanvas : RenderableComponent, IUpdatable
    {
        public GraphicalUiElement GumElement { get; private set; }

        Renderer renderer;

        public override float Width => Screen.Width;
        public override float Height => Screen.Height;

        public GumCanvas(string screenName)
        {
            renderer = GumService.Default.SystemManagers.Renderer;
            Debug.Log(LoadedGumProject.Components.Count);
            foreach (var component in LoadedGumProject.Components)
            {
                Debug.Log($"Component: {component.Name} - {component.GetType()}");
            }
            GraphicalUiElement screenRuntime = LoadedGumProject.GetComponentSave(screenName).ToGraphicalUiElement();
            GumElement = screenRuntime;
            GumElement.AddToRoot();
            //renderer = GumService.Default.SystemManagers.Renderer;
            
        }

        public override void Render(Batcher batcher, Camera camera)
        {
            //GumService.Default.Draw();
           

        }

        public void Update()
        {
/*            Debug.Log(renderer.Layers.Count);
            GumBatch batch = new GumBatch();
            batch.Begin();
            batch.Draw(GumElement);
            batch.End();*/
        }

        public void LoadUI(string fileName)
        {

        }


        public override void OnRemovedFromEntity()
        {
            base.OnRemovedFromEntity();

            

            Debug.Log("GumCanvas removed from entity: " + Entity.Name);
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            Debug.Log("GumCanvas added to entity: " + Entity.Name);
        }

        public override void OnEnabled()
        {
            base.OnEnabled();

            Debug.Log("GumCanvas enabled for entity: " + Entity.Name);
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            Debug.Log("GumCanvas disabled for entity: " + Entity.Name);
        }

    }
}
