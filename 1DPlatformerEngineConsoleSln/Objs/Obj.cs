﻿

namespace Objs // TODO: I guess, this project is redundant
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
