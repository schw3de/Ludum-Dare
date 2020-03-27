using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.MRnothing
{
    public class Timer
    {
        private Action timerFinishedCallback;

        private DateTime endTime;

        private double waitTimeUntilCheck = 0.5f;

        private Coroutine coroutine;

        public void Start(double seconds, Action callback)
        {
            this.Cancel();

            this.timerFinishedCallback = callback;

            if (waitTimeUntilCheck > seconds)
                waitTimeUntilCheck = seconds;

            this.endTime = DateTime.Now.AddSeconds(seconds);

            this.coroutine = CoroutineStarter.Instance.StartCoroutine(this.RunTimer());
        }

        public void Cancel()
        {
            if (this.coroutine != null)
            {
                CoroutineStarter.Instance.StopCoroutine(this.coroutine);
            }
        }

        private IEnumerator RunTimer()
        {
            while (endTime >= DateTime.Now)
            {
                if (timerFinishedCallback == null)
                    yield return null;

                yield return new WaitForSeconds((float)waitTimeUntilCheck);
            }

            if (timerFinishedCallback != null)
                this.timerFinishedCallback();
        }
    }
}
