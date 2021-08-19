using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenTK.Input;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private Windows.Window window;
        private Windows.Window platformerWindow;

        public GameProc(List<Objs.Obj> firstObjList, Windows.Window window)
        {
            this.window = window;
            platformerWindow = new Windows.ConsolePlatformer.Window(35, 36, "non 1D Platformer");
            List<Objs.Obj>[,] firstObjListMap = convertObjListIntoObjListMap(firstObjList);
            ObjMap.SetContent(firstObjListMap.Clone() as List<Objs.Obj>[,]);
            ObjMap.UpdateContent();
        }

        public void Run()
        {
            foreach(List<Objs.Obj> objList in ObjMap.GetContent())
            {
                objList.ForEach(obj => obj.Start());
            }

            // Count time between loop iterations for stable FPS
            Stopwatch betweenIterationsStopwatch = new Stopwatch();
            betweenIterationsStopwatch.RestartAsync();

            while(true)
            {
                if(EndGame)
                {
                    break;
                }

                KeyboardState keyboard = window.KeyDetector.GetKeyboard();
                EndGame = keyboard.IsKeyDown(Key.Escape) || window.Main() != Windows.Result.Ok;
                EndGame = keyboard.IsKeyDown(Key.Escape) || platformerWindow.Main() != Windows.Result.Ok;
                foreach (List<Objs.Obj> objList in ObjMap.GetContent())
                {
                    objList.ForEach(obj => obj.Update());
                }
                ObjMap.UpdateContent();

                int remainingTime = (1000 / Configurator.Fps) - betweenIterationsStopwatch.GetTime();
                if(remainingTime > 0)
                {
                    Thread.Sleep(remainingTime);
                }
                betweenIterationsStopwatch.RestartAsync();
            }
        }

        private List<Objs.Obj>[,] convertObjListIntoObjListMap(List<Objs.Obj> objList)
        {
            int yLength = objList.Max(obj => obj.Y) + 1;
            int xLength = objList.Max(obj => obj.X) + 1;
            List<Objs.Obj>[,] objListMap = new List<Objs.Obj>[yLength, xLength];
            for(int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    objListMap[y, x] = new List<Objs.Obj>();
                }
            }

            foreach(Objs.Obj obj in objList)
            {
                objListMap[obj.Y, obj.X].Add(obj);
            }

            return objListMap;
        }
    }
}
