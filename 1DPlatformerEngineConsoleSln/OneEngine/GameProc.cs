using System.Collections.Generic;
using System.Threading;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private Visualizer visualizer;
        //TODO: Delete this
        private Visualizer nonVisualizer = new ConsolePlatformerVisualizer(16, 36, "non 1D Platformer");

        public GameProc(Objs.Obj[] firstObjs, Visualizer visualizer)
        {
            ObjList.SetContent(new List<Objs.Obj>(firstObjs));
            ObjList.UpdateContent();
            this.visualizer = visualizer;
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

                visualizer.Main();
                nonVisualizer.Main();
                visualizer.SetKeys();
                ObjList.GetContent().ForEach(obj => obj.Update());
                ObjList.UpdateContent();

                Thread.Sleep(1000 / Configurator.Fps);
            }
        }
    }
}
