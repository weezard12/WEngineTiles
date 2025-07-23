//Code for MyComponents/SelectFileWindow (MyComponents/EditorWindow)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class SelectFileWindow : EditorWindow
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/SelectFileWindow");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new SelectFileWindow(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(SelectFileWindow)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/SelectFileWindow", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public SearchableContent SearchableContentInstance { get; protected set; }

    public SelectFileWindow(InteractiveGue visual) : base(visual) { }
    public SelectFileWindow()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        SearchableContentInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<SearchableContent>(this.Visual,"SearchableContentInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
