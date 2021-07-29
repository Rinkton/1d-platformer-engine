using OneEngine;
using Objs;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Obj[] firstObjs = 
                new Obj[] 
                {
                    new Block(0, 7), new Block(1, 7), new Block(2, 7),
                    new Block(4, 6), new Block(5, 6), new Block(12, 6),
                    new Player(1, 6)
                };
            var visualizer = new ConsolePlatformerVisualizer(8, 8, "Platformer(non 1D)");

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
