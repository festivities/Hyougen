using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyougen {
    internal class Scene {
        public static readonly float[] vertices = {
             0.5f,  0.5f, 0.0f, 
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f, 
            -0.5f,  0.5f, 0.0f 
        };

        public static readonly uint[] indices = {
            0, 1, 3,  
            1, 2, 3 
        };

        public static Shader? shader;

        public const string ailixiya = 
            "C:\\Users\\fes\\dev\\repositories\\Hyougen\\Ailixiya\\animation_AvatarQ_Elysia_C1_GS_UI_004.fbx";
    };
}
