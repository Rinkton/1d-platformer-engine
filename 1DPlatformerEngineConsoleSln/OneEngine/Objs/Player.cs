using System;
using OpenTK.Input;

namespace OneEngine.Objs
{
    public class Player : Obj
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool TurnedRight { get; private set; }

        #region variables for moving (their description contains in move method, in XML comment)
        private const int jumpPeriodStart = 500;
        private const int jumpPeriodChange = 100;
        Stopwatch jumpStopwatch = new Stopwatch();
        private int jumpIteration = 0;
        private const int jumpMaximumPeriod = 800;
        private bool jumping = false;

        private const int fallPeriodStart = 500;
        private const int fallPeriodChange = -100;
        Stopwatch fallStopwatch = new Stopwatch();
        private int fallIteration = 0;
        #endregion

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

            if(floorDistance > 0 && jumping == false)
            {
                move(fallPeriodStart)
            }
            if((KeyKeeper.Key == Key.Space && floorDistance == 0) || jumping)
            {

            }
            #endregion
        }

        /// <summary>
        /// Responsible for any kind of Player's moving
        /// </summary>
        /// <param name="periodStart">Time in milliseconds player moving at start</param>
        /// <param name="periodChange">Time in milliseconds player boosting every iteration</param>
        /// <param name="stopwatch">Stopwatch for get time exacty for this moving</param>
        /// <param name="iteration">Count of times player moving this type. Needed for OK work of boosting</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="periodMaximum">Time in milliseconds. Reaching this period method function 
        /// returns that boosting ends</param>
        /// <returns>boostEnd. If it have periodMaximum and if this one reached, then it will returns true</returns>
        private bool move(int periodStart, int periodChange, Stopwatch stopwatch, 
            int iteration, ObjMoveType x, ObjMoveType y, int periodMaximum = 0)
        {
            return false;
        }

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

        enum ObjMoveType
        {
            Positive = 1,
            Nothing = 0,
            Negative = -1
        }

        /// <summary>
        /// Move Player like just Obj in ObjList.Content
        /// </summary>
        /// <param name="moveX"></param>
        /// <param name="moveY"></param>
        private void objMove(ObjMoveType moveX, ObjMoveType moveY)
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
