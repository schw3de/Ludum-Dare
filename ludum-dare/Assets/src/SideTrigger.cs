using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace schw3de.ld
{
    public class SideTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnMouseDown()
        {
            Debug.Log($"OnMouseDown! {gameObject.name}");
        }
    }
}
