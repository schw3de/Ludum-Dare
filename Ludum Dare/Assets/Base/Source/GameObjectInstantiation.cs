using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.Base.Source
{
    public class GameObjectInstantiation : Singleton<GameObjectInstantiation>
    {
        public T Instantiate<T>(GameObject gameObject, Vector3 position, Transform parent)
        {
            GameObject go = Instantiate(gameObject, position, Quaternion.identity) as GameObject;
            go.transform.SetParent(parent);

            return go.GetComponent<T>();
        }
    }
}
