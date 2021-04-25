using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace schw3de.ld48
{
    public class Dialog : MonoBehaviour
    {
        public Animator Animator;

        public Button Button1;
        public Button Button2;

        public DialogType DialogType;

        private Action<DialogResultType> _callback;
        private DialogResultType _dialogResultType;

        private void Start()
        {

        }

        public void Show(Action<DialogResultType> callback, params DialogResultType[] buttonResultType)
        {
            _callback = callback;
            Animator.Play("FlyIn");

            Button1.Apply(() => FlyOut(buttonResultType[0]));

            if (buttonResultType.Length > 1)
            {
                Button2.Apply(() => FlyOut(buttonResultType[1]));
            }
        }
        
        private void FlyOut(DialogResultType dialogResultType)
        {
            _dialogResultType = dialogResultType;
            Animator.Play("FlyOut");
        }

        public void FlyInExit()
        {
            //_callback();
        }

        public void FlyOutExit()
        {
            _callback(_dialogResultType);
        }
    }
}
