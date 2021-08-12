using System.Collections.Generic;
using System.Threading;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private Visualizer visualizer;
        private Visualizer nonVisualizer;

        public GameProc(Objs.Obj[] firstObjs, Visualizer visualizer)
        {
            ObjList.SetContent(new List<Objs.Obj>(firstObjs));
            ObjList.UpdateContent();
            this.visualizer = visualizer;
            nonVisualizer = new ConsolePlatformerVisualizer(35, 36, "non 1D Platformer");
        }

        public void Run()
        {
            ObjList.GetContent().ForEach(obj => obj.Start());

            while(true)
            {
                if(EndGame)
                {
                    break;
                }

                EndGame = KeyChecker.Escape || visualizer.Main();
                EndGame = KeyChecker.Escape || nonVisualizer.Main();
                visualizer.SetKeys();
                ObjList.GetContent().ForEach(obj => obj.Update());
                ObjList.UpdateContent();

                //TODO: Need to upgrade it: add a Stopwatch instead Sleep
                Thread.Sleep(1000 / Configurator.Fps);
            }
        }
    }
}
