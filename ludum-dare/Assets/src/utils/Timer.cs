using System;

namespace schw3de.ld.utils
{
    public class Timer
    {
        private DateTime _startTime;
        private TimeSpan _duration;

        public Timer(TimeSpan duration)
        {
            _duration = duration;
        }

        public Timer()
        {
        }

        public void Start()
        {
            _startTime = DateTime.UtcNow;
        }

        public void Start(TimeSpan duration)
        {
            Start();
            _duration = duration;
        }

        public bool IsFinished()
        {
            return _startTime.Add(_duration) < DateTime.UtcNow;
        }
    }
}
