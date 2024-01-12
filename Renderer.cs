using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Hyougen.Utilities;
using FbxSharp;

namespace Hyougen {
    internal class Renderer : GameWindow {
        protected static int VertexBufferObject;
        protected static int VertexArrayObject;
        protected static int ElementBufferObject;
        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            // generate VBO
            VertexBufferObject = GL.GenBuffer();

            // generate VAO
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene.vertices.Count * sizeof(float), Scene.vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene.indices.Length * sizeof(uint), Scene.indices, BufferUsageHint.StaticDraw);

            Scene.shader = new Shader(Shader.vertexPath, Shader.fragmentPath);
        }
        protected override void OnRenderFrame(FrameEventArgs e) {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Scene.shader.Use();

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, Scene.indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs foo) {
            base.OnUpdateFrame(foo);

            if(this.KeyboardState.IsKeyDown(Keys.Escape)) Close();
        }
        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnUnload() {
            base.OnUnload();
            Scene.shader.Dispose();
        }

        protected Renderer(int width,
                           int height,
                           string title) : 
                           base(GameWindowSettings.Default, 
                                new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        protected static void Main(string[] args) {
            using(Renderer renderer = new Renderer(1920 / 2, 1080 / 2, "Tite")) {
                renderer.Run();
            }
        }
    };
}
