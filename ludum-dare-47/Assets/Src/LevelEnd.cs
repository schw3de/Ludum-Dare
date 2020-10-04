using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
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
        private TextMeshProUGUI _upgradeSpeedText;

        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private List<LevelData> _levels;

        [SerializeField]
        private GameObject _gameOverPanel;

        [SerializeField]
        private TextMeshProUGUI _salary;

        private bool _isGameOver;

        private void Awake()
        {
            _isGameOver = _levels.Last().LevelName == GameState.Instance.CurrentLevel.LevelName;

            var salary = GameState.Instance.CalculateSalary();

            _salary.text = $"Articles Balance: {GameState.Instance.ArticlesScore}{Environment.NewLine}" +
                           $"Customer Satisfaction: {GameState.Instance.CustomerSatisfactionScore}{Environment.NewLine}" +
                           $"Base salary : +{GameState.Instance.BaseSalery}{Environment.NewLine}" +
                           $"End salary : {GameState.Instance.BaseSalery} {GameState.Instance.ArticlesScore} * {GameState.Instance.CustomerSatisfactionScore} = {salary}{Environment.NewLine}" +
                           $"Current salary : {GameState.Instance.TotalSalary}";

            if (!_isGameOver)
            {
                int currentIndex = _levels.IndexOf(_levels.First(x => x.LevelName == GameState.Instance.CurrentLevel.LevelName));
                GameState.Instance.CurrentLevel = Instantiate(_levels[++currentIndex]);
            }
            _gameOverPanel.SetActive(_isGameOver);

            EvaluateUpgradeSpeed();

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(Restart);

            _upgradeSpeed.onClick.RemoveAllListeners();
            _upgradeSpeed.onClick.AddListener(UpgradeSpeed);

            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(Continue);

            GameState.Instance.ClearScore();
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
            var costs = Features.Instance.TreadmillSpeedCosts.First();

            GameState.Instance.TotalSalary -= costs;
            Features.Instance.TreadmillSpeedCosts.RemoveAt(0);
            Features.Instance.TreadmillSpeed += Features.Instance.TreadmillSpeedAdditive.First();
            Features.Instance.TreadmillSpeedAdditive.RemoveAt(0);

            EvaluateUpgradeSpeed();
        }

        private void EvaluateUpgradeSpeed()
        { 
            _upgradeSpeed.interactable = Features.Instance.TreadmillSpeedCosts.Any() && Features.Instance.TreadmillSpeedCosts.First() <= GameState.Instance.TotalSalary;

            if(!Features.Instance.TreadmillSpeedCosts.Any())
            {
                _upgradeSpeedText.text = "Sold out!";
            }
            else
            {
                _upgradeSpeedText.text = $"Cost {Features.Instance.TreadmillSpeedCosts.First()} €{Environment.NewLine}Upgrade";
            }
        }
    }
}
