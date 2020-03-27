using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace schw3de.MRnothing
{
    public class SpikeChecker : MonoBehaviour
    {
        public AudioClip SpikeSound;
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Gotcha!");
            Game.Instance.GameOver();
            Game.Instance.AudioSource.PlayOneShot(this.SpikeSound);
        }
    }
}

