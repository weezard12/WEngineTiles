//Code for TestScreen
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class TestScreen : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("TestScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TestScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TestScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("TestScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public EditorWindow TestWindowInstance { get; protected set; }
    public TilesSelectionWindow TilesSelectionWindowInstance { get; protected set; }

    public TestScreen(InteractiveGue visual) : base(visual) { }
    public TestScreen() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        base.ReactToVisualChanged();
        TestWindowInstance = new EditorWindow();
        TestWindowInstance.Name = "TestWindowInstance";
        TilesSelectionWindowInstance = new TilesSelectionWindow();
        TilesSelectionWindowInstance.Name = "TilesSelectionWindowInstance";
    }
    protected virtual void AssignParents()
    {
        this.AddChild(TestWindowInstance);
        this.AddChild(TilesSelectionWindowInstance);
    }
    private void ApplyDefaultVariables()
    {
        this.TestWindowInstance.Visual.Height = 398f;
        this.TestWindowInstance.Visual.Width = 368f;

        this.TilesSelectionWindowInstance.Visual.X = 647f;
        this.TilesSelectionWindowInstance.Visual.Y = 4f;

    }
    partial void CustomInitialize();
}
