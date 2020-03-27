using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.MRnothing
{
    public class DisappearManager
    {
        private Action disappearCallback;

        private Timer timer;

        public DisappearManager(Action disappearCallback)
        {
            this.timer = new Timer();
            this.disappearCallback = disappearCallback;
        }
        public void SetTimeToDisappear(double seconds)
        {
            this.timer.Start(seconds, this.disappearCallback);
        }
    }
}
