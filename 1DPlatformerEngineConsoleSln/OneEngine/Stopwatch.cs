using System;
using System.Threading.Tasks;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">If you declare it, then after pointed time is end, stopwatch stopped</param>
        public async void RestartAsync(int time = 0)
        {
            startTime = DateTime.Now;
            Activated = true;
            if(time != 0)
            {
                await Task.Run(() => waitTimeEnded(time));
            }
        }

        /// <summary>
        /// Get time in milliseconds.
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

        private void waitTimeEnded(int time)
        {
            while(true)
            {
                if (GetTime() > time)
                {
                    Stop();
                    break;
                }
            }
        }
    }
}
