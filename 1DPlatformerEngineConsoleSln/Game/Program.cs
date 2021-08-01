using OneEngine;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            OneEngine.Objs.Obj[] firstObjs = 
                new OneEngine.Objs.Obj[] 
                {
                    new OneEngine.Objs.Player(1, 4), new OneEngine.Objs.Block(0, 7), new OneEngine.Objs.Block(1, 7),
                    new OneEngine.Objs.Block(2, 7), new OneEngine.Objs.Block(3, 6), new OneEngine.Objs.Block(4, 6),
                    new OneEngine.Objs.Block(6, 2), new OneEngine.Objs.Block(7, 1), new OneEngine.Objs.Block(8, 1),
                    new OneEngine.Objs.Block(1, 1)
                };
            var visualizer = new ConsolePlatformerVisualizer(8, 8, "Platformer(non 1D)");

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
