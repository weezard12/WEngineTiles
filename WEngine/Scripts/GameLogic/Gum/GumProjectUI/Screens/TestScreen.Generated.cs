//Code for TestScreen
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class TestScreen : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("TestScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TestScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TestScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("TestScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ContainerRuntime ContainerInstance { get; protected set; }
    public Panel PanelInstance { get; protected set; }
    public Panel PanelInstance1 { get; protected set; }
    public Panel PanelInstance2 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance1 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance2 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance3 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance4 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance5 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance6 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance7 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance8 { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance9 { get; protected set; }
    public StackPanel StackPanelInstance { get; protected set; }
    public ScrollViewer ScrollViewerInstance { get; protected set; }

    public TestScreen(InteractiveGue visual) : base(visual)
    {
    }
    public TestScreen()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ContainerInstance = this.Visual?.GetGraphicalUiElementByName("ContainerInstance") as ContainerRuntime;
        PanelInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"PanelInstance");
        PanelInstance1 = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"PanelInstance1");
        PanelInstance2 = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"PanelInstance2");
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as ColoredRectangleRuntime;
        ColoredRectangleInstance1 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance1") as ColoredRectangleRuntime;
        ColoredRectangleInstance2 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance2") as ColoredRectangleRuntime;
        ColoredRectangleInstance3 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance3") as ColoredRectangleRuntime;
        ColoredRectangleInstance4 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance4") as ColoredRectangleRuntime;
        ColoredRectangleInstance5 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance5") as ColoredRectangleRuntime;
        ColoredRectangleInstance6 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance6") as ColoredRectangleRuntime;
        ColoredRectangleInstance7 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance7") as ColoredRectangleRuntime;
        ColoredRectangleInstance8 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance8") as ColoredRectangleRuntime;
        ColoredRectangleInstance9 = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance9") as ColoredRectangleRuntime;
        StackPanelInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"StackPanelInstance");
        ScrollViewerInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ScrollViewer>(this.Visual,"ScrollViewerInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
