using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld47
{
    public class Features : Singleton<Features>
    {
        public float TreadmillSpeed { get; set; } = 1.5f;

        public bool AutomaticTreadmill { get; set; }
    }
}
