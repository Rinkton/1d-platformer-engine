using SunshineConsole;

namespace OneEngine.Windows.ConsolePlatformer
{
    class Window : Windows.Window
    {
        public Window(int rows, int columns, string windowName)
        {
            ConsoleWindow console = new ConsoleWindow(rows, columns, windowName);
            Visualizer = new Visualizer(console);
            KeyDetector = new Presets.KeyDetectorOpenTK();
        }
    }
}
