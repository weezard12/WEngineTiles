//Code for MyComponents/SearchableContent (MyComponents/EditorWindowContent)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class SearchableContent : EditorWindowContent
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/SearchableContent");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new SearchableContent(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(SearchableContent)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/SearchableContent", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TextBox Search { get; protected set; }

    public SearchableContent(InteractiveGue visual) : base(visual) { }
    public SearchableContent() : base(new ContainerRuntime())
    {

        this.Visual.ChildrenLayout = global::Gum.Managers.ChildrenLayout.Regular;


        ApplyDefaultVariables();
        CustomInitialize();
    }
    protected override void InitializeInstances()
    {
        base.ReactToVisualChanged();
        base.InitializeInstances();
        Search = new TextBox();
        Search.Name = "Search";
    }
    protected override void AssignParents()
    {
        // Intentionally do not call base.AssignParents so that this class can determine the addition of order
        this.AddChild(ScrollViewerInstance);
        this.AddChild(Search);
    }
    private void ApplyDefaultVariables()
    {
        this.ScrollViewerInstance.Visual.FlipHorizontal = false;
        this.ScrollViewerInstance.Visual.Height = -25f;
        this.ScrollViewerInstance.Visual.X = 0f;
        this.ScrollViewerInstance.Visual.Y = 13f;

        Search.PlaceholderText = @"Search for file / folder";
        this.Search.Visual.X = 0f;
        this.Search.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.Search.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;
        this.Search.Visual.Y = 0f;
        this.Search.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.Search.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

    }
    partial void CustomInitialize();
}
