using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using Nez;
using System.Collections.Generic;
using WEngine.Scripts.GameLogic.Project;
using WEngine.Scripts.Main;
using WEngine.Scripts.Scenes.Tiles;

namespace WEngine.Scripts.Scenes
{
    internal class EditorHomeScene : Scene
    {
        GumService GumService => GumService.Default;
        private Panel mainPanel;
        private StackPanel projectListPanel;
        private List<string> projectNames; // Mock list of project names for demo

        List<StackPanel> stackPanels = new List<StackPanel>();

        public override void OnStart()
        {
            base.OnStart();


            // Adjust camera for UI visibility
            Camera.Zoom = 1f;
            GraphicalUiElement.CanvasWidth = Screen.PreferredBackBufferWidth;
            GraphicalUiElement.CanvasHeight = Screen.PreferredBackBufferHeight;

            SetupEditorHomeScreen();
        }

        private void SetupEditorHomeScreen()
        {
            // Clear any existing UI elements
            GumService.Root.Children.Clear();

            projectNames = ProjectManager.GetAllProjectsNames();

            // Create main panel to hold UI elements
            mainPanel = new Panel();
            mainPanel.AddToRoot();
            mainPanel.Anchor(Anchor.TopLeft);
            mainPanel.Visual.WidthUnits = DimensionUnitType.Percentage;
            mainPanel.Visual.HeightUnits = DimensionUnitType.Percentage;
            mainPanel.Width = 100; // Full width
            mainPanel.Height = 100; // Full height
            mainPanel.Visual.ChildrenLayout = ChildrenLayout.TopToBottomStack;

            // Create New Project button at the top
            var newProjectButton = new Button();
            newProjectButton.Text = "New Project";
            newProjectButton.Visual.Width = 200;
            newProjectButton.Visual.Height = 50;
            newProjectButton.Visual.XUnits = Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            newProjectButton.Visual.XOrigin = RenderingLibrary.Graphics.HorizontalAlignment.Center;
            newProjectButton.Visual.Y = 20;
            newProjectButton.Click += (sender, args) =>
            {
                Game1.LoadGumScreen("NewProjectScreen");
                Debug.Log("New Project button clicked");
                // Add logic to create a new project
            };
            mainPanel.AddChild(newProjectButton);

            // Create project list panel on the left
            projectListPanel = new StackPanel();
            projectListPanel.Visual.Width = 300;
            projectListPanel.Visual.HeightUnits = DimensionUnitType.Percentage;
            projectListPanel.Height = 80;
            projectListPanel.Visual.Y = 80;
            projectListPanel.Spacing = 5;
            projectListPanel.Anchor(Anchor.TopLeft);
            mainPanel.AddChild(projectListPanel);

            // Populate project list
            UpdateProjectList();
        }

        private void UpdateProjectList()
        {
            // Initialize mock project list (replace with actual project data in a real application)
            projectNames = ProjectManager.GetAllProjectsNames();

            foreach (var item in stackPanels)
            {
                item.RemoveFromRoot();
            }
            foreach (var projectName in projectNames)
            {
                var projectContainer = new StackPanel();
                projectContainer.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
                projectContainer.Visual.WidthUnits = DimensionUnitType.RelativeToChildren;
                projectContainer.Visual.HeightUnits = DimensionUnitType.RelativeToChildren;
                projectContainer.Spacing = 5;

                // Project name label
                var projectLabel = new Label();
                projectLabel.Text = projectName;
                projectLabel.Visual.Width = 100;
                projectContainer.AddChild(projectLabel);

                // Open button
                var openButton = new Button();
                openButton.Text = "Open";
                openButton.Visual.Width = 60;
                openButton.Click += (sender, args) =>
                {
                    Debug.Log($"Opening project: {projectName}");

                    ProjectManager.SetCurrentProject(projectName);
                    Core.Scene = new TilesWorldEditor(true);
                };
                projectContainer.AddChild(openButton);

                // Rename button
                var renameButton = new Button();
                renameButton.Text = "Rename";
                renameButton.Visual.Width = 80;
                renameButton.Click += (sender, args) =>
                {
                    Debug.Log($"Renaming project: {projectName}");
                    // Add logic to rename the project (e.g., prompt for new name)
                };
                projectContainer.AddChild(renameButton);

                // Delete button
                var deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.Visual.Width = 60;
                deleteButton.Click += (sender, args) =>
                {
                    Debug.Log($"Deleting project: {projectName}");
                    ProjectManager.DeleteProject(projectName);
                    UpdateProjectList(); // Refresh the list
                };
                projectContainer.AddChild(deleteButton);

                stackPanels.Add(projectContainer);
                projectListPanel.AddChild(projectContainer);
            }
        }

    }
}