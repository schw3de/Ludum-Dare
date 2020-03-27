using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source
{
    public class ScratchWindow : MonoBehaviour
    {
        public GameObject ScratchWindowCanvas;
        public Text ScratchWindowText;
        public GameManager GameManager;

        private bool isTriggered;

        private IList<KeyCode> listOfPossibleKeyCodes = new List<KeyCode>{ KeyCode.K, KeyCode.L, KeyCode.P, KeyCode.O, KeyCode.N };

        private IList<KeyCode> generatedKeyCodes;

        private IList<KeyCode> pressedKeyCodes;

        void Update()
        {
            if(isTriggered)
            {
                if(generatedKeyCodes.Count == 0)
                {
                    return;
                }

                if(Input.GetKey(generatedKeyCodes.First()))
                {
                    generatedKeyCodes.RemoveAt(0);

                    if(generatedKeyCodes.Count == 0)
                    {
                        GameManager.SetEveryoneToScared(this.transform.position);

                        ScratchWindowText.text = string.Empty;

                        return;
                    }
                }

                ScratchWindowText.text = string.Format("Press following Combination in order to scratch the window: \n {0}", KeyCodesToList(generatedKeyCodes));
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            isTriggered = true;

            System.Random random = new System.Random();

            generatedKeyCodes = new List<KeyCode>();

            for(int i = 0; i < 3; i++)
            {
                KeyCode keyCode = listOfPossibleKeyCodes[random.Next(listOfPossibleKeyCodes.Count - 1)];
                generatedKeyCodes.Add(keyCode);
            }

            // ScratchWindowCanvas.SetActive(true);
        }

        private string KeyCodesToList(IList<KeyCode> generatedKeyCodes)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(KeyCode keyCode in generatedKeyCodes)
            {
                stringBuilder.Append(keyCode.ToString() + " ");
            }

            return stringBuilder.ToString();
        }

        void OnTriggerExit(Collider collider)
        {
            isTriggered = false;
            // ScratchWindowCanvas.SetActive(false);

            ScratchWindowText.text = string.Empty;
        }
    }
}
