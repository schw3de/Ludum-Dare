using UnityEngine;

namespace schw3de.ld47
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var singletonGo = new GameObject(typeof(T).Name);

                _instance = singletonGo.AddComponent<T>();

                DontDestroyOnLoad(singletonGo);

                return _instance;
            }
        }
    }
}
