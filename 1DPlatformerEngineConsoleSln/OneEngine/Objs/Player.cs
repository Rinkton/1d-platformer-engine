using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneEngine.Objs
{
    public class Player : Obj
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool TurnedRight { get; private set; }

        Stopwatch stopwatch = new Stopwatch();

        private const int jumpMovePeriodStart = 500;
        private const int jumpPeriodChange = 100;
        private const int jumpMaximumPeriod = 800;

        private const int fallMovePeriodStart = 500;
        private const int fallPeriodChange = -100;

        private int moveIteration = 0;

        public Player(int x=0, int y=0) : base(x, y)
        {
            Width = 2;
            Height = 3;

            TurnedRight = true;
        }

        public override void Update()
        {
            #region jump and gravity
            int floorDistance = getVerticalDistance(true);

            //TODO: Only one timer for fall, jump and all another?! Care yourself, there are can be bugs!
            if(floorDistance > 0)
            {
                if(stopwatch.Activated == false)
                {
                    stopwatch.Restart();
                }
                else
                {
                    fall();
                }
            }
            else
            {
                moveIteration = 0;
            }
            #endregion
        }

        // These functions are have arguments, though they can don't, because it more easier to test //
        // TODO: Is message above is needed)?
        private int getVerticalDistance(bool floor)
        {
            int xx = X;

            int yy;
            if(floor)
            {
                yy = Y + Height;
            }
            else
            {
                yy = Y - 1;
            }
            int max = 8;

            for(int i = 0; i < max; i++)
            {
                //Count distance for anyone Obj
                foreach(Obj obj in ObjList.Content)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        if(obj.X == xx + x && obj.Y == yy + i && floor)
                        {
                            return i;
                        }
                        else if (obj.X == xx + x && obj.Y == yy - i && !floor)
                        {
                            return i;
                        }
                    }
                }
            }

            return max;
        }

        private void fall()
        {
            int fallMovePeriodActual = fallMovePeriodStart + (fallPeriodChange * moveIteration);
            if(fallMovePeriodActual < 1)
            {
                fallMovePeriodActual = 1; //If it 0 - it will crashed(zero division)
            }
            double movesPerFrameNonRounded = Convert.ToDouble(stopwatch.GetTime() / fallMovePeriodActual);
            int movesPerFrame = Convert.ToInt32(Math.Ceiling(movesPerFrameNonRounded));
            for (int i = 0; i < movesPerFrame; i++)
            {
                if(getVerticalDistance(true) > 0)
                {
                    move(Move.Nothing, Move.Positive);
                    moveIteration++;
                    stopwatch.Stop();
                }
            }
        }

        enum Move
        {
            Positive = 1,
            Nothing = 0,
            Negative = -1
        }

        private void move(Move moveX, Move moveY)
        {
            X += (int)moveX;
            Y += (int)moveY;
        }

        private void turn()
        {
            TurnedRight = !TurnedRight;
        }
    }
}
