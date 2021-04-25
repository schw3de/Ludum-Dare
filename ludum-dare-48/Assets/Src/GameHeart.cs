using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld48
{
    public class GameHeart : Singleton<GameHeart>
    {
        public GameObject DialogsContainer;
        public TextMeshProUGUI Counter;
        public GameObject ThoughtPrefab;
        public GameObject ThoughContainer;
        public GameObject Canvas;

        private Dictionary<DialogType, Dialog> _dialogs;
        private Timer _timer = new Timer();
        private int _currentDialogIndex = 0;
        private GameState _gameState = GameState.Intro;
        private int _breathCounts = 0;
        private int _firstTutorialBreathCounts = 8;
        private int _thoughtsCounts = 0;
        private int _secondTutorialthoughtsCounter = 5;
        private Thought _currentThought;
        

        //private Dialog CurrentDialog => _dialogs[_currentDialogIndex];

        private void Start()
        {
            Counter.gameObject.SetActive(false);

            _dialogs = DialogsContainer.GetComponentsInChildren<Dialog>().ToDictionary(x => x.DialogType, x => x);

            foreach (var dialog in _dialogs.Values)
            {
                dialog.gameObject.SetActive(true);
            }

            //ShowDialog(DialogType.Start);
            ShowDialog(DialogType.Tutorial_3);
        }

        private void Update()
        {
            if (_gameState == GameState.Meditation_1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Keydown space");
                    BreathCircle.Instance.BreathIn(BreathIn);
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    Debug.Log("Keyup space");
                    BreathCircle.Instance.BreathOut(Breathout);
                }
            }
            else if(_gameState == GameState.Meditation_2)
            {
                if(_currentThought == null)
                {
                    _currentThought = CreateThought();
                }
            }
        }

        private Thought CreateThought()
        {
            return Instantiate(ThoughtPrefab, ThoughContainer.transform).GetComponent<Thought>();
        }

        private void Breathout()
        {
            _breathCounts++;
            SetBreathCounter();
            Sound.Instance.BreathOut();
            Debug.Log("Breath Decreased!");

            if(_gameState == GameState.Meditation_1 && _breathCounts == _firstTutorialBreathCounts)
            {
                _gameState = GameState.Tutorial;
                ShowDialog(DialogType.Tutorial_3);
            }
        }

        private void BreathIn()
        {
            _breathCounts++;
            SetBreathCounter();
            Sound.Instance.BreathIn();
            Debug.Log("Breath Increased!");
        }

        private void ShowDialog(DialogType dialogType)
        {
            Debug.Log($"Show dialog: {dialogType}");
            var dialog = _dialogs[dialogType];

            switch (dialogType)
            {
                case DialogType.Start:
                    _timer.Start(TimeSpan.FromSeconds(2), () => dialog.Show(DialogCallback, DialogResultType.Tutorial_1));
                    break;

                case DialogType.Tutorial_1:
                case DialogType.Tutorial_2:
                    dialog.Show(DialogCallback, DialogResultType.Tutorial_2);
                    break;

                case DialogType.Tutorial_3:
                    dialog.Show(DialogCallback, DialogResultType.Tutorial_3);
                    break;
            }
        }

        private void DialogCallback(DialogResultType dialogResultType)
        {
            Debug.Log($"DialogCallback {dialogResultType}");
            switch (dialogResultType)
            {
                case DialogResultType.Tutorial_1:
                    _gameState = GameState.Tutorial;
                    ShowDialog(DialogType.Tutorial_2);
                    break;
                case DialogResultType.Tutorial_2:
                    SetBreathCounter();
                    Counter.gameObject.SetActive(true);
                    _gameState = GameState.Meditation_1;
                    break;

                case DialogResultType.Tutorial_3:
                    SetThoughtsCounter();
                    _gameState = GameState.Meditation_2;
                    break;
            }
        }

        public void DialogExit(DialogResultType dialogResultType)
        {
            //Dialogs.First().Hide(FirstDialogHided);
        }

        private void ShowFirstDialog()
        {
            //Dialogs.First().Show(FirstDialogFinished);
        }

        private void FirstDialogFinished()
        {
            Debug.Log("First Dialog Finished");
        }

        private void FirstDialogHided()
        {
        }

        private void SetBreathCounter() => Counter.text = $"Breathcounter: {_breathCounts} / {_firstTutorialBreathCounts}";
        private void SetThoughtsCounter() => Counter.text = $"Thoughts: {_thoughtsCounts} / {_secondTutorialthoughtsCounter}";
    }
}
