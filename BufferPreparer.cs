using System;
using System.Collections.Generic;
using System.Linq;
using Buffer = System.Drawing.Bitmap;

namespace Render
{

    public class BufferPreparer
    {
        public Queue<Buffer> Buffers { get; private set; }
        Enviroment Enviroment { get; set; }
        public Buffer GetBuffer() => Buffers.Count == 0 ? new Buffer(1, 1) : Buffers.Dequeue();
        public Rasterizer CurrentRaterizer => Rasterizers[RasterizerIndex];
        public List<Rasterizer> Rasterizers { get; set; }
        public int RasterizerIndex { get; private set; }
        public BufferPreparer(List<Camera> cameras, Enviroment enviroment)
        {
            Enviroment = enviroment;
            Buffers = new Queue<Buffer>();
            Rasterizers = cameras.Select(c => new Rasterizer(c)).ToList();
        }
        
        public void PrepareNewBuffer()
        { 
            Buffers.Enqueue(Rasterizers[RasterizerIndex].Rasterize(Enviroment.GetPrimitives()));
        }
    }
}
