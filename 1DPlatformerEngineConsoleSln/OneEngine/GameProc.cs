using System.Collections.Generic;

namespace OneEngine
{
    public class GameProc
    {
        public bool EndGame = false;

        private List<Obj> objList;
        private IVisualizer visualizer;

        public GameProc(Obj[] firstObjs, IVisualizer visualizer)
        {
            objList = new List<Obj>(firstObjs);
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
            }
        }
    }
}
