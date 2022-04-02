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
        public TMP_FontAsset CubeFont;

        public Material CubeDefault;
        public Material CubeYellow;
        public Material CubeRed;

        public Sprite Bomb;
    }
}
