using _1DPlatformerEngineConsole;
//TODO: It's worth this underscore?
using Common;
using VisualizerPresets;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Obj[] firstObjs = new Obj[] { };
            var visualizer = new VisualizerPresets.ConsolePlatformer();

            GameProc gameProc = new GameProc(firstObjs, visualizer);
            gameProc.Run();
        }
    }
}
