using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
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

        public override bool Main()
        {
            //TODO: Optimize this shit, 10 fps!
            Stopwatch sw = new Stopwatch();
            sw.RestartAsync();
            System.Diagnostics.Debug.WriteLine("start");

            clearConsole();

            Color4[] colors = getColors();

            visualize(colors);

            System.Diagnostics.Debug.WriteLine(sw.GetTime());
            sw.Stop();

            return !console.WindowUpdate();
        }

        public override void SetKeys()
        {
            //TODO: It's exactly effective method?
            KeyChecker.W = console.KeyIsDown(Key.W);
            KeyChecker.S = console.KeyIsDown(Key.S);
            KeyChecker.Space = console.KeyIsDown(Key.Space);
            KeyChecker.R = console.KeyIsDown(Key.R);
            KeyChecker.Escape = console.KeyIsDown(Key.Escape);
            KeyChecker.F = console.KeyIsDown(Key.F);
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

            console.CursorVisible = !playerObj.fixateMouse;

            float fov = playerObj.Fov;
            float pov = playerObj.Pov;
            float start = (fov / 2) + (pov - 90);

            float step = fov / console.Rows;
            float xDir;
            float yDir;

            int viewX = playerObj.TurnedRight ? playerObj.X + (playerObj.Width - 1) : playerObj.X;
            int viewY = playerObj.Y+1;

            for (int i = 0; i < console.Rows; i++)
            {
                float currentAngle = start + (step * i);
                getCoordDirs(currentAngle, out xDir, out yDir);
                xDir = playerObj.TurnedRight ? xDir * 1 : xDir * -1;
                colorList.Add(castRay(xDir, yDir, viewX, viewY));
            }

            return colorList.ToArray();
        }

        private void getCoordDirs(float angle, out float xDir, out float yDir)
        {
            if(angle <= 90)
            {
                xDir = angle / 90;
            }
            else
            {
                xDir = 1 - (angle - 90) / 90;
            }
            yDir = angle / 90 - 1;
        }

        private Color4 castRay(float xDir, float yDir, int viewX, int viewY)
        {
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
                    if(obj.X == viewX + x && obj.Y == viewY + y && obj.GetType() != new Objs.Player().GetType())
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
            //TODO: Eliminate the fisheye effect (if it'll appeare)
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
