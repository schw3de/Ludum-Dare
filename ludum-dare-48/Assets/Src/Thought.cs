using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace schw3de.ld48
{
    public class Thought : MonoBehaviour
    {
        public TextMeshPro Text;

        private string[] thoughtsTemplate = new [] { "Work", "Thing", "ABC" };

        private void Start()
        {
            Text = GetComponent<TextMeshPro>();
            Text.text = thoughtsTemplate.GetRandomItem();
        }
    }
}