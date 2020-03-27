using schw3de.MRnothing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    public class InfoBoardMessage : MonoBehaviour
    {
        public GameObject Activator;
        public AudioClip OpeningAudio;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if(!Game.Instance.AudioSource.isPlaying)
            //{
                Game.Instance.AudioSource.PlayOneShot(this.OpeningAudio);
            //}
            this.Activator.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            this.Activator.SetActive(false);
        }
    }
}
