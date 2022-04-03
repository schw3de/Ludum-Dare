using UnityEngine;

namespace schw3de.ld
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        //protected static bool _dontDestroyOnLoad = false;

        protected void Awake()
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"Awake Singleton {gameObject.name}");
        }

        public static T Instance
        {
            get
            {
                return _instance;
                //if (_instance != null)
                //{
                //    return _instance;
                //}


                //var instance = FindObjectOfType<T>();

                //if(instance != null)
                //{
                //    if(_dontDestroyOnLoad)
                //    {
                //        DontDestroyOnLoad(instance.gameObject);
                //    }
                //    _instance = instance.GetComponent<T>();
                //    return _instance;
                //}

                //var singletonGo = new GameObject($"{typeof(T).Name}-singleton");

                //_instance = singletonGo.AddComponent<T>();

                //if(_dontDestroyOnLoad)
                //{
                //    DontDestroyOnLoad(singletonGo);
                //}

                //return _instance;
            }
        }
    }
}
