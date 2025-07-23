//Code for EditorScreen
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorScreen : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("EditorScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditorScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditorScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("EditorScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TilesSelectionWindow TilesSelectionWindowInstance { get; protected set; }

    public EditorScreen(InteractiveGue visual) : base(visual) { }
    public EditorScreen() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        base.ReactToVisualChanged();
        TilesSelectionWindowInstance = new TilesSelectionWindow();
        TilesSelectionWindowInstance.Name = "TilesSelectionWindowInstance";
    }
    protected virtual void AssignParents()
    {
        this.AddChild(TilesSelectionWindowInstance);
    }
    private void ApplyDefaultVariables()
    {
        this.TilesSelectionWindowInstance.Visual.Y = 0f;
        this.TilesSelectionWindowInstance.Visual.X = 0f;
        this.TilesSelectionWindowInstance.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.TilesSelectionWindowInstance.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;
        this.TilesSelectionWindowInstance.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.TilesSelectionWindowInstance.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

    }
    partial void CustomInitialize();
}
