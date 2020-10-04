using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.ld47
{
    public class Start : MonoBehaviour
    {
        [SerializeField]
        public Button _startButton;

        public void Awake()
        {
            _startButton.onClick.RemoveAllListeners();
            _startButton.onClick.AddListener(() => StartTheGame());
        }

        private void StartTheGame()
        {
            SceneManager.LoadScene(Scenes.Level);
        }
    }
}
