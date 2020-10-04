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

        private void Awake()
        {
            _articlesText.text = "...";
            _total.text = string.Empty;
            _currentArticleCost.text = "0,0 €";
        }

        public void AddArticle(Article article)
        {
            _articlesText.text += $"{article.ArticleName} - {article.Cost} €  {Environment.NewLine}";
            _currentArticleCost.text = $"{article.Cost} €";
            _total.text = $"Toal: {_articles.Sum(x => x.Cost)} €";
        }
    }
}
