using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.MRnothing
{
    public class Collector : MonoBehaviour
    {
        public float SecondsToAppear;
        public AudioClip PickupAudio;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.GetComponent<Collider2D>().enabled = false;
            this.gameObject.SetActive(false);
            Game.Instance.AudioSource.PlayOneShot(this.PickupAudio);
        }
    }
}
