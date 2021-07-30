using OpenTK.Input;

namespace OneEngine
{
    /// <summary>
    /// Also responsible for key detecting...
    /// </summary>
    public abstract class Visualizer
    {
        public Visualizer(int rows, int columns, string windowName)
        {

        }

        public abstract void Main(Objs.Obj[] objs);

        public abstract Key GetKey();
    }
}
