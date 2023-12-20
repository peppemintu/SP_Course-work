using System.Numerics;

namespace Render
{
    public interface IShader
    {
        void ComputeShader(ref Vertex vertex, Camera camera);
    }


    public struct Light
    {
        public Vector3 Pos;
        public float Intensivity;
        public Light(Vector3 pos , float intensivity)
        {
            Pos = pos;
            Intensivity = intensivity;
        }
    }
}









