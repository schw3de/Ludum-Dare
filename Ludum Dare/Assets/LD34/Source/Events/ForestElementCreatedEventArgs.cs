using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schw3de.LD34.Source.Events
{
    public class ForestElementCreatedEventArgs : EventArgs
    {
        public ForestElement ForestElement { get; set; }

        public int AmountForestElements { get; set; }
    }
}
