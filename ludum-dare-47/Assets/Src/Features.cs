using System.Collections.Generic;

namespace schw3de.ld47
{
    public class Features : Singleton<Features>
    {
        static Features()
        {
            _dontDestroyOnLoad = true;
        }

        public float TreadmillSpeed { get; set; }

        public List<float> TreadmillSpeedAdditive { get; set; }

        public List<decimal> TreadmillSpeedCosts { get; set; }

        public bool AutomaticTreadmill { get; set; }

        public void Reset()
        {
            TreadmillSpeed = 1.5f;
            TreadmillSpeedAdditive = new List<float> { 0.5f, 0.4f };
            TreadmillSpeedCosts = new List<decimal> { 30, 100 };
            AutomaticTreadmill = false;
        }
    }
}
