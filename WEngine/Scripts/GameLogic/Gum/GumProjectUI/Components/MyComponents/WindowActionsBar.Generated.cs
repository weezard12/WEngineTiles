//Code for MyComponents/WindowActionsBar (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class WindowActionsBar : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/WindowActionsBar");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new WindowActionsBar(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(WindowActionsBar)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/WindowActionsBar", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonClose ButtonCloseInstance { get; protected set; }
    public ButtonClose ButtonCloseInstance1 { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }

    public WindowActionsBar(InteractiveGue visual) : base(visual) { }
    public WindowActionsBar()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ButtonCloseInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonClose>(this.Visual,"ButtonCloseInstance");
        ButtonCloseInstance1 = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonClose>(this.Visual,"ButtonCloseInstance1");
        TextInstance = this.Visual?.GetGraphicalUiElementByName("TextInstance") as TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
