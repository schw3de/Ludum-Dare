using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.ld49
{
    public class StartUi : MonoBehaviour
    {
        public Button StartButton;

        void Start()
        {
            this.GetButton(OnStart);
        }

        private void OnStart()
        {
            SceneManager.LoadScene("Level");
        }
    }
}
