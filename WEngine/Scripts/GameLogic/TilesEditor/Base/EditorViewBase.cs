using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEngine.Scripts.Main;
using IDrawable = Nez.UI.IDrawable;

namespace WEngine.Scripts.GameLogic.TilesEditor.Base
{
    internal class EditorViewBase : Entity
    {
        public string ViewName { get; set; }

        public UICanvas Canvas { get; private set; }
        protected Window UIWindow { get; private set; }

        public EditorViewBase(string viewName)
        {
            ViewName = viewName;

            Name = viewName;
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            // Create a full-screen UI canvas for this view
            Canvas = AddComponent<UICanvas>();
            Canvas.IsFullScreen = true;
            Canvas.DebugRenderEnabled = true; // Enable debug rendering for the canvas

            Sprite closeSprite = new (Core.Content.Load<Texture2D>("Assets/Editor/Icons/closeIcon"));
            Sprite minimizeSprite = new (Core.Content.Load<Texture2D>("Assets/Editor/Icons/minimizeIcon"));

            // Creates the UI View window.
            var skin = Skin.CreateDefaultSkin();
/*            var style = new EditorWindowStyle
            {
                TitleFont = Graphics.Instance.BitmapFont,
                TitleFontColor = Color.White,

                CloseButtonStyle = new ImageButtonStyle
                {
                    Up = new SpriteDrawable(closeSprite), // Provide textures for button states
                    Down = new SpriteDrawable(closeSprite),
                    Over = new SpriteDrawable(closeSprite)
                },

                MinimizeButtonStyle = new ImageButtonStyle
                {
                    Up = new SpriteDrawable(minimizeSprite),
                    Down = new SpriteDrawable(minimizeSprite),
                    Over = new SpriteDrawable(minimizeSprite)
                }
            };*/

            UIWindow = new Window(ViewName,skin);
            CustomizeDefaultWindow(UIWindow);
            //UIWindow.DebugAll();
            UIWindow.SetDebug(true); // Enable debug mode for the window

            // Initialize the UI for this view
            InitializeUI(Canvas.Stage, UIWindow);
        }

        private void CustomizeDefaultWindow(Window window)
        {
            window.SetMovable(true);
            window.GetTitleLabel().SetVisible(false); // Hide the default title label
            window.SetResizable(true);
            var skin = Skin.CreateDefaultSkin();

            Table windowTable = new Table();
            windowTable.SetFillParent(true);
            window.Add(windowTable);

            windowTable.Bottom().Left();
            windowTable.Add(new Label("Test")).Bottom().Left();

            return;
            Table titleTable = new Table();
            // Custom Title Bar Logic


            // Create a horizontal group for titlebar controls
            var titleBarButtons = new HorizontalGroup();
            titleBarButtons.SetPad(0);
            titleBarButtons.SetSpacing(5);

            // Create minimize button
            var minimizeButton = new TextButton("_", skin);
            minimizeButton.OnClicked += b =>
            {
                if (window.IsVisible())
                    window.SetVisible(false);
                else
                    window.SetVisible(true);
            };

            // Create close button
            var closeButton = new TextButton("X", skin);
            closeButton.OnClicked += b =>
            {
                window.Remove(); // Removes the window from UI
            };

            // Add buttons to the window’s title bar
            titleBarButtons.AddElement(minimizeButton);
            titleBarButtons.AddElement(closeButton);

            // Right-align buttons by adding the horizontal group to title table
            titleTable.Add(titleBarButtons).Center();
            window.Add(titleTable).Right();
        }

        // Helper class for linear gradient background
        private class LinearGradientDrawable : IDrawable
        {
            private Color _startColor;
            private Color _endColor;
            private Texture2D _gradientTexture;
            private int _lastHeight = -1;

            // Padding properties
            private float _leftWidth;
            private float _rightWidth;
            private float _topHeight;
            private float _bottomHeight;

            public LinearGradientDrawable(Color startColor, Color endColor)
            {
                _startColor = startColor;
                _endColor = endColor;
            }

            public float LeftWidth { get => _leftWidth; set => _leftWidth = value; }
            public float RightWidth { get => _rightWidth; set => _rightWidth = value; }
            public float TopHeight { get => _topHeight; set => _topHeight = value; }
            public float BottomHeight { get => _bottomHeight; set => _bottomHeight = value; }
            public float MinWidth { get => 0; set { } }
            public float MinHeight { get => 0; set { } }

            public void Draw(Batcher batcher, float x, float y, float width, float height, Color color)
            {
                if (width < 1 || height < 1) return;

                int intHeight = (int)height;
                if (intHeight != _lastHeight)
                {
                    _gradientTexture?.Dispose();
                    _gradientTexture = new Texture2D(batcher.GraphicsDevice, 1, intHeight);
                    var colors = new Color[intHeight];

                    if (intHeight == 1)
                    {
                        colors[0] = _startColor;
                    }
                    else
                    {
                        for (int i = 0; i < intHeight; i++)
                        {
                            float t = i / (float)(intHeight - 1);
                            colors[i] = Color.Lerp(_startColor, _endColor, t);
                        }
                    }

                    _gradientTexture.SetData(colors);
                    _lastHeight = intHeight;
                }

                batcher.Draw(_gradientTexture, new Vector2(x, y), null, color, 0, Vector2.Zero,
                             new Vector2(width / _gradientTexture.Width, height / _gradientTexture.Height),
                             SpriteEffects.None, 0);
            }

            public void SetPadding(float top, float bottom, float left, float right)
            {
                _topHeight = top;
                _bottomHeight = bottom;
                _leftWidth = left;
                _rightWidth = right;
            }
        }

        protected virtual void InitializeUI(Stage stage, Window window)
        {
            stage.AddElement(window);
        }
    }
}