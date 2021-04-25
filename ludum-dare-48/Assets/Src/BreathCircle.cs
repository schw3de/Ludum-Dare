using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld48
{
    public class BreathCircle : Singleton<BreathCircle>
    {
        public SpriteRenderer SpriteRenderer;
        public BreathCircleState State;

        private bool _increase;
        private Action _callback;
        private float _durationBreathIn = 4;
        private float _durationBreathOut = 4.5f;
        private Coroutine _currentLerp;

        private void Start()
        {
            if(State == BreathCircleState.Empty && SpriteRenderer.color.a != 0)
            {
                SpriteRenderer.SetAlpha(0);
            }
        }

        public void BreathIn(Action callback)
        {
            if(_currentLerp != null)
            {
                StopCoroutine(_currentLerp);
            }

            _callback = callback;
            var targetAlpha = SpriteRenderer.color;
            targetAlpha.a = 1;
            var duration = _durationBreathIn;
            if(SpriteRenderer.color.a != 0)
            {
                duration = Math.Abs(SpriteRenderer.color.a - 1) * _durationBreathIn;
            }
            Debug.Log($"Breath in with duration: {duration}");
            _currentLerp = StartCoroutine(LerpAlpha(SpriteRenderer.color, targetAlpha, duration));
            State = BreathCircleState.BreathIn;
        }

        public void BreathOut(Action callback)
        {
            if (_currentLerp != null)
            {
                StopCoroutine(_currentLerp);
            }

            _callback = callback;
            var targetAlpha = SpriteRenderer.color;
            targetAlpha.a = 0;

            var duration = _durationBreathOut;
            if(SpriteRenderer.color.a != 1)
            {
                duration = SpriteRenderer.color.a * _durationBreathOut;
                // duration = Math.Abs(SpriteRenderer.color.a - 1) * _durationBreathOut;
            }

            Debug.Log($"Breath out with duration: {duration}");
            _currentLerp = StartCoroutine(LerpAlpha(SpriteRenderer.color, targetAlpha, duration));
            State = BreathCircleState.BreathOut;
        }

        public void CallCallback()
        {
            if (_callback != null)
            {
                _callback();
                _callback = null;
            }
        }

        private IEnumerator LerpAlpha(Color start, Color end, float duration)
        {
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
                SpriteRenderer.color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            SpriteRenderer.color = end; //without this, the value will end at something like 0.9992367
            _currentLerp = null;
            CallCallback();
        }

    }
}
