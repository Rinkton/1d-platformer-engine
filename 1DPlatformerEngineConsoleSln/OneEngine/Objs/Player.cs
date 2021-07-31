using System;
using OpenTK.Input;

namespace OneEngine.Objs
{
    public class Player : Obj
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool TurnedRight { get; private set; }

        private const int jumpMovePeriodStart = 500;
        private const int jumpPeriodChange = 100;
        private const int jumpMaximumPeriod = 800;
        Stopwatch jumpStopwatch = new Stopwatch();
        private bool jumping = false;
        private int jumpMoveIteration = 0;

        private const int fallMovePeriodStart = 500;
        private const int fallPeriodChange = -100;
        Stopwatch fallStopwatch = new Stopwatch();
        private int fallMoveIteration = 0;

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
                if(fallStopwatch.Activated == false)
                {
                    fallStopwatch.Restart();
                }
                else
                {
                    fall();
                }
            }
            else
            {
                fallMoveIteration = 0;
            }
            if((KeyKeeper.Key == Key.Space && floorDistance == 0) || jumping)
            {
                if (jumpStopwatch.Activated == false)
                {
                    jumpStopwatch.Restart();
                    jumping = true;
                }
                else
                {
                    jump();
                }
            }
            else
            {
                jumpMoveIteration = 0;
            }
            #endregion
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

        private void fall()
        {
            int fallMovePeriodActual = fallMovePeriodStart + (fallPeriodChange * jumpMoveIteration);
            if(fallMovePeriodActual < 1)
            {
                fallMovePeriodActual = 1; //If it 0 - it will crashed(zero division)
            }
            double movesPerFrameNonRounded = Convert.ToDouble(fallStopwatch.GetTime() / fallMovePeriodActual);
            int movesPerFrame = Convert.ToInt32(Math.Ceiling(movesPerFrameNonRounded));
            for (int i = 0; i < movesPerFrame; i++)
            {
                if(getVerticalDistance(true) > 0)
                {
                    move(Move.Nothing, Move.Positive);
                    fallMoveIteration++;
                    fallStopwatch.Stop();

                    // if we have more than one move per this frame, then it useful
                    fallMovePeriodActual = fallMovePeriodStart + (fallPeriodChange * fallMoveIteration);
                }
            }
        }

        private void jump()
        {
            int jumpMovePeriodActual = jumpMovePeriodStart + (jumpPeriodChange * jumpMoveIteration);
            if (jumpMovePeriodActual < 1)
            {
                jumpMovePeriodActual = 1; //If it 0 - it will crashed(zero division)
            }
            double movesPerFrameNonRounded = Convert.ToDouble(jumpStopwatch.GetTime() / jumpMovePeriodActual);
            int movesPerFrame = Convert.ToInt32(Math.Ceiling(movesPerFrameNonRounded));
            ///!!!
            int maximumMoves = (jumpMaximumPeriod - jumpMovePeriodActual) / jumpPeriodChange;
            //TODO: If per frame happened many events, then it do what can in this case, but remaining events...
            //TODO: ...don't give to others.
            if (movesPerFrame > maximumMoves)
            {
                movesPerFrame = maximumMoves;
                jumping = false;
            }
            ///!!!
            for (int i = 0; i < movesPerFrame; i++)
            {
                if (getVerticalDistance(false) > 0) //!!! false
                {
                    move(Move.Nothing, Move.Negative);
                    jumpMoveIteration++;
                    jumpStopwatch.Stop();

                    // if we have more than one move per this frame, then it useful
                    jumpMovePeriodActual = jumpMovePeriodStart + (jumpPeriodChange * jumpMoveIteration);
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
