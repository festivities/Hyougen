using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using FbxSharp;
using ChamberLib;

namespace Hyougen.Utilities {
    internal class FBXIO {
        public void Import() {
            Importer importer = new Importer();
            FbxSharp.Scene scene = importer.Import(Scene.ailixiya);

            foreach(FbxSharp.Node node in scene.Nodes) {
                Mesh mesh = null;

                for(int index = 0; index != node.GetNodeAttributeCount(); ++index) {
                    // find the first NodeAttribute that's a Mesh, fuck FBX format
                    if(node.GetNodeAttributeByIndex(index) is Mesh) {
                        mesh = node.GetNodeAttributeByIndex(index) as Mesh;
                        break;
                    }

                    FbxSharp.Matrix meshTransform = node.EvaluateGlobalTransform();

                    List<Vertex_PBiBwNT> vertices =
                        Enumerable.Range(0, mesh.GetControlPointsCount())
                            .Select(ix => {
                                FbxSharp.Vector4 cp = mesh.GetControlPointAt((int)ix);
                                FbxSharp.Vector4 baked = meshTransform.MultNormalize(cp);
                                // THIS IS SO FUCKING SCUFFED
                                ChamberLib.Vector3 pos = new ChamberLib.Vector4((float)baked.X, (float)baked.Y,
                                    (float)baked.Z, (float)baked.W).ToVectorXYZ();
                                return new Vertex_PBiBwNT { Position = pos };
                            }).ToList();
                }
            }
        }
    };
}
