

namespace OneEngine.Objs
{
    public class Obj
    {
        public int X;
        public int Y;

        //TODO: Replace class names in XML comments via <see/>
        /// <summary>
        /// Create Obj instance, but don't add it in <see cref="ObjMap"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Obj(int x=0, int y=0)
        {
            X = x;
            Y = y;
        }

        public virtual void Start() { }

        public virtual void Update() { }
    }
}
