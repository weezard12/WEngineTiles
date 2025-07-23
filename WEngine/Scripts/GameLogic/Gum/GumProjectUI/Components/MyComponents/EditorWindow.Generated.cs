//Code for MyComponents/EditorWindow (Controls/WindowStandard)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class EditorWindow : WindowStandard
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/EditorWindow");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new EditorWindow(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(EditorWindow)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/EditorWindow", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public TextRuntime TitleText { get; protected set; }
    public ButtonClose CloseButton { get; protected set; }
    public ButtonIcon MinimizeButton { get; protected set; }

    public EditorWindow(InteractiveGue visual) : base(visual) { }
    public EditorWindow() : base(new ContainerRuntime())
    {

        this.Visual.Y = 0f;


        ApplyDefaultVariables();
        CustomInitialize();
    }
    protected override void InitializeInstances()
    {
        base.ReactToVisualChanged();
        base.InitializeInstances();
        ColoredRectangleInstance = new ColoredRectangleRuntime();
        ColoredRectangleInstance.ElementSave = ObjectFinder.Self.GetStandardElement("ColoredRectangle");
        if (ColoredRectangleInstance.ElementSave != null) ColoredRectangleInstance.AddStatesAndCategoriesRecursivelyToGue(ColoredRectangleInstance.ElementSave);
        if (ColoredRectangleInstance.ElementSave != null) ColoredRectangleInstance.SetInitialState();
        ColoredRectangleInstance.Name = "ColoredRectangleInstance";
        TitleText = new TextRuntime();
        TitleText.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (TitleText.ElementSave != null) TitleText.AddStatesAndCategoriesRecursivelyToGue(TitleText.ElementSave);
        if (TitleText.ElementSave != null) TitleText.SetInitialState();
        TitleText.Name = "TitleText";
        CloseButton = new ButtonClose();
        CloseButton.Name = "CloseButton";
        MinimizeButton = new ButtonIcon();
        MinimizeButton.Name = "MinimizeButton";
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
    }
    private void ApplyDefaultVariables()
    {











        this.ColoredRectangleInstance.Alpha = 255;
        this.ColoredRectangleInstance.Green = 121;
        this.ColoredRectangleInstance.Height = 0f;
        this.ColoredRectangleInstance.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.ColoredRectangleInstance.Red = 108;
        this.ColoredRectangleInstance.Width = 0f;
        this.ColoredRectangleInstance.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.ColoredRectangleInstance.X = 0f;
        this.ColoredRectangleInstance.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.ColoredRectangleInstance.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.ColoredRectangleInstance.Y = 0f;
        this.ColoredRectangleInstance.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.ColoredRectangleInstance.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;

        this.TitleText.Text = @"Window title";
        this.TitleText.X = 5f;
        this.TitleText.Y = 5f;

        this.CloseButton.Visual.Height = 20f;
        this.CloseButton.Visual.Width = 20f;
        this.CloseButton.Visual.X = 0f;
        this.CloseButton.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.CloseButton.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;
        this.CloseButton.Visual.Y = 2f;
        this.CloseButton.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.CloseButton.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

        this.MinimizeButton.Visual.Height = 20f;
        this.MinimizeButton.Visual.Width = 20f;
        this.MinimizeButton.Visual.X = -20f;
        this.MinimizeButton.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.MinimizeButton.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;
        this.MinimizeButton.Visual.Y = 2f;
        this.MinimizeButton.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.MinimizeButton.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

    }
    partial void CustomInitialize();
}
