using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.MRnothing
{
    public class Game : Singleton<Game>
    {
        public Text text;
        public GameObject Ui;
        public AudioClip FinishedLevelAudio;
        public AudioClip LoopSound;

        private Timer timer = new Timer();
        private bool allowRCheck;

        private Dictionary<Levels, string> finishedMessages = new Dictionary<Levels, string>
        {
            { Levels.Level1, "Thats was not so hard wasnt it? :)" },
            { Levels.Level2, "Awesome, I can already see Mr. Somebody!" },
            { Levels.Level3, "My Hero, my dream, you are the greatest!" },
            { Levels.Level4, "The biggest champ of all!" },
            { Levels.Level5, "I call that skills!" },
            { Levels.Level6, "Thats it! You made it! Thx for playing!" },
        };
        public AudioSource AudioSource { get; set; }

        public void FinishedLevel()
        {
            this.AudioSource.PlayOneShot(this.FinishedLevelAudio);
            this.RegisteredCurrentPlayer.BlockControls();
            this.Ui.SetActive(true);
            this.text.text = this.finishedMessages[this.CurrentLevel];
            this.CurrentLevel = this.GetNextLevel(this.CurrentLevel);
        }

        public Player RegisteredCurrentPlayer { get; set; }

        public Levels CurrentLevel { get; set; }


        protected override void Awake()
        {
            base.Awake();
            this.Ui.SetActive(false);
            this.AudioSource = this.GetComponent<AudioSource>();
            this.AudioSource.Play();
        }

        private void Update()
        {
            if(this.allowRCheck && Input.GetKeyDown(KeyCode.R))
            {
                this.ReloadCurrentLevel();
            }
        }

        public void GameOver()
        {
            this.RegisteredCurrentPlayer.Die();
            this.timer.Start(2, this.ShowGameOver);
        }

        private void ShowGameOver()
        {
            this.Ui.SetActive(true);
            this.text.text = "I am Sorry! Game Over My Friend. Maybe try again? Press R";
            this.allowRCheck = true;
        }

        public void LoadFirstLevel()
        {
            this.CurrentLevel = Levels.Level1;
            SceneManager.LoadScene(this.CurrentLevel.ToString());
        }

        public void ReloadCurrentLevel()
        {
            this.allowRCheck = false;
            this.Ui.SetActive(false);
            SceneManager.LoadScene(this.CurrentLevel.ToString());
        }

        public static void LoadLevel(Levels level)
        {
            SceneManager.LoadScene(level.ToString());
        }

        private Levels GetNextLevel(Levels level)
        {
            switch (level)
            {
                case Levels.Level1:
                    return Levels.Level2;
                case Levels.Level2:
                    return Levels.Level3;
                case Levels.Level3:
                    return Levels.Level4;
                case Levels.Level4:
                    return Levels.Level5;
                case Levels.Level5:
                    return Levels.Level6;
                case Levels.Level6:
                    return Levels.Bootstrap;
            }

            return Levels.Level1;
        }
    }
}
