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
        //public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        public BreathCircleState State;

        private bool _increase;
        private Action _callback;
        private float _durationBreathIn = 4;
        private float _durationBreathOut = 6;
        private Coroutine _currentLerp;

        private void Start()
        {
            //Animator = GetComponent<Animator>();
            if(State == BreathCircleState.Empty && SpriteRenderer.color.a != 0)
            {
                SpriteRenderer.SetAlpha(0);
                //Debug.Log($"Sprite Renderer: {SpriteRenderer.color.a}");
            }
        }

        private void Update()
        {
            //if (State == BreathCircleState.Increase && !SpriteRenderer.AddAlpha(0.001f))
            //{
            //    CallCallback();
            //}
            //else if (State == BreathCircleState.Decrease && !SpriteRenderer.SubstractAlpha(0.01f))
            //{
            //    CallCallback();
            //}
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
            var duration =  SpriteRenderer.color.a == 0 ? _durationBreathIn : SpriteRenderer.color.a * _durationBreathIn;
            _currentLerp = StartCoroutine(LerpAlpha(SpriteRenderer.color, targetAlpha, duration));
            //Animator.Play("Expand");
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
                duration = Math.Abs(SpriteRenderer.color.a - 1) * _durationBreathOut;
            }

            _currentLerp = StartCoroutine(LerpAlpha(SpriteRenderer.color, targetAlpha, duration));
            //Animator.Play("Shrink");
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
