//Code for MyComponents/EditorWindow (Controls/WindowStandard)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorWindow : WindowStandard
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/EditorWindow");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditorWindow(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditorWindow)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/EditorWindow", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public TextRuntime TitleText { get; protected set; }
    public ButtonClose CloseButton { get; protected set; }
    public ButtonIcon MinimizeButton { get; protected set; }

    public EditorWindow(InteractiveGue visual) : base(visual) { }
    public EditorWindow()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as ColoredRectangleRuntime;
        TitleText = this.Visual?.GetGraphicalUiElementByName("TitleText") as TextRuntime;
        CloseButton = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonClose>(this.Visual,"CloseButton");
        MinimizeButton = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonIcon>(this.Visual,"MinimizeButton");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
