using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.ld47
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField]
        private Button _upgradeSpeed;

        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private List<LevelData> _levels;

        [SerializeField]
        private GameObject _gameOverPanel;

        private bool _isGameOver;

        private void Awake()
        {
            _isGameOver = _levels.Last().LevelName == GameState.Instance.CurrentLevel.LevelName;

            if(!_isGameOver)
            {
                int currentIndex = _levels.IndexOf(_levels.First(x => x.LevelName == GameState.Instance.CurrentLevel.LevelName));
                GameState.Instance.CurrentLevel = Instantiate(_levels[++currentIndex]);
            }
            _gameOverPanel.SetActive(_isGameOver);

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(Restart);

            _upgradeSpeed.onClick.RemoveAllListeners();
            _upgradeSpeed.onClick.AddListener(UpgradeSpeed);

            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(Continue);
        }

        private void Restart()
        {
            SceneManager.LoadScene(Scenes.Start);
        }

        private void Continue()
        {
            
            if(_isGameOver)
            {
                SceneManager.LoadScene(Scenes.Start);
            }
            else
            {
                SceneManager.LoadScene(Scenes.Level);
            }
        }

        private void UpgradeSpeed()
        {
            Features.Instance.TreadmillSpeed += 0.3f;
        }
    }
}
