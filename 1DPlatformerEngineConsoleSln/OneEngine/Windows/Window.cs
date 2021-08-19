using OpenTK.Input;

namespace OneEngine.Windows
{
    /// <summary>
    /// Responsible for window creating, visualizing and key press detecting
    /// </summary>
    public abstract class Window
    {
        public readonly int Width;
        public readonly int Height;
        public readonly string Name;

        public Visualizer Visualizer;

        public KeyDetector KeyDetector;

        public Window(int width, int height, string name)
        {
            Width = width;
            Height = height;
            Name = name;
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
