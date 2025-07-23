//Code for Controls/MenuItem (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

partial class MenuItem : MonoGameGum.Forms.Controls.MenuItem
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("Controls/MenuItem");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new MenuItem(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(MenuItem)] = template;
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(MonoGameGum.Forms.Controls.MenuItem)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("Controls/MenuItem", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public enum MenuItemCategory
    {
        Enabled,
        Highlighted,
        Selected,
        Focused,
    }

    MenuItemCategory? mMenuItemCategoryState;
    public MenuItemCategory? MenuItemCategoryState
    {
        get => mMenuItemCategoryState;
        set
        {
            mMenuItemCategoryState = value;
            var appliedDynamically = false;
            if(!appliedDynamically)
            {
                switch (value)
                {
                    case MenuItemCategory.Enabled:
                        this.Background.SetProperty("ColorCategoryState", "DarkGray");
                        break;
                    case MenuItemCategory.Highlighted:
                        this.Background.SetProperty("ColorCategoryState", "LightGray");
                        break;
                    case MenuItemCategory.Selected:
                        this.Background.SetProperty("ColorCategoryState", "Primary");
                        break;
                    case MenuItemCategory.Focused:
                        this.Background.SetProperty("ColorCategoryState", "DarkGray");
                        break;
                }
            }
        }
    }
    public NineSliceRuntime Background { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }

    public MenuItem(InteractiveGue visual) : base(visual) { }
    public MenuItem() : base(new ContainerRuntime())
    {

        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
         
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.Visual.X = 0f;
        this.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;
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
        Background = new NineSliceRuntime();
        Background.ElementSave = ObjectFinder.Self.GetStandardElement("NineSlice");
        if (Background.ElementSave != null) Background.AddStatesAndCategoriesRecursivelyToGue(Background.ElementSave);
        if (Background.ElementSave != null) Background.SetInitialState();
        Background.Name = "Background";
        TextInstance = new TextRuntime();
        TextInstance.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (TextInstance.ElementSave != null) TextInstance.AddStatesAndCategoriesRecursivelyToGue(TextInstance.ElementSave);
        if (TextInstance.ElementSave != null) TextInstance.SetInitialState();
        TextInstance.Name = "TextInstance";
    }
    protected virtual void AssignParents()
    {
        this.AddChild(Background);
        this.AddChild(TextInstance);
    }
    private void ApplyDefaultVariables()
    {
this.Background.SetProperty("ColorCategoryState", "DarkGray");
        this.Background.Height = 0f;
        this.Background.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Background.Width = 0f;
        this.Background.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Background.X = 0f;
        this.Background.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.Background.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.Background.Y = 0f;
        this.Background.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.Background.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;

this.TextInstance.SetProperty("ColorCategoryState", "White");
this.TextInstance.SetProperty("StyleCategoryState", "Normal");
        this.TextInstance.Height = 0f;
        this.TextInstance.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.TextInstance.HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.TextInstance.Text = @"Menu Item";
        this.TextInstance.VerticalAlignment = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.TextInstance.Width = 2f;
        this.TextInstance.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.TextInstance.X = 2f;
        this.TextInstance.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.TextInstance.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.TextInstance.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.TextInstance.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;

    }
    partial void CustomInitialize();
}
