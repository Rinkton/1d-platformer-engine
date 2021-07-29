﻿using OneEngine;
using Objs;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Obj[] firstObjs = new Obj[] { };
            var visualizer = new ConsolePlatformerVisualizer();

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
