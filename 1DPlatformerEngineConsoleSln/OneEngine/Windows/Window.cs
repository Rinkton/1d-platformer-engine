using OpenTK.Input;

namespace OneEngine.Windows
{
    /// <summary>
    /// Responsible for window creating, visualizing and key press detecting
    /// </summary>
    public abstract class Window
    {
        public Visualizer Visualizer;

        public KeyDetector KeyDetector;

        public Window()
        {

        }

        public virtual Result Main()
        {
            bool visualizedSuccessfully = Visualizer.Visualize();
            if (visualizedSuccessfully)
            {
                return Result.VisualizeFailed;
            }

            if (KeyDetector.GetKeyboard().IsKeyDown(Key.Escape))
            {
                return Result.Exit;
            }

            return Result.Ok;
        }
    }
}
