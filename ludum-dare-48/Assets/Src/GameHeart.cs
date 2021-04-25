using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld48
{
    public class GameHeart : Singleton<GameHeart>
    {
        public GameObject DialogsContainer;

        private Dictionary<DialogType, Dialog> _dialogs;
        private Timer _timer = new Timer();
        private int _currentDialogIndex = 0;
        private GameState _gameState = GameState.Intro;
        private int _breathCounts = 0;

        //private Dialog CurrentDialog => _dialogs[_currentDialogIndex];

        private void Start()
        {
            _dialogs = DialogsContainer.GetComponentsInChildren<Dialog>().ToDictionary(x => x.DialogType, x => x);

            foreach (var dialog in _dialogs.Values)
            {
                dialog.gameObject.SetActive(true);
            }

            ShowDialog(DialogType.Start);
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
        }

        private void Breathout()
        {
            Sound.Instance.BreathOut();
            Debug.Log("Breath Decreased!");
        }

        private void BreathIn()
        {
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
                    _breathCounts = 5;
                    _gameState = GameState.Meditation_1;
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


    }
}
