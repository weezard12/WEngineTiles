//Code for MyComponents/WindowActionsBar (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class WindowActionsBar : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MyComponents/WindowActionsBar");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new WindowActionsBar(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(WindowActionsBar)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MyComponents/WindowActionsBar", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonClose ButtonCloseInstance { get; protected set; }
    public ButtonClose ButtonCloseInstance1 { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }

    public WindowActionsBar(InteractiveGue visual) : base(visual) { }
    public WindowActionsBar() : base(new ContainerRuntime())
    {

        this.Visual.ChildrenLayout = global::Gum.Managers.ChildrenLayout.Regular;
        this.Visual.Height = 30f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.Absolute;
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.X = 0f;
        this.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.Visual.Y = 0f;
        this.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        base.ReactToVisualChanged();
        ButtonCloseInstance = new ButtonClose();
        ButtonCloseInstance.Name = "ButtonCloseInstance";
        ButtonCloseInstance1 = new ButtonClose();
        ButtonCloseInstance1.Name = "ButtonCloseInstance1";
        TextInstance = new TextRuntime();
        TextInstance.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (TextInstance.ElementSave != null) TextInstance.AddStatesAndCategoriesRecursivelyToGue(TextInstance.ElementSave);
        if (TextInstance.ElementSave != null) TextInstance.SetInitialState();
        TextInstance.Name = "TextInstance";
    }
    protected virtual void AssignParents()
    {
        this.AddChild(ButtonCloseInstance);
        this.AddChild(ButtonCloseInstance1);
        this.AddChild(TextInstance);
    }
    private void ApplyDefaultVariables()
    {
        this.ButtonCloseInstance.Visual.Height = 30f;
        this.ButtonCloseInstance.Visual.Width = 30f;
        this.ButtonCloseInstance.Visual.X = 0f;
        this.ButtonCloseInstance.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.ButtonCloseInstance.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;
        this.ButtonCloseInstance.Visual.Y = 0f;
        this.ButtonCloseInstance.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.ButtonCloseInstance.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

        this.ButtonCloseInstance1.Visual.Height = 30f;
        this.ButtonCloseInstance1.Visual.Width = 30f;
        this.ButtonCloseInstance1.Visual.X = -37f;
        this.ButtonCloseInstance1.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.ButtonCloseInstance1.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;
        this.ButtonCloseInstance1.Visual.Y = 0f;
        this.ButtonCloseInstance1.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.ButtonCloseInstance1.Visual.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

        this.TextInstance.Text = @"Title";

    }
    partial void CustomInitialize();
}
