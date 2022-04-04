using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public class Cube : MonoBehaviour
    {
        private static int cubeIndex = 0;

        public int CubeIndex = -1;
        public CubeSide[] CubeSides = new CubeSide[6];

        private MeshRenderer _meshRenderer;
        private CubeActions _cubeActions;
        private CubeSideCreation _cubeSideCreation;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Init(CubeActions cubeActions, CubeSideCreation cubeSideCreation)
        {
            CubeIndex = cubeIndex++;
            gameObject.name = $"Cube {CubeIndex}";

            _cubeActions = cubeActions;
            _cubeSideCreation = cubeSideCreation;

            for (int cubeSideIndex = 0; cubeSideIndex < CubeSides.Length; cubeSideIndex++)
            {
                var cubeSideGo = new GameObject($"CubeSideIndex-{cubeSideIndex}");
                cubeSideGo.transform.SetParent(transform, false);

                var cubeSide = cubeSideGo.AddComponent<CubeSide>();

                cubeSide.Init(cubeSideIndex,
                              _cubeSideCreation.CubeSideStates[cubeSideIndex],
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

        public void Destroy()
        {
            Debug.Log($"Destroy Cube {CubeIndex}");
            // TODO add particels
            DestroyImmediate(gameObject);
        }

        public void Explode()
        {
            Instantiate(GameAssets.Instance.ExplosionPrefab, transform);
        }

        public void Stop()
        {
            foreach (var cubeSide in CubeSides)
            {
                cubeSide.Stop();
            }
        }

        private void OnRightClick(CubeSide _)
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

        public int GetMinCubeCountdown() 
            => CubeSides.Where(_ => _.CubeSideState == CubeSideState.Countdown).Min(_ => _.CountDownIndex);

        private void OnCountdownChange(CubeSide cubeSide)
        {
            _cubeActions.CountdownChanged(this);
        }

        private void OnLeftClick(CubeSide cubeSideClicked)
        {
            if(cubeSideClicked.CubeSideState == CubeSideState.Bomb)
            {
                _cubeActions.BombClicked(this);
            }

            if(cubeSideClicked.CubeSideState != CubeSideState.Reload)
            {
                return;
            }

            AudioSourceFx.Instance.PlayReloadFx();

            RandomizeCubeSides();

            foreach (var cubeSide in CubeSides)
            {
                cubeSide.SetCountdown(8);
            }

            OnCountdownChange(CubeSides.First());
        }

        private void RandomizeCubeSides()
        {
            _cubeSideCreation.ShuffleCubeSideStates();

            for (int cubeSideIndex = 0; cubeSideIndex < CubeSides.Length; cubeSideIndex++)
            {
                CubeSides[cubeSideIndex].SetCubeSideState(_cubeSideCreation.CubeSideStates[cubeSideIndex]);
            }
        }

        public void SetCubeState(CubeState cubeState)
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
