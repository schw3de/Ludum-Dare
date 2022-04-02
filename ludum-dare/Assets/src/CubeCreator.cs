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
        public static void CreateCube(GameObject cube, TMP_FontAsset tmp_FontAsset)
        {
            var cubeSides = new CubeSide[6];

            for (int cubeSideIndex = 0; cubeSideIndex < cubeSides.Length; cubeSideIndex++)
            {
                var cubeSideGo = new GameObject($"CubeSideIndex-{cubeSideIndex}");
                cubeSideGo.transform.SetParent(cube.transform, false);

                var cubeSide = cubeSideGo.AddComponent<CubeSide>();

                cubeSide.Init(cubeSideIndex, tmp_FontAsset);

                cubeSides[cubeSideIndex] = cubeSide;
            }
        }
    }
}
