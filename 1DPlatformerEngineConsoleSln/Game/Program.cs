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
                    new OneEngine.Objs.Block(0, 5), new OneEngine.Objs.Block(1, 5), new OneEngine.Objs.Block(2, 5),
                    new OneEngine.Objs.Block(4, 6), new OneEngine.Objs.Block(5, 6), new OneEngine.Objs.Block(12, 6),
                    new OneEngine.Objs.Player(1, -1), new OneEngine.Objs.Block(5, 0), new OneEngine.Objs.Block(6, 0)
                };
            var visualizer = new ConsolePlatformerVisualizer(8, 8, "Platformer(non 1D)");

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
