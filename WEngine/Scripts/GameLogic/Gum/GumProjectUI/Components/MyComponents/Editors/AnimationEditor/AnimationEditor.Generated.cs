//Code for MyComponents/Editors/AnimationEditor/AnimationEditor (Container)
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class AnimationEditor : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/Editors/AnimationEditor/AnimationEditor");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new AnimationEditor(visual);
            return visual;
        });
        global::MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(AnimationEditor)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/Editors/AnimationEditor/AnimationEditor", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public Panel FramesPanel { get; protected set; }
    public StackPanel FramesActionsPanel { get; protected set; }
    public StackPanel FPSPanel { get; protected set; }
    public SelectionPanel FramesSelectionPanel { get; protected set; }
    public ButtonStandard AddFrameButton { get; protected set; }
    public ButtonStandard DeleteFrameButton { get; protected set; }
    public Label FPSLabel { get; protected set; }
    public CustomTextBox FPSTextBox { get; protected set; }

    public AnimationEditor(InteractiveGue visual) : base(visual)
    {
    }
    public AnimationEditor()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        FramesPanel = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Panel>(this.Visual,"FramesPanel");
        FramesActionsPanel = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"FramesActionsPanel");
        FPSPanel = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"FPSPanel");
        FramesSelectionPanel = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<SelectionPanel>(this.Visual,"FramesSelectionPanel");
        AddFrameButton = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"AddFrameButton");
        DeleteFrameButton = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"DeleteFrameButton");
        FPSLabel = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<Label>(this.Visual,"FPSLabel");
        FPSTextBox = global::MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<CustomTextBox>(this.Visual,"FPSTextBox");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
