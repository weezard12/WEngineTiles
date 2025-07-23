//Code for MyComponents/EditorWindowContent (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorWindowContent : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/EditorWindowContent");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditorWindowContent(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditorWindowContent)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/EditorWindowContent", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ScrollViewer ScrollViewerInstance { get; protected set; }

    public EditorWindowContent(InteractiveGue visual) : base(visual) { }
    public EditorWindowContent() : base(new ContainerRuntime())
    {

        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;

        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        base.ReactToVisualChanged();
        ScrollViewerInstance = new ScrollViewer();
        ScrollViewerInstance.Name = "ScrollViewerInstance";
    }
    protected virtual void AssignParents()
    {
        this.AddChild(ScrollViewerInstance);
    }
    private void ApplyDefaultVariables()
    {
        this.ScrollViewerInstance.Visual.Height = 0f;
        this.ScrollViewerInstance.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.ScrollViewerInstance.Visual.Width = 0f;
        this.ScrollViewerInstance.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.ScrollViewerInstance.Visual.X = 0f;
        this.ScrollViewerInstance.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.ScrollViewerInstance.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.ScrollViewerInstance.Visual.Y = 0f;
        this.ScrollViewerInstance.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.ScrollViewerInstance.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;

    }
    partial void CustomInitialize();
}
