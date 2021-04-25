using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace schw3de.ld48
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        protected static bool _dontDestroyOnLoad = false;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }


                var instance = FindObjectOfType<T>();

                if(instance != null)
                {
                    if(_dontDestroyOnLoad)
                    {
                        DontDestroyOnLoad(instance.gameObject);
                    }
                    _instance = instance.GetComponent<T>();
                    return _instance;
                }

                var singletonGo = new GameObject($"{typeof(T).Name}-singleton");

                _instance = singletonGo.AddComponent<T>();

                if(_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(singletonGo);
                }

                return _instance;
            }
        }
    }
}
