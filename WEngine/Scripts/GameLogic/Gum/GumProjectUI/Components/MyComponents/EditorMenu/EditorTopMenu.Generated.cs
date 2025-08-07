//Code for MyComponents/EditorMenu/EditorTopMenu (Controls/Panel)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorTopMenu : Panel
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/EditorMenu/EditorTopMenu");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditorTopMenu(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditorTopMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/EditorMenu/EditorTopMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public FileMenuItem FileMenuItemInstance { get; protected set; }
    public EditMenuItem EditMenuItemInstance { get; protected set; }
    public ViewMenuItem ViewMenuItemInstance { get; protected set; }
    public Menu MenuInstance { get; protected set; }

    public EditorTopMenu(InteractiveGue visual) : base(visual)
    {
    }
    public EditorTopMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        FileMenuItemInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<FileMenuItem>(this.Visual,"FileMenuItemInstance");
        EditMenuItemInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<EditMenuItem>(this.Visual,"EditMenuItemInstance");
        ViewMenuItemInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ViewMenuItem>(this.Visual,"ViewMenuItemInstance");
        MenuInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Menu>(this.Visual,"MenuInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
