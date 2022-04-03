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
        private CubeActions _cubeActions;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Init(CubeActions cubeActions, CubeSideCreation cubeSideCreation)
        {
            _cubeActions = cubeActions;

            for (int cubeSideIndex = 0; cubeSideIndex < CubeSides.Length; cubeSideIndex++)
            {
                var cubeSideGo = new GameObject($"CubeSideIndex-{cubeSideIndex}");
                cubeSideGo.transform.SetParent(transform, false);

                var cubeSide = cubeSideGo.AddComponent<CubeSide>();

                cubeSide.Init(cubeSideIndex,
                              cubeSideCreation.CubeSideStates[cubeSideIndex],
                              new CubeSideActions(OnLeftClick,
                                                  OnRightClick,
                                                  OnCountdownChange));

                CubeSides[cubeSideIndex] = cubeSide;
            }
        }

        public static Cube Create(CubeActions cubeActions,
                                  CubeSideCreation cubeSideCreation,
                                  Transform parent,
                                  Vector3 position)
        {
            var cubeGo = Instantiate(GameAssets.Instance.CubePrefab, parent.transform);
            cubeGo.transform.localPosition = position;
            cubeGo.SetActive(true);

            var cube = cubeGo.AddComponent<Cube>();
            cube.Init(cubeActions, cubeSideCreation);

            return cube;
        }

        private void OnRightClick(CubeSide obj)
        {
            CameraMovement.Instance.SetTarget(transform);
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
            var minCountDownIndex = CubeSides.Where(_ => _.CubeSideState == CubeSideState.Countdown).Min(_ => _.CountDownIndex);

            if (minCountDownIndex <= 0)
            {
                DestroyImmediate(gameObject);
                _cubeActions.CubeDestroyed(this);
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

        private void OnLeftClick(CubeSide cubeSideClicked)
        {
            if(cubeSideClicked.CubeSideState != CubeSideState.Reload)
            {
                return;
            }

            //cubeSide.SetCountdown(8);
            //OnCountdownChange(cubeSideClicked);

            foreach (var cubeSide in CubeSides)
            {
                cubeSide.SetCountdown(8);
                OnCountdownChange(cubeSide);

            }
        }

        private void SetCubeState(CubeState cubeState)
        {
            switch (cubeState)
            {
                case CubeState.Default:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeDefaultMaterial);
                    break;

                case CubeState.Yellow:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeYellowMaterial);
                    break;

                case CubeState.Red:
                    SetMaterial(_meshRenderer, GameAssets.Instance.CubeRedMaterial);
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
