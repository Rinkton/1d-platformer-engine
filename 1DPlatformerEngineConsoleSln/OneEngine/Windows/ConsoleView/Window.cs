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
        public Window(int windowHeight, int windowWidth, string windowName)
        {
            ConsoleWindow console = new ConsoleWindow(windowHeight, windowWidth, windowName);
            Visualizer = new Visualizer(console);
            KeyDetector = new Presets.KeyDetectorOpenTK();
        }
    }
}
