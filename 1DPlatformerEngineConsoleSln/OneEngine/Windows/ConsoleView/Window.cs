using SunshineConsole;

namespace OneEngine.Windows.ConsoleView
{
    public class Window : Windows.Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowHeight">Count of rays that need to cast</param>
        /// <param name="windowWidth">The width to stretch a 1D line</param>
        /// <param name="windowName"></param>
        public Window(int width, int height, string name) : base(width, height, name)
        {
            ConsoleWindow console = new ConsoleWindow(Height, Width, Name);
            Visualizer = new Visualizer(console);
            KeyDetector = new Presets.KeyDetectorOpenTK();
        }
    }
}
