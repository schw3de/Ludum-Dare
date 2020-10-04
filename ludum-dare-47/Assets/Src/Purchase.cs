using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    public class Purchase : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _articlesPrefabs;

        [SerializeField]
        private Transform _dropPosition;

        public List<Article> Articles { get; set; }

        public void InstantiateArticles(List<GameObject> articles, Action<List<GameObject>> createdArticles)
        {
            StartCoroutine(ArticleDropPoint.Instance.InstantiateArticle(articles, createdArticles));
        }

        private void CreatedArticles(List<GameObject> createdArtictles)
        {
            Articles = createdArtictles.Select(x => x.GetComponent<Article>()).ToList();
        }
    }
}
