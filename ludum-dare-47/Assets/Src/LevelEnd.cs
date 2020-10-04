using System;
using System.Collections.Generic;
using System.Linq;
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
        private Button _upgradeScanner;

        [SerializeField]
        private TextMeshProUGUI _upgradeScannerText;

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

        [SerializeField]
        private GameObject _fired;

        [SerializeField]
        private TextMeshProUGUI _dayOver;

        private bool _isGameOver;
        private bool _isFired;

        private void Awake()
        {
            _dayOver.text = $"{GameState.Instance.CurrentLevel.LevelName} completed";

            var salary = GameState.Instance.CalculateSalary();

            if (GameState.Instance.TotalSalary < 0)
            {
                _isFired = true;
            }
            else if (_levels.Last().LevelName == GameState.Instance.CurrentLevel.LevelName)
            {
                _isGameOver = true;
            }

            _salary.text = $"Articles Lost: {GameState.Instance.ArticlesLost}{Environment.NewLine}" +
                           $"Articles Fraud: {GameState.Instance.ArticlesFraud}{Environment.NewLine}" +
                           $"Customers unsatisfied: {GameState.Instance.CustomerSatisfactionScore}{Environment.NewLine}" +
                           $"Base salary : +{GameState.Instance.BaseSalery}{Environment.NewLine}" +
                           $"End salary : {GameState.Instance.BaseSalery} - {GameState.Instance.ArticlesLost} - {GameState.Instance.ArticlesFraud} - {GameState.Instance.CustomerSatisfactionScore}*10 = {salary} €{Environment.NewLine}" +
                           $"Current salary : {GameState.Instance.TotalSalary} €";


            if (!_isGameOver && !_isFired)
            {
                int currentIndex = _levels.IndexOf(_levels.First(x => x.LevelName == GameState.Instance.CurrentLevel.LevelName));
                GameState.Instance.CurrentLevel = Instantiate(_levels[++currentIndex]);
            }

            _fired.SetActive(_isFired);
            _gameOverPanel.SetActive(_isGameOver);

            EvaluateUpgradeSpeed();
            EvaluateUpgradeScanner();

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(Restart);

            _upgradeSpeed.onClick.RemoveAllListeners();
            _upgradeSpeed.onClick.AddListener(UpgradeSpeed);

            _upgradeScanner.onClick.RemoveAllListeners();
            _upgradeScanner.onClick.AddListener(UpgradeScanner);

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
            if (_isFired || _isGameOver)
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
            Features.Instance.TreadmillSpeed = Features.Instance.TreadmillSpeeds.First();
            Features.Instance.TreadmillSpeeds.RemoveAt(0);

            EvaluateUpgradeSpeed();
            EvaluateUpgradeScanner();
        }

        private void UpgradeScanner()
        {
            var upgrade = Features.Instance.ScannerSpeedCosts.First();

            GameState.Instance.TotalSalary -= upgrade.Item1;
            Features.Instance.ScannerSpeedCosts.RemoveAt(0);
            Features.Instance.ScannerSpeedSeconds = upgrade.Item2;

            EvaluateUpgradeScanner();
            EvaluateUpgradeSpeed();
        }

        private void EvaluateUpgradeScanner()
        {
            _upgradeScanner.interactable = Features.Instance.ScannerSpeedCosts.Any() && Features.Instance.ScannerSpeedCosts.First().Item1 <= GameState.Instance.TotalSalary;

            if (!Features.Instance.ScannerSpeedCosts.Any())
            {
                _upgradeScannerText.text = "Sold out!";
            }
            else
            {
                _upgradeScannerText.text = $"Cost {Features.Instance.ScannerSpeedCosts.First().Item1} €{Environment.NewLine}Upgrade";
            }
        }

        private void EvaluateUpgradeSpeed()
        {
            _upgradeSpeed.interactable = Features.Instance.TreadmillSpeedCosts.Any() && Features.Instance.TreadmillSpeedCosts.First() <= GameState.Instance.TotalSalary;

            if (!Features.Instance.TreadmillSpeedCosts.Any())
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
