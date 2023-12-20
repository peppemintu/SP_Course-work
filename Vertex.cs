using System.Numerics;

namespace Render
{
    public struct Vertex
    {
        public Primitive Primitive;
        public Vector3 Position;
        public TGAColor Color;
        public Vector3 Normal;

        public Vertex(Vector3 pos , TGAColor color, Vector3 normal, Primitive primitive)
        {
            Primitive = primitive;
            Position = pos;
            Color = color;
            Normal = normal;
        }
    }
}
