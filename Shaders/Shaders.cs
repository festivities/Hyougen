using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hyougen {
    internal class Shader {
        static int Handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath) {
            string vertShaderSrc = File.ReadAllText(vertexPath);
            string fragShaderSrc = File.ReadAllText(fragmentPath);

            int vertShader = GL.CreateShader(ShaderType.VertexShader);
            int fragShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertShader, vertShaderSrc);
            GL.ShaderSource(fragShader, fragShaderSrc);

            GL.CompileShader(vertShader);

            GL.GetShader(vertShader, ShaderParameter.CompileStatus, out int success);
            if(success == 0) {
                string infoLog = GL.GetShaderInfoLog(vertShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(fragShader);

            // tutorial fucked this up, original code is redefining success object ugh
            GL.GetShader(fragShader, ShaderParameter.CompileStatus, out success);
            if(success == 0) {
                string infoLog = GL.GetShaderInfoLog(fragShader);
                Console.WriteLine(infoLog);
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertShader);
            GL.AttachShader(Handle, fragShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
            if(success == 0) {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, vertShader);
            GL.DetachShader(Handle, fragShader);
            GL.DeleteShader(fragShader);
            GL.DeleteShader(vertShader);
        }
        ~Shader() { if(!disposedValue) Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?"); }

        public void Use() { GL.UseProgram(Handle); }
        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                GL.DeleteProgram(Handle);
                disposedValue = true;
            }
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public const string vertexPath = "C:\\Users\\fes\\dev\\repositories\\Hyougen\\Shaders\\Vert.vert";
        public const string fragmentPath = "C:\\Users\\fes\\dev\\repositories\\Hyougen\\Shaders\\Frag.frag";
    };
}
