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
    public ColoredRectangleRuntime Background { get; protected set; }
    public ContainerRuntime TopBar { get; protected set; }
    public Panel PanelInstance { get; protected set; }
    public TopBarCategory TopBarCategoryInstance { get; protected set; }
    public TopBarCategory TopBarCategoryInstance1 { get; protected set; }
    public TopBarCategory TopBarCategoryInstance2 { get; protected set; }
    public ButtonClose ButtonCloseInstance { get; protected set; }

    public EditorScreen(InteractiveGue visual) : base(visual)
    {
    }
    public EditorScreen()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TilesSelectionWindowInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TilesSelectionWindow>(this.Visual,"TilesSelectionWindowInstance");
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as ColoredRectangleRuntime;
        TopBar = this.Visual?.GetGraphicalUiElementByName("TopBar") as ContainerRuntime;
        PanelInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"PanelInstance");
        TopBarCategoryInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TopBarCategory>(this.Visual,"TopBarCategoryInstance");
        TopBarCategoryInstance1 = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TopBarCategory>(this.Visual,"TopBarCategoryInstance1");
        TopBarCategoryInstance2 = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TopBarCategory>(this.Visual,"TopBarCategoryInstance2");
        ButtonCloseInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonClose>(this.Visual,"ButtonCloseInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
