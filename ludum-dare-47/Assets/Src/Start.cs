using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.ld47
{
    public class Start : MonoBehaviour
    {
        [SerializeField]
        private LevelData _firstLevel;
        [SerializeField]
        public Button _startButton;

        public void Awake()
        {
            _startButton.onClick.RemoveAllListeners();
            _startButton.onClick.AddListener(() => StartTheGame());
            GameState.Instance.CurrentLevel = Instantiate(_firstLevel);
            GameState.Instance.Reset();
            Features.Instance.Reset();
        }

        private void StartTheGame()
        {
            Sound.Instance.Beep();
            SceneManager.LoadScene(Scenes.Level);
        }
    }
}
