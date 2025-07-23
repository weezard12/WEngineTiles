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
    public SelectFileWindow() : base(new ContainerRuntime())
    {

        this.Visual.Height = 268f;
        this.Visual.Width = 290f;


        ApplyDefaultVariables();
        CustomInitialize();
    }
    protected override void InitializeInstances()
    {
        base.ReactToVisualChanged();
        base.InitializeInstances();
        SearchableContentInstance = new SearchableContent();
        SearchableContentInstance.Name = "SearchableContentInstance";
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
        TitleBarInstance.AddChild(ColoredRectangleInstance);
        TitleBarInstance.AddChild(TitleText);
        TitleBarInstance.AddChild(CloseButton);
        TitleBarInstance.AddChild(MinimizeButton);
        InnerPanelInstance.AddChild(SearchableContentInstance);
    }
    private void ApplyDefaultVariables()
    {















        this.SearchableContentInstance.Visual.Height = -24f;
        this.SearchableContentInstance.Visual.X = 0f;
        this.SearchableContentInstance.Visual.Y = 24f;

    }
    partial void CustomInitialize();
}
