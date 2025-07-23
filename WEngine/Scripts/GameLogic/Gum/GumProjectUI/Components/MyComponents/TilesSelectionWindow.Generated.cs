//Code for MyComponents/TilesSelectionWindow (MyComponents/EditorWindow)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class TilesSelectionWindow : EditorWindow
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/TilesSelectionWindow");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TilesSelectionWindow(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TilesSelectionWindow)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/TilesSelectionWindow", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonStandard ImportTilesetButton { get; protected set; }
    public EditorWindowContent EditorWindowContentInstance { get; protected set; }

    public TilesSelectionWindow(InteractiveGue visual) : base(visual) { }
    public TilesSelectionWindow() : base(new ContainerRuntime())
    {



        ApplyDefaultVariables();
        CustomInitialize();
    }
    protected override void InitializeInstances()
    {
        base.ReactToVisualChanged();
        base.InitializeInstances();
        ImportTilesetButton = new ButtonStandard();
        ImportTilesetButton.Name = "ImportTilesetButton";
        EditorWindowContentInstance = new EditorWindowContent();
        EditorWindowContentInstance.Name = "EditorWindowContentInstance";
    }
    protected override void AssignParents()
    {
        // Intentionally do not call base.AssignParents so that this class can determine the addition of order
        this.AddChild(Background);
        this.AddChild(InnerPanelInstance);
        this.AddChild(TitleBarInstance);
        this.AddChild(BorderTopLeftInstance);
        this.AddChild(BorderTopRightInstance);
        this.AddChild(BorderBottomLeftInstance);
        this.AddChild(BorderBottomRightInstance);
        this.AddChild(BorderTopInstance);
        this.AddChild(BorderBottomInstance);
        this.AddChild(BorderLeftInstance);
        this.AddChild(BorderRightInstance);
        TitleBarInstance.AddChild(ColoredRectangleInstance);
        TitleBarInstance.AddChild(TitleText);
        TitleBarInstance.AddChild(CloseButton);
        TitleBarInstance.AddChild(MinimizeButton);
        InnerPanelInstance.AddChild(ImportTilesetButton);
        InnerPanelInstance.AddChild(EditorWindowContentInstance);
    }
    private void ApplyDefaultVariables()
    {

        this.InnerPanelInstance.Visual.ChildrenLayout = global::Gum.Managers.ChildrenLayout.TopToBottomStack;











        this.TitleText.Text = @"Tiles Selection";



        ImportTilesetButton.ButtonDisplayText = @"Import Tileset";
        this.ImportTilesetButton.Visual.Height = 19f;
        this.ImportTilesetButton.Visual.IgnoredByParentSize = true;
        this.ImportTilesetButton.Visual.Width = 0f;
        this.ImportTilesetButton.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.ImportTilesetButton.Visual.X = 0f;
        this.ImportTilesetButton.Visual.Y = 25f;

        this.EditorWindowContentInstance.Visual.Height = -46f;
        this.EditorWindowContentInstance.Visual.Width = 0f;
        this.EditorWindowContentInstance.Visual.X = 0f;
        this.EditorWindowContentInstance.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.EditorWindowContentInstance.Visual.Y = 0f;
        this.EditorWindowContentInstance.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;

    }
    partial void CustomInitialize();
}
