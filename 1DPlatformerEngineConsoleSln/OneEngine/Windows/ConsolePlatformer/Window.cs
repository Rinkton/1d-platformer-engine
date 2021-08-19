using SunshineConsole;

namespace OneEngine.Windows.ConsolePlatformer
{
    class Window : Windows.Window
    {
        public Window(int width, int height, string name) : base(width, height, name)
        {
            ConsoleWindow console = new ConsoleWindow(Height, Width, Name);
            Visualizer = new Visualizer(console);
            KeyDetector = new Presets.KeyDetectorOpenTK();
        }
    }
}
