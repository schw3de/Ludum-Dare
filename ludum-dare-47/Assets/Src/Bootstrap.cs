using UnityEngine;

namespace schw3de.ld47
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Bootstrap loading...");
            var instance = Taker.Instance;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Bootstrap completed.");
        }
    }
}
