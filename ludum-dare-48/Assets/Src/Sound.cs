using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld48
{
    public class Sound : Singleton<Sound>
    {
        public AudioClip BreathInClip;
        public AudioClip BreathOutClip;
        public AudioClip BirdsMorning;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void BreathIn()
        {
            _audioSource.PlayOneShot(BreathInClip);
        }

        public void BreathOut()
        {
            _audioSource.PlayOneShot(BreathOutClip);
        }
    }
}
