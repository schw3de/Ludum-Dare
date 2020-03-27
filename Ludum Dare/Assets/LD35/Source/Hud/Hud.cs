using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;
using UnityEngine.UI;

namespace schw3de.LD35.Source
{
    public class Hud : Singleton<Hud>
    {
        //[SerializeField]
        //private Button StartGameButton;

        [SerializeField]
        private GameObject MainMenu;

        [SerializeField]
        private GameObject Life1;

        [SerializeField]
        private GameObject Life2;

        [SerializeField]
        private GameObject Life3;

        [SerializeField]
        private GameObject LifeText;

        [SerializeField]
        private GameObject GameOver;

        [SerializeField]
        private Animator GameOverAnimation;

        [SerializeField]
        private GameObject Won;

        [SerializeField]
        private Animator WonAnimation;

        public Action<HudEvent> HudEventCallback { get; set; }

        public GameObject Life11
        {
            get
            {
                return Life1;
            }

            set
            {
                this.Life1 = value;
            }
        }

        public void SetState(HudState hudstate)
        {
            switch (hudstate)
            {
                case HudState.Intro:
                    //StartGameButton.gameObject.SetActive(true);
                    break;

                case HudState.GameLoop:
                    //StartGameButton.gameObject.SetActive(false);
                    break;
            }
        }

        public void ShowStartMenu(bool isShow)
        {
            this.MainMenu.SetActive(isShow);
            this.Life1.SetActive(true);
            this.Life2.SetActive(true);
            this.Life3.SetActive(true);
            this.LifeText.SetActive(true);
            this.GameOver.SetActive(false);
            this.GameOver.transform.localScale = Vector3.zero;

            this.Won.SetActive(false);
            this.Won.transform.localScale = Vector3.zero;
        }

        internal void ShowWon()
        {
            this.Won.SetActive(true);
            this.WonAnimation.Play("gameover");
        }

        public void ShowGameOver()
        {
            this.GameOver.SetActive(true);
            this.GameOverAnimation.Play("gameover");
        }

        public void RemoveLife()
        {
            if(this.Life1.activeSelf)
            {
                this.Life1.SetActive(false);
            }
            else if(this.Life2.activeSelf)
            {
                this.Life2.SetActive(false);
            }
            else if(this.Life3.activeSelf)
            {
                this.Life3.SetActive(false);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            //StartGameButton.gameObject.SetActive(false);
            //this.gameObject.SetActive(false);
        }

        private void Start()
        {
            //StartGameButton.onClick.AddListener(() => this.HudEventCallback(HudEvent.StartGame));
        }
    }
}
