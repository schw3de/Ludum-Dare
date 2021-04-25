using TMPro;
using UnityEngine;

namespace schw3de.ld48
{
    public class Thought : MonoBehaviour
    {
        public TextMeshPro Text;

        private readonly string[] thoughtsTemplate = new[] { "Past", "Future", "Money", "Work", "Homework", "Memory", "Food", "Eating", "Stuff", "Cleaning", "Regrets", "Doubts", "Childhood", "Old Friends", "Parents", "Pain", "Worries", "Finances", "Household", "Things", "Netflix", "Movies" };
        private bool _canLetGo;
        private bool _isLettingGo;

        public void FlewAway()
        {
            if (!_isLettingGo)
            {
                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            Text = GetComponent<TextMeshPro>();
            Text.text = thoughtsTemplate.GetRandomItem();
        }

        public bool LetGo()
        {
            if (!_canLetGo)
            {
                return false;
            }
            Taker.Instance.StopDragging();
            _isLettingGo = true;
            GetComponent<Animator>().enabled = false;
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.freezeRotation = false;
            return true;
        }

        public bool CanBeDragged() => !_isLettingGo;

        public void CanLetGo()
        {
            _canLetGo = true;
        }

        public void CanNotLetGo()
        {
            _canLetGo = false;
        }

        public void StartDrag()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void CancelDrag()
        {
            GetComponent<Animator>().enabled = true;
        }
    }
}