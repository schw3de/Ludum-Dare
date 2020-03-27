using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;
using UnityEngine.UI;

namespace schw3de.LD34.Source
{
    public class Hud : Singleton<Hud>
    {
        public Text AmountOfForestElementsLeft;

        public Text Time;

        public GameObject GameOverCanvas;

        public GameObject StartGameCanvas;

        public GameObject MainHud;

        public void SetAmountForestElements(int amountForestElements)
        {
            this.AmountOfForestElementsLeft.text = string.Format("Forest elements: {0}", amountForestElements);
        }

        public void StartGame()
        {
            this.StartGameCanvas.SetActive(false);
        }

        public void ShowGameOver()
        {
            this.GameOverCanvas.SetActive(true);
        }

        internal void SetTime(int timeLeft)
        {
            this.Time.text = string.Format("Time left: {0}", timeLeft);
        }

        public void DisableHud()
        {
            this.MainHud.SetActive(false);
        }
    }
}
