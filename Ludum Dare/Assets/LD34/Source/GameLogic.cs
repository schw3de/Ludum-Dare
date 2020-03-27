using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class GameLogic : Singleton<GameLogic>
    {
        public GameObject PlayerPrefab;

        public Transform StartPosition;

        public Camera Cam1;

        public Camera Cam2;

        public Camera Cam3;

        public void ShowGameOver()
        {
            Hud.Instance.ShowGameOver();
        }

        public void GameOverShown()
        {

        }

        public void StartGame()
        {
            Hud.Instance.StartGame();

            this.PlayerInstance = GameObject.Instantiate<GameObject>(this.PlayerPrefab);
            this.PlayerInstance.transform.position = this.StartPosition.position;

            FollowTarget.Instance.Target = this.PlayerInstance.transform;

            Forest.Instance.CreateForest();
            this.gameIsRuning = true;
        }



        public new static GameLogic Instance { get; private set; }

        public GameObject PlayerInstance { get; private set; }

        float timeLeft = 5.0f;

        private int amountForestElements;

        private bool trainAnimationIsRuning;

        private bool gameIsRuning;

        private void Update()
        {
            if (!this.gameIsRuning)
                return;

            timeLeft -= Time.deltaTime;
            Hud.Instance.SetTime((int)Mathf.Round(timeLeft));

            if (timeLeft < 0)
            {
                if (this.trainAnimationIsRuning)
                    return;

                this.trainAnimationIsRuning = true;

                Forest.Instance.DisableTreeCreating();
                Player.Instance.gameObject.SetActive(false);
                Hud.Instance.DisableHud();

                this.StartCoroutine(this.RunTrainAnimation());

                // Game Over;
                if(this.amountForestElements > 0)
                {

                }
            }
        }

        private IEnumerator RunTrainAnimation()
        {
            this.Cam1.enabled = false;
            this.Cam2.enabled = true;
            Train.Instance.StartMoving();

            yield return new WaitForSeconds(3.0f);
            this.Cam2.enabled = false;
            this.Cam3.enabled = true;
        }

        protected override void Awake()
        {
            base.Awake();


        }

        private void Start()
        {
            Forest.Instance.ForestElementCreated += ForestElementGenerated;
            Forest.Instance.ForestElementDestroyed += ForestElementDestroyed;
        }

        private void ForestElementDestroyed(object sender, Events.ForestElementDestroyedEventArgs e)
        {
            Hud.Instance.SetAmountForestElements(e.AmountForestElements);
            this.amountForestElements = e.AmountForestElements;
        }

        private void ForestElementGenerated(object sender, Events.ForestElementCreatedEventArgs e)
        {
            Hud.Instance.SetAmountForestElements(e.AmountForestElements);
            this.amountForestElements = e.AmountForestElements;
        }
    }
}
