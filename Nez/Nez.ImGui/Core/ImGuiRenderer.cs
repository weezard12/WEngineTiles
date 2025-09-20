﻿using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace Nez.ImGuiTools
{
	/// <summary>
	/// ImGui renderer for use with XNA-likes (FNA & MonoGame)
	/// </summary>
	public class ImGuiRenderer
	{
		public ImFontPtr DefaultFontPtr { get; private set; }

		// Graphics
		BasicEffect _effect;
		RasterizerState _rasterizerState;

		readonly VertexDeclaration _vertexDeclaration;
		readonly int _vertexDeclarationSize;

		byte[] _vertexData;
		VertexBuffer _vertexBuffer;
		int _vertexBufferSize;

		byte[] _indexData;
		IndexBuffer _indexBuffer;
		int _indexBufferSize;

		// Textures
		Dictionary<IntPtr, Texture2D> _loadedTextures = new Dictionary<IntPtr, Texture2D>();

		int _textureId;
		IntPtr? _fontTextureId;

		// Input
		int _scrollWheelValue;


		List<ImGuiKey> _keys = new List<ImGuiKey>();


		public ImGuiRenderer(Game game)
		{
			unsafe
			{
				_vertexDeclarationSize = sizeof(ImDrawVert);
			}

			_vertexDeclaration = new VertexDeclaration(
				_vertexDeclarationSize,

				// Position
				new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),

				// UV
				new VertexElement(8, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),

				// Color
				new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 0)
			);

			ImGui.SetCurrentContext(ImGui.CreateContext());

			_rasterizerState = new RasterizerState()
			{
				CullMode = CullMode.None,
				DepthBias = 0,
				FillMode = FillMode.Solid,
				MultiSampleAntiAlias = false,
				ScissorTestEnable = true,
				SlopeScaleDepthBias = 0
			};

			SetupInput();
		}


		#region ImGuiRenderer

		/// <summary>
		/// Creates a texture and loads the font data from ImGui. Should be called when the <see cref="GraphicsDevice" /> is initialized but before any rendering is done
		/// </summary>
		public unsafe void RebuildFontAtlas(ImGuiOptions options)
		{
			// Get font texture from ImGui
			var io = ImGui.GetIO();

			if (options._includeDefaultFont)
				DefaultFontPtr = io.Fonts.AddFontDefault();

			foreach (var font in options._fonts)
				io.Fonts.AddFontFromFileTTF(font.Item1, font.Item2);

			io.Fonts.GetTexDataAsRGBA32(out byte* pixelData, out int width, out int height, out int bytesPerPixel);

			// Copy the data to a managed array
			var pixels = new byte[width * height * bytesPerPixel];
			Marshal.Copy(new IntPtr(pixelData), pixels, 0, pixels.Length);

			// Create and register the texture as an XNA texture
			var tex2d = new Texture2D(Core.GraphicsDevice, width, height, false, SurfaceFormat.Color);
			tex2d.SetData(pixels);

			// Should a texture already have been built previously, unbind it first so it can be deallocated
			if (_fontTextureId.HasValue)
				UnbindTexture(_fontTextureId.Value);

			// Bind the new texture to an ImGui-friendly id
			_fontTextureId = BindTexture(tex2d);

			// Let ImGui know where to find the texture
			io.Fonts.SetTexID(_fontTextureId.Value);
			io.Fonts.ClearTexData(); // Clears CPU side texture data
		}

		/// <summary>
		/// Creates a pointer to a texture, which can be passed through ImGui calls such as <see cref="ImGui.Image" />. That pointer is then used by ImGui to let us know what texture to draw
		/// </summary>
		public IntPtr BindTexture(Texture2D texture)
		{
			var id = new IntPtr(_textureId++);
			_loadedTextures.Add(id, texture);
			return id;
		}

		/// <summary>
		/// Removes a previously created texture pointer, releasing its reference and allowing it to be deallocated
		/// </summary>
		public void UnbindTexture(IntPtr textureId)
		{
			_loadedTextures.Remove(textureId);
		}

		/// <summary>
		/// Sets up ImGui for a new frame, should be called at frame start
		/// </summary>
		public void BeforeLayout(float deltaTime)
		{
			ImGui.GetIO().DeltaTime = deltaTime;
			UpdateInput();
			ImGui.NewFrame();
		}

		/// <summary>
		/// Asks ImGui for the generated geometry data and sends it to the graphics pipeline, should be called after the UI is drawn using ImGui.** calls
		/// </summary>
		public void AfterLayout()
		{
			ImGui.Render();
			unsafe
			{
				RenderDrawData(ImGui.GetDrawData());
			}
		}

		#endregion


		#region Setup & Update

#if FNA
		delegate string GetClipboardTextDelegate();
		delegate void SetClipboardTextDelegate(IntPtr userData, string txt);

		static void SetClipboardText(IntPtr userData, string txt) => SDL2.SDL.SDL_SetClipboardText(txt);

		static string GetClipboardText() => SDL2.SDL.SDL_GetClipboardText();
#endif

		/// <summary>
		/// Maps ImGui keys to XNA keys. We use this later on to tell ImGui what keys were pressed
		/// </summary>
		void SetupInput()
		{
			var io = ImGui.GetIO();

#if FNA
			// forward clipboard methods to SDL
			io.SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate<SetClipboardTextDelegate>(SetClipboardText);
			io.GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate<GetClipboardTextDelegate>(SDL2.SDL.SDL_GetClipboardText);
#endif

			// In newer ImGui.NET, we need to use AddKeyEvent instead of the old KeyMap approach
			_keys.Add(ImGuiKey.Tab);
			_keys.Add(ImGuiKey.LeftArrow);
			_keys.Add(ImGuiKey.RightArrow);
			_keys.Add(ImGuiKey.UpArrow);
			_keys.Add(ImGuiKey.DownArrow);
			_keys.Add(ImGuiKey.PageUp);
			_keys.Add(ImGuiKey.PageDown);
			_keys.Add(ImGuiKey.Home);
			_keys.Add(ImGuiKey.End);
			_keys.Add(ImGuiKey.Delete);
			_keys.Add(ImGuiKey.Backspace);
			_keys.Add(ImGuiKey.Enter);
			_keys.Add(ImGuiKey.Escape);
			_keys.Add(ImGuiKey.LeftCtrl);
			_keys.Add(ImGuiKey.A);
			_keys.Add(ImGuiKey.C);
			_keys.Add(ImGuiKey.V);
			_keys.Add(ImGuiKey.X);
			_keys.Add(ImGuiKey.Y);
			_keys.Add(ImGuiKey.Z);


#if !FNA
			Core.Instance.Window.TextInput += (s, a) =>
			{
				if (a.Character == '\t')
					return;

				io.AddInputCharacter(a.Character);
			};
#else
			TextInputEXT.TextInput += c =>
			{
				if (c == '\t')
					return;
				ImGui.GetIO().AddInputCharacter(c);
			};
#endif
		}

		/// <summary>
		/// Updates the <see cref="Effect" /> to the current matrices and texture
		/// </summary>
		Effect UpdateEffect(Texture2D texture)
		{
			_effect = _effect ?? new BasicEffect(Core.GraphicsDevice);

			var io = ImGui.GetIO();

			_effect.World = Matrix.Identity;
			_effect.View = Matrix.Identity;
			_effect.Projection = Matrix.CreateOrthographicOffCenter(0, io.DisplaySize.X, io.DisplaySize.Y, 0, -1f, 1f);
			_effect.TextureEnabled = true;
			_effect.Texture = texture;
			_effect.VertexColorEnabled = true;

			return _effect;
		}

		/// <summary>
		/// Sends XNA input state to ImGui
		/// </summary>
		void UpdateInput()
		{
			var io = ImGui.GetIO();

			var mouse = Input.CurrentMouseState;
			var keyboard = Input.CurrentKeyboardState;

			// Map XNA Keys to ImGuiKey and use AddKeyEvent
			io.AddKeyEvent(ImGuiKey.Tab, keyboard.IsKeyDown(Keys.Tab));
			io.AddKeyEvent(ImGuiKey.LeftArrow, keyboard.IsKeyDown(Keys.Left));
			io.AddKeyEvent(ImGuiKey.RightArrow, keyboard.IsKeyDown(Keys.Right));
			io.AddKeyEvent(ImGuiKey.UpArrow, keyboard.IsKeyDown(Keys.Up));
			io.AddKeyEvent(ImGuiKey.DownArrow, keyboard.IsKeyDown(Keys.Down));
			io.AddKeyEvent(ImGuiKey.PageUp, keyboard.IsKeyDown(Keys.PageUp));
			io.AddKeyEvent(ImGuiKey.PageDown, keyboard.IsKeyDown(Keys.PageDown));
			io.AddKeyEvent(ImGuiKey.Home, keyboard.IsKeyDown(Keys.Home));
			io.AddKeyEvent(ImGuiKey.End, keyboard.IsKeyDown(Keys.End));
			io.AddKeyEvent(ImGuiKey.Delete, keyboard.IsKeyDown(Keys.Delete));
			io.AddKeyEvent(ImGuiKey.Backspace, keyboard.IsKeyDown(Keys.Back));
			io.AddKeyEvent(ImGuiKey.Enter, keyboard.IsKeyDown(Keys.Enter));
			io.AddKeyEvent(ImGuiKey.Escape, keyboard.IsKeyDown(Keys.Escape));
			io.AddKeyEvent(ImGuiKey.LeftCtrl, keyboard.IsKeyDown(Keys.LeftControl));
			io.AddKeyEvent(ImGuiKey.RightCtrl, keyboard.IsKeyDown(Keys.RightControl));
			io.AddKeyEvent(ImGuiKey.LeftShift, keyboard.IsKeyDown(Keys.LeftShift));
			io.AddKeyEvent(ImGuiKey.RightShift, keyboard.IsKeyDown(Keys.RightShift));
			io.AddKeyEvent(ImGuiKey.LeftAlt, keyboard.IsKeyDown(Keys.LeftAlt));
			io.AddKeyEvent(ImGuiKey.RightAlt, keyboard.IsKeyDown(Keys.RightAlt));
			io.AddKeyEvent(ImGuiKey.LeftSuper, keyboard.IsKeyDown(Keys.LeftWindows));
			io.AddKeyEvent(ImGuiKey.RightSuper, keyboard.IsKeyDown(Keys.RightWindows));

			// Add letter keys
			io.AddKeyEvent(ImGuiKey.A, keyboard.IsKeyDown(Keys.A));
			io.AddKeyEvent(ImGuiKey.C, keyboard.IsKeyDown(Keys.C));
			io.AddKeyEvent(ImGuiKey.V, keyboard.IsKeyDown(Keys.V));
			io.AddKeyEvent(ImGuiKey.X, keyboard.IsKeyDown(Keys.X));
			io.AddKeyEvent(ImGuiKey.Y, keyboard.IsKeyDown(Keys.Y));
			io.AddKeyEvent(ImGuiKey.Z, keyboard.IsKeyDown(Keys.Z));

			// Modifier keys are now set using AddKeyEvent
			io.AddKeyEvent(ImGuiKey.ModCtrl, keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl));
			io.AddKeyEvent(ImGuiKey.ModShift, keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift));
			io.AddKeyEvent(ImGuiKey.ModAlt, keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt));
			io.AddKeyEvent(ImGuiKey.ModSuper, keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows));

			io.DisplaySize = new System.Numerics.Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth,
				Core.GraphicsDevice.PresentationParameters.BackBufferHeight);
			io.DisplayFramebufferScale = new System.Numerics.Vector2(1f, 1f);

			io.MousePos = new System.Numerics.Vector2(mouse.X, mouse.Y);

			io.MouseDown[0] = mouse.LeftButton == ButtonState.Pressed;
			io.MouseDown[1] = mouse.RightButton == ButtonState.Pressed;
			io.MouseDown[2] = mouse.MiddleButton == ButtonState.Pressed;

			var scrollDelta = mouse.ScrollWheelValue - _scrollWheelValue;
			io.MouseWheel = scrollDelta > 0 ? 1 : scrollDelta < 0 ? -1 : 0;
			_scrollWheelValue = mouse.ScrollWheelValue;
		}

		#endregion


		#region Internals

		/// <summary>
		/// Gets the geometry as set up by ImGui and sends it to the graphics device
		/// </summary>
		void RenderDrawData(ImDrawDataPtr drawData)
		{
			// Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers
			var lastViewport = Core.GraphicsDevice.Viewport;
			var lastScissorBox = Core.GraphicsDevice.ScissorRectangle;

			Core.GraphicsDevice.BlendFactor = Color.White;
			Core.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
			Core.GraphicsDevice.RasterizerState = _rasterizerState;
			Core.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

			// Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
			drawData.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

			// Setup projection
			Core.GraphicsDevice.Viewport = new Viewport(0, 0,
				Core.GraphicsDevice.PresentationParameters.BackBufferWidth,
				Core.GraphicsDevice.PresentationParameters.BackBufferHeight);

			UpdateBuffers(drawData);
			RenderCommandLists(drawData);

			// Restore modified state
			Core.GraphicsDevice.Viewport = lastViewport;
			Core.GraphicsDevice.ScissorRectangle = lastScissorBox;
		}

		unsafe void UpdateBuffers(ImDrawDataPtr drawData)
		{
			if (drawData.TotalVtxCount == 0)
			{
				return;
			}

			// Expand buffers if we need more room
			if (drawData.TotalVtxCount > _vertexBufferSize)
			{
				_vertexBuffer?.Dispose();

				_vertexBufferSize = (int)(drawData.TotalVtxCount * 1.5f);
				_vertexBuffer = new VertexBuffer(Core.GraphicsDevice, _vertexDeclaration, _vertexBufferSize,
					BufferUsage.None);
				_vertexData = new byte[_vertexBufferSize * _vertexDeclarationSize];
			}

			if (drawData.TotalIdxCount > _indexBufferSize)
			{
				_indexBuffer?.Dispose();

				_indexBufferSize = (int)(drawData.TotalIdxCount * 1.5f);
				_indexBuffer = new IndexBuffer(Core.GraphicsDevice, IndexElementSize.SixteenBits, _indexBufferSize,
					BufferUsage.None);
				_indexData = new byte[_indexBufferSize * sizeof(ushort)];
			}

			// Copy ImGui's vertices and indices to a set of managed byte arrays
			int vtxOffset = 0;
			int idxOffset = 0;

			for (var n = 0; n < drawData.CmdListsCount; n++)
			{
				// Use CmdLists instead of CmdListsRange in newer ImGui.NET
				var cmdList = drawData.CmdLists[n];

				fixed (void* vtxDstPtr = &_vertexData[vtxOffset * _vertexDeclarationSize])
				fixed (void* idxDstPtr = &_indexData[idxOffset * sizeof(ushort)])
				{
					Buffer.MemoryCopy((void*)cmdList.VtxBuffer.Data, vtxDstPtr, _vertexData.Length,
						cmdList.VtxBuffer.Size * _vertexDeclarationSize);
					Buffer.MemoryCopy((void*)cmdList.IdxBuffer.Data, idxDstPtr, _indexData.Length,
						cmdList.IdxBuffer.Size * sizeof(ushort));
				}

				vtxOffset += cmdList.VtxBuffer.Size;
				idxOffset += cmdList.IdxBuffer.Size;
			}

			// Copy the managed byte arrays to the gpu vertex- and index buffers
			_vertexBuffer.SetData(_vertexData, 0, drawData.TotalVtxCount * _vertexDeclarationSize);
			_indexBuffer.SetData(_indexData, 0, drawData.TotalIdxCount * sizeof(ushort));
		}

		unsafe void RenderCommandLists(ImDrawDataPtr drawData)
		{
			Core.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
			Core.GraphicsDevice.Indices = _indexBuffer;

			int vtxOffset = 0;
			int idxOffset = 0;

			for (int n = 0; n < drawData.CmdListsCount; n++)
			{
				// Use CmdLists instead of CmdListsRange in newer ImGui.NET
				var cmdList = drawData.CmdLists[n];
				for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
				{
					var drawCmd = cmdList.CmdBuffer[cmdi];
					if (!_loadedTextures.ContainsKey(drawCmd.TextureId))
					{
						throw new InvalidOperationException(
							$"Could not find a texture with id '{drawCmd.TextureId}', please check your bindings");
					}

					Core.GraphicsDevice.ScissorRectangle = new Rectangle(
						(int)drawCmd.ClipRect.X,
						(int)drawCmd.ClipRect.Y,
						(int)(drawCmd.ClipRect.Z - drawCmd.ClipRect.X),
						(int)(drawCmd.ClipRect.W - drawCmd.ClipRect.Y)
					);

					var effect = UpdateEffect(_loadedTextures[drawCmd.TextureId]);
					foreach (var pass in effect.CurrentTechnique.Passes)
					{
						pass.Apply();

#pragma warning disable CS0618 // FNA does not expose an alternative method.
						Core.GraphicsDevice.DrawIndexedPrimitives(
							primitiveType: PrimitiveType.TriangleList,
							baseVertex: vtxOffset,
							minVertexIndex: 0,
							numVertices: cmdList.VtxBuffer.Size,
							startIndex: idxOffset,
							primitiveCount: (int)drawCmd.ElemCount / 3
						);
#pragma warning restore CS0618
					}

					idxOffset += (int)drawCmd.ElemCount;
				}

				vtxOffset += cmdList.VtxBuffer.Size;
			}
		}

		#endregion
	}
}