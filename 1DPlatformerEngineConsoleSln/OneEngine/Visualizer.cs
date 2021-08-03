using OpenTK.Input;

namespace OneEngine
{
    /// <summary>
    /// Also responsible for key detecting...
    /// </summary>
    public abstract class Visualizer
    {
        public Visualizer()
        {

        }

        public abstract void Main();

        public abstract void SetKeys();
    }
}
