using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics;
using OpenTK.Input;
using SunshineConsole;

namespace OneEngine.Windows.ConsoleView
{
    class Visualizer : Presets.VisualizerSunshineConsole
    {
        public Visualizer(ConsoleWindow console) : base(console) { }

        public override bool Visualize()
        {
            //TODO: Optimization better... but it can be more better, right? For example destroy relating between window height and ray count
            Stopwatch sw = new Stopwatch();
            sw.RestartAsync();
            System.Diagnostics.Debug.WriteLine("start");

            clearConsole();

            Color4[] colors = getColors();

            drawAll(colors);

            System.Diagnostics.Debug.WriteLine(sw.GetTime());
            sw.Stop();

            return !Console.WindowUpdate();
        }

        private Color4[] getColors()
        {
            List<Color4> colorList = new List<Color4>();

            Type playerType = new Objs.Player(0, 0, null).GetType();
            Objs.Player playerObj = (Objs.Player)ObjMap.FindFirstObjByThisType(playerType);

            Console.CursorVisible = !playerObj.FixateMouse;

            float fov = playerObj.Fov;
            float pov = playerObj.Pov;
            float start = (fov / 2) + (pov - 90);

            float step = fov / Console.Rows;
            float xDir;
            float yDir;

            int viewX = playerObj.TurnedRight ? playerObj.X + (playerObj.Width - 1) : playerObj.X;
            int viewY = playerObj.Y + 1;

            int maxDistance = 8;

            List<Objs.Obj> restrictedObjList = getRestrictedObjList(viewX, viewY, maxDistance, maxDistance);

            for (int i = 0; i < Console.Rows; i++)
            {
                float currentAngle = start + (step * i);
                getCoordDirs(currentAngle, out xDir, out yDir);
                xDir = playerObj.TurnedRight ? xDir * 1 : xDir * -1;
                colorList.Add(castRay(xDir, yDir, viewX, viewY, maxDistance, restrictedObjList));
            }

            return colorList.ToArray();
        }

        //TODO: X Y - Position, Width Height - Size
        private List<Objs.Obj> getRestrictedObjList(int viewX, int viewY, int areaWidth, int areaHeight)
        {
            List<Objs.Obj> restrictedObjList = new List<Objs.Obj>();

            #region take only the current area
            for(int y = -areaHeight; y < areaHeight; y++)
            {
                for (int x = -areaWidth; x < areaWidth; x++)
                {
                    Objs.Obj obj = ObjMap.GetObj(viewX + x, viewY + y);

                    if(obj.GetType() != new Objs.NullObj().GetType())
                    {
                        restrictedObjList.Add(obj);
                    }
                }
            }
            #endregion

            return restrictedObjList;
        }

        private void getCoordDirs(float angle, out float xDir, out float yDir)
        {
            if (angle <= 90)
            {
                xDir = angle / 90;
            }
            else
            {
                xDir = 1 - (angle - 90) / 90;
            }
            yDir = angle / 90 - 1;
        }

        private Color4 castRay(float xDir, float yDir, int viewX, int viewY, int maxDistance, List<Objs.Obj> restrictedObjList)
        {
            double distance = 0;
            float distanceStep = 0.5f;
            int x = 0;
            int y = 0;
            float xx = x;
            float yy = y;

            while (distance < maxDistance)
            {
                x = Convert.ToInt32(Math.Floor(xx));
                y = Convert.ToInt32(Math.Floor(yy));

                //TODO: Put this foreach in special function
                foreach (Objs.Obj obj in restrictedObjList)
                {
                    if (obj.X == viewX + x && obj.Y == viewY + y && obj.GetType() != new Objs.Player(0, 0, null).GetType())
                    {
                        byte common = Convert.ToByte(255 - (255 / Math.Sqrt(maxDistance)) * Math.Sqrt(distance));//255 - (255 / maxDistance) * distance);
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

        private void drawAll(Color4[] colors)
        {
            //TODO: Eliminate the fisheye effect (if it'll appeare)
            int y = 0;

            foreach (Color4 color in colors)
            {
                for (int x = 0; x < Console.Cols; x++)
                {
                    drawSymbol(x, y, '█', color);
                }
                y++;
            }
        }
    }
}
