using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public class GameAssets : Singleton<GameAssets>
    {
        public GameObject CubePrefab;
        public TMP_FontAsset CubeFont;

        public Material CubeDefaultMaterial;
        public Material CubeYellowMaterial;
        public Material CubeRedMaterial;

        public Sprite BombSprite;
        public Sprite ReloadSprite;

        public Sprite[] CubeCountdownSprite;

        public Sprite MenuStart;
        public Sprite MenuTutorial1Sprite;
        public Sprite MenuTutorial2Sprite;
        public Sprite MenuTutorial3Sprite;

        public GameObject ExplosionPrefab;

        public AudioClip[] ReloadSounds;
        public AudioClip GameOverSound;
    }
}
