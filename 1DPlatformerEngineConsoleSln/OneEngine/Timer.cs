using System;
using System.Timers;

namespace OneEngine
{
    class Stopwatch
    {
        public bool Activated { get; private set; }

        private DateTime startTime;

        public Stopwatch()
        {
            Activated = false;
        }

        public void Restart()
        {
            startTime = DateTime.Now;
            Activated = true;
        }

        /// <summary>
        /// Time in milliseconds.
        /// </summary>
        public int GetTime()
        {
            if(Activated)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                return Convert.ToInt32(elapsed.TotalMilliseconds);
            }
            else
            {
                return 0;
            }
        }

        public void Stop()
        {
            Activated = false;
        }
    }
}
