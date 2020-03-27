using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.Base.Source
{
    public class Timer
    {
        private Action timerFinishedCallback;

        private DateTime endTime;

        private double waitTimeUntilCheck = 0.5f;

        private bool cancel;

        public void Start(double seconds, Action callback)
        {
            this.timerFinishedCallback = callback;

            if (waitTimeUntilCheck > seconds)
                waitTimeUntilCheck = seconds;

            this.endTime = DateTime.Now.AddSeconds(seconds);

            CoroutineProvider.Instance.StartCoroutine(this.RunTimer());
        }

        public void Cancel()
        {
            this.cancel = true;
        }

        private IEnumerator RunTimer()
        {
            while(endTime >= DateTime.Now)
            {
                if (timerFinishedCallback == null || this.cancel)
                    yield return null;

                yield return new WaitForSeconds((float)waitTimeUntilCheck);
            }

            if (timerFinishedCallback != null && !this.cancel)
                this.timerFinishedCallback();
        }
    }
}
