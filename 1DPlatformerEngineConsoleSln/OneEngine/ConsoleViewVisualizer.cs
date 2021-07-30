using OpenTK.Input;

namespace OneEngine
{
    public class ConsoleViewVisualizer : Visualizer
    {
        public ConsoleViewVisualizer(int rows, int columns, string windowName) : base(rows, columns, windowName)
        {

        }

        public override void Main(Objs.Obj[] objs)
        {

        }

        public override Key GetKey()
        {
            throw new System.NotImplementedException();
        }
    }
}
