using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace schw3de.ld.utils
{
    public static class ButtonHelper
    {
        public static Button GetButton(this MonoBehaviour monoBehaviour, UnityAction onButtonClick)
        {
            return monoBehaviour.GetButton<Button>(onButtonClick);
        }

        public static T GetButton<T>(this MonoBehaviour monoBehaviour, UnityAction onButtonClick) where T : Button
        {
            var button = monoBehaviour.GetComponentInChildren<T>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick);
            return button;

        }

        public static void Apply(this Button button, UnityAction onButtonClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick);
        }
    }
}
