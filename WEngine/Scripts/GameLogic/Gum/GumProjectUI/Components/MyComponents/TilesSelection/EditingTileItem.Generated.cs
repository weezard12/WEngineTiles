//Code for MyComponents/TilesSelection/EditingTileItem (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditingTileItem : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/TilesSelection/EditingTileItem");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditingTileItem(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditingTileItem)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/TilesSelection/EditingTileItem", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public Panel TopPanel { get; protected set; }
    public MenuItem TileTypeMenu { get; protected set; }
    public Panel InnerPanel { get; protected set; }
    public Panel ButtomPanel { get; protected set; }
    public ButtonStandard ConfirmButton { get; protected set; }
    public ButtonStandard CloseButton { get; protected set; }

    public EditingTileItem(InteractiveGue visual) : base(visual)
    {
    }
    public EditingTileItem()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TopPanel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"TopPanel");
        TileTypeMenu = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<MenuItem>(this.Visual,"TileTypeMenu");
        InnerPanel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"InnerPanel");
        ButtomPanel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"ButtomPanel");
        ConfirmButton = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"ConfirmButton");
        CloseButton = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"CloseButton");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
