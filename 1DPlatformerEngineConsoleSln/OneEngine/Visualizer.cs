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
        //TODO: Why "Visualizer" if it's responsible for key detecting too?
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Visualize failed?</returns>
        public abstract bool Main();

        public abstract void SetKeys();
    }
}
