using System;

namespace schw3de.ld47.utils
{
    public class Timer
    {
        private DateTime _startTime;

        private TimeSpan _timespan;

        public void SetTime(TimeSpan timespan)
        {
            _startTime = DateTime.UtcNow;
            _timespan = timespan;
        }

        public bool HasFinished()
        {
            return _startTime.Add(_timespan) < DateTime.UtcNow;
        }
    }
}
