using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public static class CubeCreator
    {
        public static Cube CreateCube(GameObject cubeGo, TMP_FontAsset tmp_FontAsset)
        {
            var cube = cubeGo.AddComponent<Cube>();
            cube.Init(tmp_FontAsset);

            return cube;
        }
    }
}
