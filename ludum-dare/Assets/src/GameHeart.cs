using System.Collections.Generic;
using UnityEngine;

namespace schw3de.ld
{
    public class GameHeart : Singleton<GameHeart>
    {
        public GameObject PrefabCube;

        private GameObject _cubeParent;
        private List<Cube> _cubes = new List<Cube>();

        private static readonly Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(5, 0, 0),
            new Vector3(0, 5, 0),
            new Vector3(0, 0, 5),
        };

        private new void Awake()
        {
            base.Awake();
            Debug.Log("Awake GameHeart");
            _cubeParent = new GameObject("CubeParent");

            CreateCube();
            CreateCube();
            CreateCube();
            CreateCube();
            StartCountDown();
        }

        public void CreateCube()
        {
            var cubeSideStates =  new CubeSideState[]
                                       {
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Bomb,
                                           CubeSideState.Reload,
                                       };

            _cubes.Add(Cube.Create(new CubeActions(OnCountdownChanged),
                                   new CubeSideCreation(
                                       cubeSideStates = cubeSideStates.ShuffleArray()),
                                   _cubeParent.transform,
                                   positions[_cubes.Count]));
        }

        private void OnCountdownChanged(Cube cube)
        {
            var minCountDownIndex = cube.GetMinCubeCountdown();

            if (minCountDownIndex <= 0)
            {
                cube.Destroy();
                _cubes.Remove(cube);
            }
            else if (minCountDownIndex < 4)
            {
                cube.SetCubeState(CubeState.Red);
            }
            else if (minCountDownIndex < 6)
            {
                cube.SetCubeState(CubeState.Yellow);
            }
            else
            {
                cube.SetCubeState(CubeState.Default);
            }
        }

        public void StartCountDown()
        {
            foreach (var cube in _cubes)
            {
                cube.StartCountDowns(8);
            }
        }
    }
}
