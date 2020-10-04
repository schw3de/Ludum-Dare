using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld47
{
    public class Features : Singleton<Features>
    {
        static Features()
        {
            _dontDestroyOnLoad = true;
        }

        public float TreadmillSpeed { get; set; } = 1.5f;

        public List<float> TreadmillSpeedAdditive { get; set; } = new List<float> { 0.5f, 0.4f };

        public List<decimal>  TreadmillSpeedCosts { get; set; } = new List<decimal> { 30, 100 };

        public bool AutomaticTreadmill { get; set; }
    }
}
