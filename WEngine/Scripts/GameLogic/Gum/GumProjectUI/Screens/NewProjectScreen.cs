using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Nez;
using RenderingLibrary.Graphics;
using System.Linq;
using WEngine.Scripts.Main;
using WEngine.Scripts.Scenes.Tiles;

partial class NewProjectScreen
{
    partial void CustomInitialize()
    {
        CreateProjectButton.Click += (s, e) => TryCreateProject(ProjectNameEntry.Text);
    }
    public void TryCreateProject(string projectName)
    {
        if (string.IsNullOrEmpty(projectName))
        {
            Debug.Error("Project name cant be empty");
            return;
        }
        ProjectManager.CreateProject(projectName);
        Core.Scene = new TilesWorldEditor();
    }

}
