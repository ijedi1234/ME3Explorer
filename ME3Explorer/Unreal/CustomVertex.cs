using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Unreal {
    public class CustomVertex {

        public struct PositionNormalTextured {

            public const VertexFormat Format = VertexFormat.PositionNormal;

            public float Tu, Tv, X, Y, Z;
            public Vector3 Position, Normal;

            public PositionNormalTextured(Vector3 vec, Vector3 normal, float tu, float tv) {
                Position = vec; Normal = normal;
                X = vec.X; Y = vec.Y; Z = vec.Z;
                Tu = tu; Tv = tv;
            }

        }

        public struct PositionTextured {

            public const VertexFormat Format = VertexFormat.PositionBlend4;

            public Vector3 Position;
            public float Tu, Tv, X, Y, Z;

            public PositionTextured(Vector3 vec, float tu, float tv) {
                Position = vec;
                X = vec.X; Y = vec.Y; Z = vec.Z;
                Tu = tu; Tv = tv;
            }

        }

        public struct PositionColored {

            public const VertexFormat Format = VertexFormat.PositionBlend4;

            public int Color;
            public Vector3 Position;
            public float X, Y, Z;

            public PositionColored(Vector3 vec, int color) {
                Position = vec;
                X = vec.X; Y = vec.Y; Z = vec.Z;
                Color = color;
            }

        }

    }
}
