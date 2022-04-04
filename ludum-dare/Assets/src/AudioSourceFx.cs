using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld
{
    public class AudioSourceFx : Singleton<AudioSourceFx>
    {
        private AudioSource _audioSource;
        private new void Awake()
        {
            base.Awake();
            _audioSource= GetComponent<AudioSource>();
        }

        public void PlayReloadFx()
        {
            _audioSource.PlayOneShot(GameAssets.Instance.ReloadSounds.GetRandomItem());
        }

        public void PlayGameOver()
        {
            _audioSource.PlayOneShot(GameAssets.Instance.GameOverSound);
        }
    }
}
