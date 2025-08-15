//Code for MyComponents/TilesSelection/TileItem (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class TileItem : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/TilesSelection/TileItem");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TileItem(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TileItem)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/TilesSelection/TileItem", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public enum QuickStyles
    {
        Hovered,
        Selected,
        Clear,
    }

    QuickStyles? _quickStylesState;
    public QuickStyles? QuickStylesState
    {
        get => _quickStylesState;
        set
        {
            _quickStylesState = value;
            if(value != null)
            {
                if(Visual.Categories.ContainsKey("QuickStyles"))
                {
                    var category = Visual.Categories["QuickStyles"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
                else
                {
                    var category = ((Gum.DataTypes.ElementSave)this.Visual.Tag).Categories.FirstOrDefault(item => item.Name == "QuickStyles");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
            }
        }
    }
    public SpriteRuntime TileSprite { get; protected set; }
    public CustomTextBox TileName { get; protected set; }
    public Label TileType { get; protected set; }
    public ContainerRuntime SpriteContainer { get; protected set; }
    public Panel PanelInstance { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public ContainerRuntime ContainerInstance { get; protected set; }

    public TileItem(InteractiveGue visual) : base(visual)
    {
    }
    public TileItem()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TileSprite = this.Visual?.GetGraphicalUiElementByName("TileSprite") as SpriteRuntime;
        TileName = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<CustomTextBox>(this.Visual,"TileName");
        TileType = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"TileType");
        SpriteContainer = this.Visual?.GetGraphicalUiElementByName("SpriteContainer") as ContainerRuntime;
        PanelInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"PanelInstance");
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as ColoredRectangleRuntime;
        ContainerInstance = this.Visual?.GetGraphicalUiElementByName("ContainerInstance") as ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
