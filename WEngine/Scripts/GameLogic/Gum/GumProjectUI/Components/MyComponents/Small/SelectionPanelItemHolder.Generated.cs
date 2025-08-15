//Code for MyComponents/Small/SelectionPanelItemHolder (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class SelectionPanelItemHolder : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/Small/SelectionPanelItemHolder");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new SelectionPanelItemHolder(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(SelectionPanelItemHolder)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/Small/SelectionPanelItemHolder", () => 
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
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }

    public SelectionPanelItemHolder(InteractiveGue visual) : base(visual)
    {
    }
    public SelectionPanelItemHolder()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as ColoredRectangleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
