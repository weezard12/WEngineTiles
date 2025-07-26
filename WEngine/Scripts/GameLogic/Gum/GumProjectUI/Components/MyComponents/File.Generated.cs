//Code for MyComponents/File (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class File : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/File");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new File(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(File)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/File", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public enum QuikcStyles
    {
        Hovered,
        Selected,
    }

    QuikcStyles? _quikcStylesState;
    public QuikcStyles? QuikcStylesState
    {
        get => _quikcStylesState;
        set
        {
            _quikcStylesState = value;
            if(value != null)
            {
                if(Visual.Categories.ContainsKey("QuikcStyles"))
                {
                    var category = Visual.Categories["QuikcStyles"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
                else
                {
                    var category = ((Gum.DataTypes.ElementSave)this.Visual.Tag).Categories.FirstOrDefault(item => item.Name == "QuikcStyles");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.Visual.ApplyState(state);
                }
            }
        }
    }
    public ContainerRuntime FileClickBounds { get; protected set; }
    public ContainerRuntime ContainerInstance { get; protected set; }
    public SpriteRuntime FileIcon { get; protected set; }
    public Label FileNameLabel { get; protected set; }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }

    public File(InteractiveGue visual) : base(visual) { }
    public File()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        FileClickBounds = this.Visual?.GetGraphicalUiElementByName("FileClickBounds") as ContainerRuntime;
        ContainerInstance = this.Visual?.GetGraphicalUiElementByName("ContainerInstance") as ContainerRuntime;
        FileIcon = this.Visual?.GetGraphicalUiElementByName("FileIcon") as SpriteRuntime;
        FileNameLabel = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"FileNameLabel");
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as ColoredRectangleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
