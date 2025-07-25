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
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public TextRuntime TitleText { get; protected set; }
    public ButtonClose CloseButton { get; protected set; }
    public ButtonIcon MinimizeButton { get; protected set; }

    public TestWindow(InteractiveGue visual) : base(visual) { }
    public TestWindow() : base(new ContainerRuntime())
    {

        this.Visual.Y = 0f;


        ApplyDefaultVariables();
        CustomInitialize();
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
