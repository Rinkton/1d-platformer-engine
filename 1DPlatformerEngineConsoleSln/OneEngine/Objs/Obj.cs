

namespace OneEngine.Objs
{
    public class Obj
    {
        public int X;
        public int Y;

        public Obj(int x, int y)
        {
            X = x;
            Y = y;
        }

        public virtual void Start() { }

        public virtual void Update() { }
    }
}
