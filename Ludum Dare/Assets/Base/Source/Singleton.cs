using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.Base.Source
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this.GetComponent<T>();
        }
    }
}
