using System;
using System.Collections.Generic;
using Common;
using Visualizers;

namespace _1DPlatformerEngineConsole
{
    public class GameProc
    {
        private List<Obj> objList;
        private IVisualizer visualizer;
        private bool endGame = false;

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
                if(endGame)
                {
                    break;
                }

                visualizer.Main(objList.ToArray());
                objList.ForEach(obj => obj.Update());
            }
        }
    }
}
