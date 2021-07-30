using System.Collections.Generic;
using System.Threading;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private Visualizer visualizer;

        public GameProc(Objs.Obj[] firstObjs, Visualizer visualizer)
        {
            ObjList.Content = new List<Objs.Obj>(firstObjs);
            this.visualizer = visualizer;
        }

        public void Run()
        {
            ObjList.Content.ForEach(obj => obj.Start());

            while(true)
            {
                if(EndGame)
                {
                    break;
                }

                visualizer.Main(ObjList.Content.ToArray());
                KeyKeeper.Key = visualizer.GetKey();
                ObjList.Content.ForEach(obj => obj.Update());

                Thread.Sleep(1000 / Configurator.Fps);
            }
        }
    }
}
