using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source
{
    public class GameManager : MonoBehaviour
    {
        public GameObject StartMenu;
        public GameObject GameOverMenu;
        public GameObject Monster;

        public GameState GameState { get; private set; }

        public bool EveryoneScared { get; private set; }

        public Vector3 ScaredPosition { get; set; }

        private bool locked;

        private Timer timer = new Timer();

        void Update()
        {
            if(locked)
            {
                return;
            }

            locked = true;

            try
            {
                switch (GameState)
                {
                    case GameState.Menu:
                        break;

                    case GameState.Start:
                        GameOverMenu.SetActive(false);
                        StartMenu.SetActive(false);
                        Monster.SetActive(true);
                        GameState = GameState.Running;
                        break;

                    case Source.GameState.GameOver:
                        Monster.SetActive(false);
                        GameOverMenu.SetActive(true);
                        break;
                }

                if(EveryoneScared && timer.IsFinished())
                {
                    EveryoneScared = false;
                }
            }
            finally
            {
                locked = false;
            }
        }

        public void SetGameState(GameState gameState)
        {
            GameState = gameState;
        }

        public void StartGame()
        {
            GameState = GameState.Start;
        }

        public void SetEveryoneToScared(Vector3 scaredPosition)
        {
            this.ScaredPosition = scaredPosition;
            EveryoneScared = true;
            timer.SetTime(3);
        }
    }
}
