using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace schw3de.ld47
{
    public class ArticleDropPoint : Singleton<ArticleDropPoint>
    {
        [SerializeField]
        private GameObject _nextCustomerStopper;

        private bool _articleHasLeftDropPoint;
 
        public IEnumerator InstantiateArticle(List<GameObject> articles, Action<List<GameObject>> articlesCreated)
        {
            var createdArticles = new List<GameObject>();
            foreach (var article in articles)
            {
                _articleHasLeftDropPoint = false;
                var angle = UnityEngine.Random.Range(0.0f, 360.0f);
                GameObject articleGo = Instantiate(article, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                createdArticles.Add(articleGo);
                yield return new WaitUntil(() => _articleHasLeftDropPoint);
            }

            _articleHasLeftDropPoint = false;
            GameObject go = Instantiate(_nextCustomerStopper, transform.position, Quaternion.identity);
            yield return new WaitUntil(() => _articleHasLeftDropPoint);

            articlesCreated(createdArticles);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == Tags.Article || collision.gameObject.tag == Tags.NextCustomerStopper)
            {
                _articleHasLeftDropPoint = true;
            }
        }
    }
}
