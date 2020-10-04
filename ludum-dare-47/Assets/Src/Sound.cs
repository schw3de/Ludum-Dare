using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    public class Sound : Singleton<Sound>
    {
        [SerializeField]
        private AudioClip _beep;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Beep()
        {
            _audioSource.PlayOneShot(_beep);
        }
    }
}
