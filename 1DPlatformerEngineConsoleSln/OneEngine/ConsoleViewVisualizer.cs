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
            //TODO: All this realization it's a shit and ducttape
            //TODO: maybe problems with fractional numbers
            //TODO: maybe problems with start and end cut and all it at all unstable
            List<Color4> colorList = new List<Color4>();

            Objs.Player playerObj = ObjList.GetContent().OfType<Objs.Player>().First();

            float fov = playerObj.Fov;
            float start = -(fov / 180);

            float step = -((start * 2) / console.Cols);
            bool firstHalf = true;
            float yDir = start;
            float xDir = 1 + yDir;

            for (int i = 0; i < console.Rows; i++)
            {
                colorList.Add(castRay(ref firstHalf, ref xDir, ref yDir, step, playerObj));
            }

            return colorList.ToArray();
        }

        private Color4 castRay(ref bool firstHalf, ref float xDir, ref float yDir, float step, Objs.Player playerObj)
        {
            int viewX = playerObj.X + (playerObj.Width - 1);
            int viewY = playerObj.Y;

            if (xDir >= 1)
            {
                firstHalf = false;
            }

            xDir += firstHalf ? +step : -step;
            yDir += step;

            double distance = 0;
            float distanceStep = 0.5f;
            int max = 8;
            int x = 0;
            int y = 0;
            float xx = x;
            float yy = y;

            while(distance < max)
            {
                x = Convert.ToInt32(Math.Floor(xx));
                y = Convert.ToInt32(Math.Floor(yy));

                foreach (Objs.Obj obj in ObjList.GetContent())
                {
                    if(obj.X == viewX + x && obj.Y == viewY + y)
                    {
                        byte common = Convert.ToByte(255 - (255 / max) * distance);
                        Color4 objColor = new Color4(common, common, common, 255);

                        return objColor;
                    }
                }

                xx += xDir * distanceStep;
                yy += yDir * distanceStep;

                distance = Math.Sqrt(Math.Pow(xx, 2) + Math.Pow(yy, 2));
            }
            return default(Color4);
        }

        private void visualize(Color4[] colors)
        {
            //TODO: Eliminate the fisheye effect
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
