using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Render.Primitives;


namespace Render
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var env = new Enviroment(1000);
            var camera2 = new Camera(new Vector3(0, 100, -400), 1, 1.57f, 800, 600);
            //CreateSylvanasScene(env, camera2);
            CreateCubeScene(env, camera2, 5, 5, 5);
            var render = new Render(env, 800, 600, camera2);
            render.Start();
        }

        public static void CreateSylvanasScene(Enviroment enviroment, Camera bindingCamera)
        {
            var model = ObjParser.FromObjFile(@".\Models\sylvanas_obj.obj", null);
            var smShader = new ShadowMappingShader(enviroment, new Rasterizer(bindingCamera), 100000f);
            var pShader = new PhongModelShader(new Light(bindingCamera.Pivot.Center, 2f));
            model.Shaders = new IShader[] { smShader, pShader };
            model.Scale(10f);
            model.Rotate(3.14f + 0.5f, Axis.Y);
            model.Move(new Vector3(0, -2200, -1000));
            enviroment.AddPrimitive(model);
        }
        public static void CreateCubeScene(Enviroment enviroment, Camera bindingCamera, int x, int y, int z)
        {
            var rand = new Random();

            var ps = new PhongModelShader(new Light(Vector3.Zero, 10f));
            var sm = new ShadowMappingShader(enviroment, new Rasterizer(bindingCamera), 50000f);
            var cubes = Enumerable.Range(0, x)
                .SelectMany(i1 => Enumerable.Range(0, y)
                .SelectMany(i2 => Enumerable.Range(0, z)
                .Select(i3 => new Cube(new Vector3(i1 * 200 * x, i2 * 200 * y, i3 * 200 * z), rand.Next(50, 800))
                {
                    Shaders =
                new IShader[] { sm, ps }
                })))
                .ToArray();

            foreach (var item in cubes)
            {
                item.Rotate((float)(rand.NextDouble() * Math.PI), Axis.X);
                item.Rotate((float)(rand.NextDouble() * Math.PI), Axis.Y);
                item.Rotate((float)(rand.NextDouble() * Math.PI), Axis.Z);
                //если свет хочется пофиксить, при запуске отдалять
                //item.Move(new Vector3(0, -1200, 0));
                enviroment.AddPrimitive(item);
            }
        }
    }
}

