using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace schw3de.ld48
{
    public class Thought : MonoBehaviour
    {
        public TextMeshPro Text;

        private string[] thoughtsTemplate = new [] { "Thought" };
        private bool _canLetGo;
        private bool _isLettingGo;
        private Action _flewAwayCallback;

        public void FlewAway()
        {
            _flewAwayCallback();
            Destroy(gameObject);
        }

        private void Start()
        {
            Text = GetComponent<TextMeshPro>();
            Text.text = thoughtsTemplate.GetRandomItem();
        }

        public void SetFlewAwayCallback(Action flewAwayCallback) => _flewAwayCallback = flewAwayCallback;

        public void LetGo()
        {
            if(!_canLetGo)
            {
                return;
            }
            Taker.Instance.StopDragging();
            _isLettingGo = true;
            GetComponent<Animator>().enabled = false;
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.freezeRotation = false;
            //rigidbody.gravityScale = -1;
        }

        public bool CanBeDragged() => !_isLettingGo;

        public void CanLetGo()
        {
            _canLetGo = true;
        }

        public void CanNotLetGo()
        {
            _canLetGo = false;
        }

        public void StartDrag()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void CancelDrag()
        {
            GetComponent<Animator>().enabled = true;
        }
    }
}