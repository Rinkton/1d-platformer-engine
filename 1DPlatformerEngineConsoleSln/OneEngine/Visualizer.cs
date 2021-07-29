using Objs;

namespace OneEngine
{
    public abstract class Visualizer
    {
        public Visualizer(int rows, int columns, string windowName)
        {

        }

        public abstract void Main(Obj[] objs);
    }
}
