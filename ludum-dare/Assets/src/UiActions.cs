using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{ 
    public class UiActions
    {
        Action StartInevitableMode { get; set; }

        public UiActions(Action startInevitableMode)
        {
            StartInevitableMode = startInevitableMode;
        }
    }
}
