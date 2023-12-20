using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Render
{
    public enum Axis
    {
        X, Y, Z
    }
    static class VectorMath
    {
        public static float Cross(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
        public static Vector3 Dot(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, -(v1.X * v2.Z - v1.Z * v2.X), v1.X * v2.Y - v1.Y * v2.X);
        }
        public static float Angle(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Acos(Cross(v1, v2) / (v1.Length() * v2.Length()));
        }
        public static Vector3 Rotate(this Vector3 Vector3, float angle, Axis axis)
        {
            var rotation = axis == Axis.X ? Matrix4x4.CreateRotationX(angle) : axis == Axis.Y ? Matrix4x4.CreateRotationY(angle) : Matrix4x4.CreateRotationZ(angle);
            return Vector3.Transform(Vector3, rotation);
        }
        public static Vector3 Proection(Vector3 v1 , Vector3 v2)
        {
            return Cross(v1, v2) / Cross(v2, v2) * v2;
        }
        
        public static float[] GetSurfaceCoefs(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var v1 = p3 - p1;
            var v2 = p2 - p1;
            var dot = Dot(v1, v2);
            var d = -Cross(dot, p1);
            return new float[] { dot.X, dot.Y, dot.Z, d };
        }
        public static bool BelongsPoly(Vector3 p1 , Vector3 p2 , Vector3 p3, Vector3 point)
        {
            var v1 = p1 - point;
            var v2 = p2 - point;
            var v3 = p3 - point;
            var s1 = Dot(v1, v2).Length() + Dot(v2, v3).Length() + Dot(v3, v1).Length();
            var s2 = Dot(p3 - p1, p3 - p2).Length();
            return Math.Abs(s1 - s2) < 0.01;
        }
        public static bool AreIntersecting(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 start, Vector3 end, out Vector3 intersect)
        {
            var coefs = GetSurfaceCoefs(p1,p2,p3);
            var d = end - start;
            var scaleCoef = -(coefs[0] * start.X + coefs[1] * start.Y + coefs[2] * start.Z + coefs[3]) /
                (coefs[0] * d.X + coefs[1] * d.Y + coefs[2] * d.Z);
            var p = start + scaleCoef * d;
            if (scaleCoef >= 0 && scaleCoef <= 1 && BelongsPoly(p1,p2,p3, p))
            {
                intersect = p;
                return true;
            }
            intersect = Vector3.Zero;
            return false;
        }
        public static Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3) 
        {
            var v1 = p3 - p1;
            var v2 = p2 - p1;
            var dot = Dot(v1, v2);
            return Vector3.Normalize(dot);
        }
    }
}
