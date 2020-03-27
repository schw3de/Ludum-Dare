using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class Explosion : MonoBehaviour
    {
        public GameObject bum;

        public void OnTriggerEnter(Collider collider)
        {
            if(collider.tag.Equals("Tree"))
            {
                GameLogic.Instance.ShowGameOver();
            }
        }
    }
}
