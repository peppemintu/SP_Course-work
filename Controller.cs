using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Render
{
    public class Controller
    {
        public HashSet<Keys> DownKeys;
        public int Speed { get; set; } = 40;

        public Render RenderObj;
        public Camera CurrentCamera => RenderObj.Preparer.CurrentRaterizer.Camera;
        public Controller(Render render)
        {
            DownKeys = new HashSet<Keys>();
            RenderObj = render;
        }
        public void KeyDown(KeyEventArgs e)
        {
            if (!DownKeys.Contains(e.KeyCode))
            {
                DownKeys.Add(e.KeyCode);
            }
        }
        public void KeyUp(KeyEventArgs e)
        {
            if (DownKeys.Contains(e.KeyCode))
            {
                DownKeys.Remove(e.KeyCode);
            }
        }
        public void ComputeKeys()
        {
            foreach (var key in DownKeys)
            {
                switch (key)
                {
                    case Keys.Q:
                        CurrentCamera.Rotate(-0.05f, Axis.Y);
                        break;
                    case Keys.S:
                        var v = -CurrentCamera.Pivot.ZAxis * Speed;
                        CurrentCamera.Move(v);
                        break;
                    case Keys.E:
                        CurrentCamera.Rotate(0.05f, Axis.Y);
                        break;
                    case Keys.W:
                        v = CurrentCamera.Pivot.ZAxis * Speed;
                        CurrentCamera.Move(v);
                        break;
                    case Keys.D:
                        v = CurrentCamera.Pivot.XAxis * Speed;
                        CurrentCamera.Move(v);
                        break;
                    case Keys.A:
                        v = -CurrentCamera.Pivot.XAxis * Speed;
                        CurrentCamera.Move(v);
                        break;
                    case Keys.R:
                        CurrentCamera.Rotate(-0.05f, Axis.X);
                        break;
                    case Keys.F:
                        CurrentCamera.Rotate(0.05f, Axis.X);
                        break;
                }
            }
        }
    }
}
