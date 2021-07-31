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
        private readonly Stopwatch jumpStopwatch = new Stopwatch();
        private int jumpIteration = 0;
        private const int jumpPeriodMaximum = 800;
        private bool jumping = false;

        private const int fallPeriodStart = 500;
        private const int fallPeriodChange = -100;
        private readonly Stopwatch fallStopwatch = new Stopwatch();
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

            if(jumping == false)
            {
                MoveResult fallResult = move(fallPeriodStart, fallPeriodChange, fallStopwatch,
                fallIteration, ObjMoveType.Nothing, ObjMoveType.Positive);
                switch (fallResult)
                {
                    case MoveResult.NotTimeYet:
                        break;
                    case MoveResult.ReachedObstacle:
                    case MoveResult.BoostEnded:
                        fallIteration = 0;
                        break;
                    case MoveResult.Ok:
                        fallIteration++;
                        break;
                }
            }

            if((KeyKeeper.Key == Key.Space && floorDistance == 0) || jumping)
            {
                MoveResult jumpResult = move(jumpPeriodStart, jumpPeriodChange, jumpStopwatch, 
                    jumpIteration, ObjMoveType.Nothing, ObjMoveType.Negative, jumpPeriodMaximum);
                switch(jumpResult)
                {
                    case MoveResult.NotTimeYet:
                        break;
                    case MoveResult.ReachedObstacle:
                    case MoveResult.BoostEnded:
                        jumpIteration = 0;
                        jumping = false;
                        break;
                    case MoveResult.Ok:
                        jumpIteration++;
                        jumping = true;
                        break;
                }
            }
            else
            {
                jumpIteration = 0;
            }
            #endregion
        }

        enum MoveResult
        {
            NotTimeYet,
            ReachedObstacle,
            BoostEnded,
            Ok
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
        /// <returns></returns>
        private MoveResult move(int periodStart, int periodChange, Stopwatch stopwatch, 
            int iteration, ObjMoveType x, ObjMoveType y, int periodMaximum = 0)
        {
            bool firstMove = false;
            if(iteration == 0)
            {
                firstMove = true;
            }
            
            if(stopwatch.Activated == false)
            {
                stopwatch.Restart();
            }

            int periodActual = periodStart + (periodChange * iteration);

            bool moveDown = y == ObjMoveType.Positive;
            if (getVerticalDistance(moveDown) == 0)
            {
                return MoveResult.ReachedObstacle;
            }
            if (stopwatch.GetTime() <= periodActual && firstMove == false)
            {
                return MoveResult.NotTimeYet;
            }

            if (periodActual < 1)
            {
                periodActual = 1; //If it 0 - it will crashed(zero division)
            }

            bool periodIsSet = periodMaximum != 0;
            if(periodIsSet && periodActual >= periodMaximum)
            {
                return MoveResult.BoostEnded;
            }

            objMove(x, y);
            stopwatch.Stop();

            return MoveResult.Ok;
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
