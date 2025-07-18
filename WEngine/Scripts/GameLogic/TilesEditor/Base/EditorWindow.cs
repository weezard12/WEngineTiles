using Microsoft.Xna.Framework;
using Nez.BitmapFonts;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = Nez.UI.IDrawable;

namespace WEngine.Scripts.GameLogic.TilesEditor.Base
{
    public class EditorWindowStyle : WindowStyle
    {
        public ImageButtonStyle CloseButtonStyle;
        public ImageButtonStyle MinimizeButtonStyle;

        public EditorWindowStyle() : base()
        {
        }

        public EditorWindowStyle(BitmapFont titleFont, Color titleFontColor, IDrawable background)
            : base(titleFont, titleFontColor, background)
        {
        }

        public EditorWindowStyle(BitmapFont titleFont, Color titleFontColor, IDrawable background, float titleFontScale)
            : base(titleFont, titleFontColor, background, titleFontScale)
        {
        }

        public EditorWindowStyle(BitmapFont titleFont, Color titleFontColor, IDrawable background, float titleFontScaleX, float titleFontScaleY)
            : base(titleFont, titleFontColor, background, titleFontScaleX, titleFontScaleY)
        {
        }
    }

    public class EditorWindow : Window
    {
        private ImageButton closeButton;
        private ImageButton minimizeButton;
        private bool isMinimized;
        private float originalHeight;

        public EditorWindow(string title, EditorWindowStyle style) : base(title, style)
        {
            InitializeButtons(style);
            isMinimized = false;
            originalHeight = height; // Store initial height
        }

        public EditorWindow(string title, Skin skin, string styleName = null)
            : this(title, skin.Get<EditorWindowStyle>(styleName))
        { }

        private void InitializeButtons(EditorWindowStyle style)
        {
            var titleTable = GetTitleTable();
            titleTable.ClearChildren();
            titleTable.Add(GetTitleLabel()).SetPadRight(10); // Add title label with padding
            titleTable.Add().SetExpandX(); // Spacer to push buttons to the right
            minimizeButton = new ImageButton(style.MinimizeButtonStyle);
            closeButton = new ImageButton(style.CloseButtonStyle);
            titleTable.Add(minimizeButton).SetMinWidth(20).SetMinHeight(20);
            titleTable.Add(closeButton).SetMinWidth(20).SetMinHeight(20);

            // Close button removes the window
            closeButton.OnClicked += (button) => Remove();

            // Minimize button toggles window size
            minimizeButton.OnClicked += (button) =>
            {
                if (isMinimized)
                {
                    SetHeight(originalHeight);
                    isMinimized = false;
                }
                else
                {
                    originalHeight = GetHeight();
                    SetHeight(GetPadTop()); // Minimize to title bar height
                    isMinimized = true;
                }
            };
        }

        public new EditorWindow SetStyle(WindowStyle style)
        {
            base.SetStyle(style);
            if (style is EditorWindowStyle editorStyle)
            {
                closeButton.SetStyle(editorStyle.CloseButtonStyle);
                minimizeButton.SetStyle(editorStyle.MinimizeButtonStyle);
            }
            return this;
        }
    }
}
