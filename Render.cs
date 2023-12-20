using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Render
{
    public class Render
    {
        public Timer Timer { get; private set; }
        public BufferPreparer Preparer { get; private set; }
        public List<Camera> Cameras { get; private set; }
        public Form RenderForm { get; private set; }
        public Enviroment Enviroment { get; private set; }
        public Controller Controller { get; private set; }

        int Width;
        int Height;
        public Render(Enviroment enviroment, int winWidth, int winHeight , params Camera[] cameras)
        {
            Width = winHeight;
            Height = winHeight;
            Enviroment = enviroment;
            Cameras = cameras.ToList();
            foreach (var cam in Cameras)
            {
                cam.ScreenWidth = winWidth;
                cam.ScreenHeight = winHeight;
            }
            Preparer = new BufferPreparer(Cameras , enviroment);
            RenderForm = new Form1()
            {
                BackColor = Color.Black,
                Width = winWidth,
                Height = winHeight
            };
            Controller = new Controller(this);
            RenderForm.KeyDown += (args, e) => Controller.KeyDown(e);
            RenderForm.KeyUp += (args, e) => Controller.KeyUp(e);
            Timer = new Timer();
            Timer.Interval = 100;
            Timer.Tick += (args, e) =>
            {
                Controller.ComputeKeys();
                Preparer.PrepareNewBuffer();
                var buffer = Preparer.GetBuffer();
                if (buffer.Width != 1)
                {
                    RenderForm.BackgroundImage = new Bitmap(buffer , RenderForm.Size);
                }
            };
        }
        public void Start()
        {
            Timer.Start();
            Application.Run(RenderForm);
        }
    }
}