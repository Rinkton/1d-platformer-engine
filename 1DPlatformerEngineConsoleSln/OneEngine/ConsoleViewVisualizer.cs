using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics;
using OpenTK.Input;
using SunshineConsole;

namespace OneEngine
{
    public class ConsoleViewVisualizer : Visualizer
    {
        private ConsoleWindow console;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowHeight">Count of rays that need to cast</param>
        /// <param name="windowWidth">The width to stretch a 1D line</param>
        /// <param name="windowName"></param>
        public ConsoleViewVisualizer(int windowHeight, int windowWidth, string windowName)
        {
            this.console = new ConsoleWindow(windowHeight, windowWidth, windowName);
        }

        public override void Main()
        {
            //TODO: Optimize this shit, 10 fps!
            //TODO: Realize turn
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            System.Diagnostics.Debug.WriteLine("start");

            clearConsole();

            Color4[] colors = getColors();

            visualize(colors);

            if (!console.WindowUpdate())
            {
                //TODO: Not so good decision, but I hope it's temporarily
                throw new Exception("WindowUpdate return false.");
            }

            System.Diagnostics.Debug.WriteLine(sw.GetTime());
            sw.Stop();
        }

        public override void SetKeys()
        {
            KeyChecker.W = console.KeyIsDown(Key.W);
            KeyChecker.S = console.KeyIsDown(Key.S);
            KeyChecker.Space = console.KeyIsDown(Key.Space);
            KeyChecker.R = console.KeyIsDown(Key.R);
        }

        private void clearConsole()
        {
            for (int i = 0; i < console.Rows; i++)
            {
                for (int j = 0; j < console.Cols; j++)
                {
                    console.Write(i, j, ' ', Color4.Black);
                }
            }
        }

        private Color4[] getColors()
        {
            List<Color4> colorList = new List<Color4>();

            Objs.Player playerObj = ObjList.GetContent().OfType<Objs.Player>().First();

            int viewX = playerObj.X + playerObj.Width;
            int viewY = playerObj.Y + 1;

            float pov = playerObj.Pov;
            float fov = playerObj.Fov;

            float startAngle = pov - (fov / 2);

            for (int i = 0; i < console.Rows; i++)
            {
                float currentAngle = startAngle + (i * (fov / console.Rows));
                colorList.Add(castRay(currentAngle, viewX, viewY));
            }

            return colorList.ToArray();
        }

        private Color4 castRay(float currentAngle, int startX, int startY)
        {
            int x = startX;
            int y = startY;
            float step = 0.05f;

            float distance = 0;

            var blockType = new Objs.Block().GetType();
            var playerType = new Objs.Player().GetType();

            int max = 20;
            double xx = x;
            double yy = y;

            while (distance < max)
            {
                xx = x + distance * Math.Cos(currentAngle);
                yy = y + distance * Math.Sin(currentAngle);
                distance += step;
                if (x != Math.Floor(xx) || y != Math.Floor(yy))
                {
                    foreach (Objs.Obj obj in ObjList.GetContent())
                    {
                        if (obj.X == Math.Floor(xx) && obj.Y == Math.Floor(yy))
                        {
                            if (blockType == obj.GetType())
                            {
                                byte common = Convert.ToByte(255 - (255 / max) * distance);
                                Color4 objColor = new Color4(common, common, common, 255);

                                return objColor;
                            }
                        }
                    }
                }
            }

            return default(Color4);
        }

        private void visualize(Color4[] colors)
        {
            int y = 0;

            foreach(Color4 color in colors)
            {
                for(int x = 0; x < console.Cols; x++)
                {
                    drawSymbol(x, y, color);
                }
                y++;
            }
        }

        private void drawSymbol(int x, int y, Color4 color)
        {
            console.Write(y, x, '█', color);
        }
    }
}
