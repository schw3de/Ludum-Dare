using UnityEngine;

namespace schw3de.ld47
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var instance = Taker.Instance;
            DontDestroyOnLoad(gameObject);
        }
    }
}
