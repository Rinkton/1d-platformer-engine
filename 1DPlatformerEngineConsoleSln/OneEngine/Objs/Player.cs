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

        private const int walkPeriodStart = 500;
        private const int walkPeriodChange = -50;
        private readonly Stopwatch walkStopwatch = new Stopwatch();
        private int walkIteration = 0;
        private const int walkPeriodMaximum = 400;
        #endregion

        public Player(int x=0, int y=0) : base(x, y)
        {
            Width = 2;
            Height = 3;

            TurnedRight = true;
        }
        //TODO: Fucking problems with collision
        public override void Update()
        {
            #region jump and gravity
            int floorDistance = getDistance(ObjMoveType.Down);

            if(jumping == false)
            {
                MoveResult fallResult = move(fallPeriodStart, fallPeriodChange, fallStopwatch,
                fallIteration, ObjMoveType.Down);
                switch (fallResult)
                {
                    case MoveResult.NotTimeYet:
                        break;
                    case MoveResult.ReachedObstacle:
                        fallIteration = 0;
                        break;
                    case MoveResult.Ok:
                        fallIteration++;
                        break;
                }
            }

            if((KeyChecker.Space && floorDistance == 0) || jumping)
            {
                MoveResult jumpResult = move(jumpPeriodStart, jumpPeriodChange, jumpStopwatch, 
                    jumpIteration, ObjMoveType.Up, jumpPeriodMaximum);
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

            if (KeyChecker.W || KeyChecker.S)
            {
                ObjMoveType moveDirection = (KeyChecker.W ^ TurnedRight) 
                    ? ObjMoveType.Left : ObjMoveType.Right;
                MoveResult walkResult = move(walkPeriodStart, walkPeriodChange, walkStopwatch,
                    walkIteration, moveDirection, walkPeriodMaximum);
                switch (walkResult)
                {
                    case MoveResult.NotTimeYet:
                        break;
                    case MoveResult.ReachedObstacle:
                        walkIteration = 0;
                        break;
                    case MoveResult.BoostEnded:
                        break;
                    case MoveResult.Ok:
                        walkIteration++;
                        break;
                }
            }
            else
            {
                walkIteration = 0;
            }
            //TODO: Turn
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
            int iteration, ObjMoveType objMoveType, int periodMaximum = 0)
        {
            //TODO: But fall must have no
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

            if (getDistance(objMoveType) == 0)
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

            //TODO: With jump it so bad go to up
            objMove(objMoveType);
            stopwatch.Stop();

            bool periodIsSet = periodMaximum != 0;
            if(periodIsSet && periodActual == periodMaximum)
            {
                return MoveResult.BoostEnded;
            }

            return MoveResult.Ok;
        }

        private int getDistance(ObjMoveType objMoveType)
        {
            int x = 0;
            int y = 0;
            switch (objMoveType)
            {
                case ObjMoveType.Up:
                    y += Y + -1;
                    break;
                case ObjMoveType.Right:
                    x += X + Width;
                    break;
                case ObjMoveType.Down:
                    y += Y + Height;
                    break;
                case ObjMoveType.Left:
                    x += X + -1;
                    break;
            }
            bool vertical = objMoveType == ObjMoveType.Up || objMoveType == ObjMoveType.Down;
            // Is calculating distance go along positive coordinates
            bool byPositive = objMoveType == ObjMoveType.Right || objMoveType == ObjMoveType.Down;

            int max = 8;

            for (int i = 0; i < max; i++)
            {
                //Count distance for anyone Obj
                foreach (Obj obj in ObjList.Content)
                {
                    if(vertical)
                    {
                        for (int xx = 0; xx < Width; xx++)
                        {
                            if(obj.X == xx + x && obj.Y == y + i && byPositive)
                            {
                                return i;
                            }
                            else if(obj.X == xx + x && obj.Y == y - i && !byPositive)
                            {
                                return i;
                            }
                        }
                    }
                    else
                    {
                        for (int yy = 0; yy < Height; yy++)
                        {
                            if(obj.X == x + i && obj.Y == yy + y && byPositive)
                            {
                                return i;
                            }
                            else if(obj.X == x - i && obj.Y == yy + y && !byPositive)
                            {
                                return i;
                            }
                        }
                    }
                }
            }

            return max;
        }

        enum ObjMoveType
        {
            Up,
            Right,
            Down,
            Left
        }

        /// <summary>
        /// Move Player like just Obj in ObjList.Content
        /// </summary>
        /// <param name="moveX"></param>
        /// <param name="moveY"></param>
        private void objMove(ObjMoveType objMoveType)
        {
            switch(objMoveType)
            {
                case ObjMoveType.Up:
                    X += 0;
                    Y += -1;
                    break;
                case ObjMoveType.Right:
                    X += 1;
                    Y += 0;
                    break;
                case ObjMoveType.Down:
                    X += 0;
                    Y += 1;
                    break;
                case ObjMoveType.Left:
                    X += -1;
                    Y += 0;
                    break;
            }
        }

        private void turn()
        {
            TurnedRight = !TurnedRight;
        }
    }
}
