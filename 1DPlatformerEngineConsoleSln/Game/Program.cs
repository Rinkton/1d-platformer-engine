using System;
using OneEngine;

namespace Game
{
    //TODO: Make a opportunity console can have colors
    //TODO: Make a Player more fat(more width)
    class Program
    {
        static void Main(string[] args)
        {
            OneEngine.Objs.Obj[] firstObjs =
                new OneEngine.Objs.Obj[]
                {
new OneEngine.Objs.Block(17, 15), new OneEngine.Objs.Block(18, 15), new OneEngine.Objs.Block(19, 15),
new OneEngine.Objs.Block(20, 15), new OneEngine.Objs.Block(21, 15), new OneEngine.Objs.Block(22, 15),
new OneEngine.Objs.Block(23, 15), new OneEngine.Objs.Block(30, 19), new OneEngine.Objs.Block(34, 19),
new OneEngine.Objs.Block(34, 20), new OneEngine.Objs.Block(34, 21), new OneEngine.Objs.Block(25, 22),
new OneEngine.Objs.Block(33, 22), new OneEngine.Objs.Block(34, 22), new OneEngine.Objs.Player(3+8, 25+1),
new OneEngine.Objs.Block(34, 23), new OneEngine.Objs.Block(25, 24), new OneEngine.Objs.Block(33, 24),
new OneEngine.Objs.Block(34, 24), new OneEngine.Objs.Block(18, 25), new OneEngine.Objs.Block(23, 25),
new OneEngine.Objs.Block(24, 25), new OneEngine.Objs.Block(25, 25), new OneEngine.Objs.Block(26, 25),
new OneEngine.Objs.Block(27, 25), new OneEngine.Objs.Block(28, 25), new OneEngine.Objs.Block(29, 25),
new OneEngine.Objs.Block(30, 25), new OneEngine.Objs.Block(31, 25), new OneEngine.Objs.Block(32, 25),
new OneEngine.Objs.Block(33, 25), new OneEngine.Objs.Block(34, 25), new OneEngine.Objs.Block(0, 26),
new OneEngine.Objs.Block(13, 26), new OneEngine.Objs.Block(34, 26), new OneEngine.Objs.Block(0, 27),
new OneEngine.Objs.Block(8, 27), new OneEngine.Objs.Block(34, 27), new OneEngine.Objs.Block(0, 28),
new OneEngine.Objs.Block(4, 28), new OneEngine.Objs.Block(18, 28), new OneEngine.Objs.Block(33, 28),
new OneEngine.Objs.Block(34, 28), new OneEngine.Objs.Block(0, 29), new OneEngine.Objs.Block(1, 29),
new OneEngine.Objs.Block(2, 29), new OneEngine.Objs.Block(3, 29), new OneEngine.Objs.Block(4, 29),
new OneEngine.Objs.Block(5, 29), new OneEngine.Objs.Block(6, 29), new OneEngine.Objs.Block(7, 29),
new OneEngine.Objs.Block(8, 29), new OneEngine.Objs.Block(9, 29), new OneEngine.Objs.Block(10, 29),
new OneEngine.Objs.Block(11, 29), new OneEngine.Objs.Block(12, 29), new OneEngine.Objs.Block(13, 29),
new OneEngine.Objs.Block(14, 29), new OneEngine.Objs.Block(15, 29), new OneEngine.Objs.Block(16, 29),
new OneEngine.Objs.Block(17, 29), new OneEngine.Objs.Block(18, 29), new OneEngine.Objs.Block(19, 29),
new OneEngine.Objs.Block(20, 29), new OneEngine.Objs.Block(21, 29), new OneEngine.Objs.Block(22, 29),
new OneEngine.Objs.Block(23, 29), new OneEngine.Objs.Block(24, 29), new OneEngine.Objs.Block(25, 29),
new OneEngine.Objs.Block(26, 29), new OneEngine.Objs.Block(27, 29), new OneEngine.Objs.Block(28, 29),
new OneEngine.Objs.Block(29, 29), new OneEngine.Objs.Block(30, 29), new OneEngine.Objs.Block(31, 29),
new OneEngine.Objs.Block(32, 29), new OneEngine.Objs.Block(33, 29), new OneEngine.Objs.Block(34, 29),
                };
            var visualizer = new ConsoleViewVisualizer(35, 36, "1D Platformer");

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
