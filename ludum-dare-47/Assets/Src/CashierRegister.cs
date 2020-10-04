using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace schw3de.ld47
{
    public class CashierRegister : Singleton<CashierRegister>
    {
        [SerializeField]
        private TextMeshProUGUI _articlesText;
        [SerializeField]
        private TextMeshProUGUI _total;
        [SerializeField]
        private TextMeshProUGUI _currentArticleCost;

        private readonly List<Article> _articles = new List<Article>();

        private bool _isTotal;

        private void Awake()
        {
            Clear();
        }

        public void Clear()
        {
            _articlesText.text = string.Empty;
            _total.text = string.Empty;
            _currentArticleCost.text = "0,0 €";
            _articles.Clear();
        }

        public void AddTotal()
        {
            var total = _articles.Sum(x => x.Cost);
            _currentArticleCost.text = $"Total: {total} €";
            _articlesText.text = _articlesText.text.Insert(0, $"Total - {total} €  {Environment.NewLine}");
            _isTotal = true;
        }

        public void AddArticle(Article article)
        {
            if(_isTotal)
            {
                _isTotal = false;
                Clear();
            }

            _articlesText.text = _articlesText.text.Insert(0, $"{article.ArticleName} - {article.Cost} €  {Environment.NewLine}");
            _currentArticleCost.text = $"{article.Cost} €";
            _articles.Add(article);
        }
    }
}
