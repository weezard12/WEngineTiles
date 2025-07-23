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
    public SearchableContent()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        Search = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TextBox>(this.Visual,"Search");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
