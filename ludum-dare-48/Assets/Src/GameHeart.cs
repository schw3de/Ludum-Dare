﻿using System;
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
        private GameState _gameState = GameState.Intro;
        private int _breathCounts = 0;
        private int _firstTutorialBreathCounts = 8;
        private int _thoughtsCounts = 0;
        private int _secondTutorialthoughtsCounter = 5;
        private int _meditationsthoughtsCounter = 10;
        private Thought _currentThought;

        private void Start()
        {
            Counter.gameObject.SetActive(false);

            _dialogs = DialogsContainer.GetComponentsInChildren<Dialog>().ToDictionary(x => x.DialogType, x => x);

            foreach (var dialog in _dialogs.Values)
            {
                dialog.gameObject.SetActive(true);
            }

            ShowDialog(DialogType.Start);
            //ShowDialog(DialogType.Tutorial_2);
        }

        private void Update()
        {
            if (_gameState == GameState.Meditation_1 || _gameState == GameState.Meditation_2 || _gameState == GameState.Meditation_3)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Keydown space");
                    BreathCircle.Instance.BreathIn(BreathIn);
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    if(_currentThought != null)
                    {
                        if(_currentThought.LetGo())
                        {
                            _thoughtsCounts++;
                            _currentThought = null;
                        }
                    }
                    Debug.Log("Keyup space");
                    BreathCircle.Instance.BreathOut(Breathout);
                }
            }
        }

        private void CreateThoughtAndSetCallback()
        {
            _currentThought = Instantiate(ThoughtPrefab, ThoughContainer.transform).GetComponent<Thought>();
        }

        private void Breathout()
        {
            _breathCounts++;
            SetBreathCounter();
            Sound.Instance.BreathOut();
            Debug.Log("Breath Decreased!");

            if(_gameState == GameState.Meditation_1 && _breathCounts >= _firstTutorialBreathCounts)
            {
                _gameState = GameState.Tutorial;
                ShowDialog(DialogType.Tutorial_3);
            }
            else if (_gameState == GameState.Meditation_2)
            {
                if (_currentThought == null)
                {
                    SetThoughtsCounter();

                    if (_thoughtsCounts >= _secondTutorialthoughtsCounter)
                    {
                        _gameState = GameState.Tutorial;
                        ShowDialog(DialogType.Tutorial_5);
                    }
                    else
                    {
                        CreateThoughtAndSetCallback();
                    }
                }
            }
            else if(_gameState == GameState.Meditation_3)
            {
                if (_currentThought == null)
                {
                    if (_thoughtsCounts >= _meditationsthoughtsCounter)
                    {
                        _gameState = GameState.Intro;
                        ShowDialog(DialogType.Start);
                    }
                    else
                    {
                        CreateThoughtAndSetCallback();
                    }
                }
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
                    _breathCounts = 0;
                    _thoughtsCounts = 0;
                    _timer.Start(TimeSpan.FromSeconds(1), () => dialog.Show(DialogCallback, DialogResultType.Tutorial_1, DialogResultType.Meditation_3));
                    break;

                case DialogType.Tutorial_1:
                    dialog.Show(DialogCallback, DialogResultType.Tutorial_2);
                    break;

                case DialogType.Tutorial_2:
                    dialog.Show(DialogCallback, DialogResultType.Meditation_1);
                    break;

                case DialogType.Tutorial_3:
                    _timer.Start(TimeSpan.FromSeconds(1), () => dialog.Show(DialogCallback, DialogResultType.Tutorial_3));
                    break;

                case DialogType.Tutorial_4:
                    dialog.Show(DialogCallback, DialogResultType.Tutorial_4);
                    break;

                case DialogType.Tutorial_5:
                    dialog.Show(DialogCallback, DialogResultType.Start);
                    break;
            }
        }

        private void DialogCallback(DialogResultType dialogResultType)
        {
            Debug.Log($"DialogCallback {dialogResultType}");
            switch (dialogResultType)
            {
                case DialogResultType.Start:
                    _gameState = GameState.Intro;
                    Counter.gameObject.SetActive(false);
                    _breathCounts = 0;
                    _thoughtsCounts = 0;
                    ShowDialog(DialogType.Start);
                    break;

                case DialogResultType.Tutorial_1:
                    _gameState = GameState.Tutorial;
                    ShowDialog(DialogType.Tutorial_1);
                    break;

                case DialogResultType.Tutorial_2:
                    _gameState = GameState.Tutorial;
                    ShowDialog(DialogType.Tutorial_2);
                    break;

                case DialogResultType.Meditation_1:
                    _gameState = GameState.Meditation_1;
                    Counter.gameObject.SetActive(true);
                    SetBreathCounter();
                    break;

                case DialogResultType.Tutorial_3:
                    _gameState = GameState.Tutorial;
                    ShowDialog(DialogType.Tutorial_4);
                    break;

                case DialogResultType.Tutorial_4:
                    Counter.gameObject.SetActive(true);
                    _gameState = GameState.Meditation_2;
                    SetThoughtsCounter();
                    CreateThoughtAndSetCallback();
                    break;

                case DialogResultType.Meditation_3:
                    Counter.gameObject.SetActive(false);
                    _gameState = GameState.Meditation_3;
                    CreateThoughtAndSetCallback();
                    break;
            }
        }

        private void SetBreathCounter()
        {
            if(_gameState == GameState.Meditation_1)
            {
                Counter.text = $"Breaths: {_breathCounts} / {_firstTutorialBreathCounts}";
            }
        }

        private void SetThoughtsCounter()
        {
            if(_gameState == GameState.Meditation_2)
            {
                Counter.text = $"Thoughts : {_thoughtsCounts} / {_secondTutorialthoughtsCounter}";
            }
        }
    }
}
