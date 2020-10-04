using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace schw3de.ld47
{
    public class Game : Singleton<Game>
    {
        [SerializeField]
        private Button _checkoutButton;
        [SerializeField]
        private List<GameObject> _articlePrefabs;
        [SerializeField]
        private GameObject _purchasePrefab;
        [SerializeField]
        private Treadmill _treadmill;
        [SerializeField]
        private List<LevelData> _levels;

        private LevelData _currentLevel;
        private Purchase _currentPurchase;
        private CustomerData _currentCustomer;
        private List<Article> _scannedArticles;
        private List<Article> _createdArticles;
        private Queue<TimeSpan> _timeSatifications;


        public void Awake()
        {
            var instance = Taker.Instance;
            _currentLevel = GameState.Instance.CurrentLevel;

            _treadmill.ApplyFeature();
            _treadmill.ApplyFeature();

            _checkoutButton.interactable = false;
            _checkoutButton.onClick.RemoveAllListeners();
            _checkoutButton.onClick.AddListener(() => Checkout());

            _timeSatifications = new Queue<TimeSpan>(_currentLevel.Customers.Select(x =>TimeSpan.FromSeconds(x.SecondsSatification)));

            CustomerQueue.Instance.Init(_currentLevel.Customers.Select(x => Guid.NewGuid()).ToList(), _timeSatifications.Dequeue());
            HandleNextCustomer();
        }

        private void HandleNextCustomer()
        {
            _currentCustomer = _currentLevel.Customers.First();
            _currentLevel.Customers.Remove(_currentCustomer);

            _currentPurchase = Instantiate(_purchasePrefab).GetComponent<Purchase>();

            var randomArticles = new List<GameObject>();
            for (int randomIndex = 0; randomIndex < _currentCustomer.AmountArticles; randomIndex++)
            {
                randomArticles.Add(_articlePrefabs.GetRandomItem());
            }

            _currentPurchase.InstantiateArticles(randomArticles, CreatedArticles);
        }

        private void CreatedArticles(List<GameObject> createdArticles)
        {
            _createdArticles = createdArticles.Select(x => x.GetComponent<Article>()).ToList();
            _checkoutButton.interactable = true;
        }

        public void ArticleScanned(Article article)
        {
            Sound.Instance.Beep();
            CashierRegister.Instance.AddArticle(article);
        }

        private void Checkout()
        {
            var totalcostExpected =_createdArticles.Select(x => x.Cost).Sum();
            var totalcostActual = CashierRegister.Instance.Articles.Select(x => x.Cost).Sum();
            _checkoutButton.interactable = false;

            var tmpScansAcutal = CashierRegister.Instance.Articles.ToList();
            var tmpScansExpected = _createdArticles.ToList();

            foreach (var scannedArticle in tmpScansAcutal.ToList())
            {
                var remove = tmpScansExpected.FirstOrDefault(x => x.ArticleName == scannedArticle.ArticleName);
                if (remove != null)
                {
                    tmpScansExpected.Remove(remove);
                    tmpScansAcutal.Remove(scannedArticle);
                }
            }

            GameState.Instance.ArticlesFraud += tmpScansAcutal.Select(x => x.Cost).Sum();
            GameState.Instance.ArticlesLost += tmpScansExpected.Select(x => x.Cost).Sum();
            GameState.Instance.CustomerSatisfactionScore += (int)CustomerQueue.Instance.ActiveCustomer.Satification <= 0 ? 1 : 0;
            
            Debug.Log($"CustomerSatisfaction: {GameState.Instance.CustomerSatisfactionScore} - Articles: {GameState.Instance.ArticlesLost}");

            CashierRegister.Instance.AddTotal();
            var timeSpan = TimeSpan.FromSeconds(0);
            if(_timeSatifications.Any())
            {
                timeSpan = _timeSatifications.Dequeue();
            }

            CustomerQueue.Instance.CustomerHasPaid(timeSpan);

            if(!_currentLevel.Customers.Any())
            {
                Debug.Log("Level Finished");
                SceneManager.LoadScene(Scenes.LevelEnd);
                return;
            }

            HandleNextCustomer();
        }
    }
}
