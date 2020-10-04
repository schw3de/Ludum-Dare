using System;
using System.Globalization;
using UnityEngine;

namespace schw3de.ld47
{
    public class Article : MonoBehaviour
    {
        [SerializeField]
        private string _articleName;

        [SerializeField]
        private string _cost;
        public string ArticleName => _articleName;
        public decimal Cost => decimal.Parse(_cost);

        private void Awake()
        {
            name = ArticleName;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log($"Article Collision: {collision.relativeVelocity}");
        }
    }
}