using UnityEngine;

namespace schw3de.ld47
{
    public class Article : MonoBehaviour
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private decimal _cost;
        public string Name => _name;
        public decimal Cost => _cost;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Debug.Log($"Article Collision: {collision.relativeVelocity}");
        }
    }
}