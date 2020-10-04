using System.Collections.Generic;

namespace schw3de.ld47
{
    public class Features : Singleton<Features>
    {
        static Features()
        {
            _dontDestroyOnLoad = true;
        }

        public int ScannerSpeedSeconds { get; set; }

        public List<(decimal, int)> ScannerSpeedCosts { get; set; }

        public float TreadmillSpeed { get; set; }

        public List<float> TreadmillSpeeds { get; set; }

        public List<decimal> TreadmillSpeedCosts { get; set; }

        public bool AutomaticTreadmill { get; set; }

        public void Reset()
        {
            ScannerSpeedSeconds = 4;
            ScannerSpeedCosts = new List<(decimal, int)> { (20, 2), (30, 1)};

            TreadmillSpeed = 1.2f;
            TreadmillSpeeds = new List<float> { 1.7f, 2.2f, 2.7f };
            TreadmillSpeedCosts = new List<decimal> { 20, 30, 40 };

            AutomaticTreadmill = false;
        }
    }
}
