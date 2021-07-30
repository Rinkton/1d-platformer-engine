using System.Collections.Generic;
using System.Threading;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private List<Objs.Obj> objList;
        private Visualizer visualizer;

        public GameProc(Objs.Obj[] firstObjs, Visualizer visualizer)
        {
            objList = new List<Objs.Obj>(firstObjs);
            this.visualizer = visualizer;
        }

        public void Run()
        {
            objList.ForEach(obj => obj.Start());

            while(true)
            {
                if(EndGame)
                {
                    break;
                }

                visualizer.Main(objList.ToArray());
                objList.ForEach(obj => obj.Update());

                Thread.Sleep(1000 / Configurator.Fps);
            }
        }
    }
}
