using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


        public void Awake()
        {
            var instance = Taker.Instance;
            _currentLevel = GameState.Instance.CurrentLevel;

            _treadmill.ApplyFeature();
            _treadmill.ApplyFeature();

            _checkoutButton.interactable = false;
            _checkoutButton.onClick.RemoveAllListeners();
            _checkoutButton.onClick.AddListener(() => Checkout());

            CustomerQueue.Instance.Init(_currentLevel.Customers.Select(x => Guid.NewGuid()).ToList());
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
            _checkoutButton.interactable = true;
        }

        public void ArticleScanned(Article article)
        {
            CashierRegister.Instance.AddArticle(article);
        }

        private void Checkout()
        {
            _checkoutButton.interactable = false;
            CashierRegister.Instance.AddTotal();
            CustomerQueue.Instance.CustomerHasPaid();

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
