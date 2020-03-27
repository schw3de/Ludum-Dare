using UnityEngine;

namespace schw3de.MRnothing
{
    public class CoroutineStarter : MonoBehaviour
    {
        public static CoroutineStarter Instance;
        public void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
