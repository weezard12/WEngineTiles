//Code for MyComponents/TestWindow (Controls/WindowStandard)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class TestWindow : WindowStandard
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/TestWindow");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TestWindow(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TestWindow)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/TestWindow", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }

    public TestWindow(InteractiveGue visual) : base(visual) { }
    public TestWindow() : base(new ContainerRuntime())
    {



        ApplyDefaultVariables();
        CustomInitialize();
    }
    protected override void InitializeInstances()
    {
        base.ReactToVisualChanged();
        base.InitializeInstances();
    }
    protected override void AssignParents()
    {
        // Intentionally do not call base.AssignParents so that this class can determine the addition of order
        this.AddChild(Background);
        this.AddChild(InnerPanelInstance);
        this.AddChild(TitleBarInstance);
        this.AddChild(BorderTopLeftInstance);
        this.AddChild(BorderTopRightInstance);
        this.AddChild(BorderBottomLeftInstance);
        this.AddChild(BorderBottomRightInstance);
        this.AddChild(BorderTopInstance);
        this.AddChild(BorderBottomInstance);
        this.AddChild(BorderLeftInstance);
        this.AddChild(BorderRightInstance);
    }
    private void ApplyDefaultVariables()
    {











    }
    partial void CustomInitialize();
}
