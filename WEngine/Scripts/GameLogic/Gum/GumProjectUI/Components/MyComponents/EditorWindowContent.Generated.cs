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
    public EditorWindowContent()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ScrollViewerInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ScrollViewer>(this.Visual,"ScrollViewerInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
