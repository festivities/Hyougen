using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FbxSharp;

namespace Hyougen.Utilities {
    internal class FBXIO {
        public void Import() {
            Importer importer = new Importer();
            FbxSharp.Scene scene = importer.Import(Scene.ailixiya);
        }
    };
}
