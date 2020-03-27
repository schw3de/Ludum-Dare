using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source
{
    public class Timer
    {
        private DateTime endTime;

        public void SetTime(double seconds)
        {
            endTime = DateTime.Now.AddSeconds(seconds);
        }

        public bool IsFinished()
        {
            return endTime < DateTime.Now;
        }
    }
}
