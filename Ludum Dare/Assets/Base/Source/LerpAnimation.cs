using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.Base.Source
{
    public class LerpAnimation : Singleton<LerpAnimation>
    {
        public void Move(GameObject gameObject, Vector3 to, float lerpTime, LerpAnimationType type, Action finished)
        {
            StartCoroutine(this.StartAnimation(gameObject, to, lerpTime, type, finished));
        }

        private IEnumerator StartAnimation(GameObject gameObject, Vector3 to, float animationDuration, LerpAnimationType type, Action finished)
        {
            float currentDuration = 0;

            while(Vector3.Distance(gameObject.transform.position, to) > 0.5f)
            {
                yield return new WaitForSeconds(0.01f);

                currentDuration += Time.deltaTime;

                if(currentDuration > animationDuration)
                {
                    currentDuration = animationDuration;
                }

                float t = currentDuration / animationDuration;

                switch (type)
                {
                    case LerpAnimationType.Straight:
                        break;

                    case LerpAnimationType.Curve:
                        t = Mathf.Sin(t * Mathf.PI * 0.5f);
                        break;
                }

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, to, t);
            }

            gameObject.transform.position = to;

            finished();
        }
    }
}
