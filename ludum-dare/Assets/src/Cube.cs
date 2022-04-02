using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public class Cube : MonoBehaviour
    {
        public CubeSide[] CubeSides = new CubeSide[6];

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Init(TMP_FontAsset tmp_FontAsset)
        {
            for (int cubeSideIndex = 0; cubeSideIndex < CubeSides.Length; cubeSideIndex++)
            {
                var cubeSideGo = new GameObject($"CubeSideIndex-{cubeSideIndex}");
                cubeSideGo.transform.SetParent(transform, false);

                var cubeSide = cubeSideGo.AddComponent<CubeSide>();

                cubeSide.Init(cubeSideIndex,
                              tmp_FontAsset,
                              OnMouseDown,
                              OnCountdownChange);

                CubeSides[cubeSideIndex] = cubeSide;
            }
        }

        public void StartCountDowns(int countdownIndex)
        {
            foreach (var cubeSide in CubeSides)
            {
                cubeSide.SetCountdown(countdownIndex);
            }
        }

        private void OnCountdownChange(CubeSide cubeSide)
        {
            var minCountDownIndex = CubeSides.Min(_ => _.CountDownIndex);

            if (minCountDownIndex <= 0)
            {
                DestroyImmediate(gameObject);
            }
            else if (minCountDownIndex < 4)
            {
                SetCubeState(CubeState.Red);
            }
            else if (minCountDownIndex < 6)
            {
                SetCubeState(CubeState.Yellow);
            }
            else
            {
                SetCubeState(CubeState.Default);
            }
        }

        private void OnMouseDown(CubeSide cubeSide)
        {
            cubeSide.SetCountdown(8);
            OnCountdownChange(cubeSide);
            //foreach(var cubeSide in CubeSides)
            //{
            //    cubeSide.SetCountdown(9);
            //}
        }

        private void SetCubeState(CubeState cubeState)
        {
            switch (cubeState)
            {
                case CubeState.Default:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeDefault);
                    break;

                case CubeState.Yellow:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeYellow);
                    break;

                case CubeState.Red:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeRed);
                    break;
            }
        }

        private void PreCheck(CubeSide cubeSide, Action<CubeSide> action)
        {
            if (gameObject != null)
            {
                action(cubeSide);
            }
        }

        private static void SetMaterial(MeshRenderer meshRenderer, Material material)
        {
            if (meshRenderer.material.name.Contains(material.name))
            {
                return;
            }

            Debug.Log("Changed Material");
            meshRenderer.material = material;
        }
    }
}
