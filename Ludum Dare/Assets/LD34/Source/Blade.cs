using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class Blade : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Tree"))
            {
                var forestElement = other.transform.root.GetComponent<ForestElement>();
                forestElement.StartDestroy();
            }
        }
    }
}
