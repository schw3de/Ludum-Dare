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

        private List<Dialog> _dialogs;
        private Timer _timer = new Timer();
        private int _currentDialog = 0;

        private Dialog CurrentDialog => _dialogs[_currentDialog];

        private void Start()
        {
            _dialogs = DialogsContainer.GetComponentsInChildren<Dialog>().ToList();

            foreach(var dialog in _dialogs)
            {
                dialog.gameObject.SetActive(true);
            }

            HandleDialogs();
        }

        private void HandleDialogs()
        {
            switch (CurrentDialog.DialogType)
            {
                case DialogType.Start:
                    _timer.Start(TimeSpan.FromSeconds(5), () => CurrentDialog.Show(DialogCallback, DialogResultType.WithTutorial));
                    break;
            }
        }

        private void DialogCallback(DialogResultType dialogResultType)
        {
            switch (dialogResultType)
            {
                case DialogResultType.WithTutorial:
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
