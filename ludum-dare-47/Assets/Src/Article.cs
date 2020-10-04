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

        public Guid Id { get; private set; } = Guid.NewGuid();

        private void Awake()
        {
            Id = Guid.NewGuid();
            name = ArticleName;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log($"Article Collision: {collision.relativeVelocity}");
        }
    }
}