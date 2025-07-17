using Nez;
using Nez.UI;

I want to add a Simple UI to my Editor with Nez UI System. Each UI part will have a name and a border around it.
Is this base class correct?

using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Main;

namespace WEngine.Scripts.GameLogic.TilesEditor.Base
{
	internal class EditorViewBase : Entity
	{
		public string ViewName { get; set; }

		public UICanvas Canvas { get; private set; }

		public EditorViewBase(string viewName)
		{
			ViewName = viewName;
		}

		public override void OnAddedToScene()
		{
			base.OnAddedToScene();

			// Create a full-screen UI canvas for this view
			Canvas = AddComponent<UICanvas>();
			Canvas.IsFullScreen = true;

			// Initialize the UI for this view
			InitializeUI(Canvas.Stage, new Container());
		}

		protected virtual void InitializeUI(Stage stage, Container rootContainer)
		{
			stage.AddElement(rootContainer);
			rootContainer.SetBackground(new PrimitiveDrawable(Color.Black)); // Semi-transparent background
		}
	}
}
