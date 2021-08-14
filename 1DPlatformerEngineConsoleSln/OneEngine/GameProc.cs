using System.Collections.Generic;
using System.Threading;
using OpenTK.Input;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private Windows.Window window;
        private Windows.Window platformerWindow;

        public GameProc(Objs.Obj[] firstObjs, Windows.Window window)
        {
            ObjList.SetContent(new List<Objs.Obj>(firstObjs));
            ObjList.UpdateContent();
            this.window = window;
            platformerWindow = new Windows.ConsolePlatformer.Window(35, 36, "non 1D Platformer");
        }

        public void Run()
        {
            ObjList.GetContent().ForEach(obj => obj.Start());

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
                ObjList.GetContent().ForEach(obj => obj.Update());
                ObjList.UpdateContent();

                int remainingTime = (1000 / Configurator.Fps) - betweenIterationsStopwatch.GetTime();
                if(remainingTime > 0)
                {
                    Thread.Sleep(remainingTime);
                }
                betweenIterationsStopwatch.RestartAsync();
            }
        }
    }
}
