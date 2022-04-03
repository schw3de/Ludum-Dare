using schw3de.ld.Ui;
using schw3de.ld.utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace schw3de.ld
{
    public class GameHeart : Singleton<GameHeart>
    {
        public GameObject PrefabCube;

        private GameObject _cubeParent;
        private List<Cube> _cubes = new List<Cube>();
        private Cube _menuCube;
        private MainUi _mainUi;
        private bool isMenu = true;
        private bool _isShowGameOver;
        private static readonly Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(5, 0, 0),
            new Vector3(0, 5, 0),
            new Vector3(0, 0, 5),
        };

        private DateTime _startTime;
        private bool _isGameRunning = false;

        private new void Awake()
        {
            base.Awake();
            Debug.Log("Awake GameHeart");
            _cubeParent = new GameObject("CubeParent");

            CreateMenuCube(false);
            //CreateCube();
            //CreateCube();
            //CreateCube();
            //CreateCube();
            //StartCountDown();
        }

        private void Start()
        {
            _mainUi = MainUi.Instance;
            //_mainUi.Init(new UiActions(OnStartInevitableMode));
        }

        private void Update()
        {
            if(isMenu && Input.GetKeyDown(KeyCode.Return))
            {
                isMenu = false;
                DestroyImmediate(_menuCube.gameObject);
                _menuCube = null;

                _startTime = DateTime.UtcNow;
                _isGameRunning = true;
                _mainUi.gameObject.SetActive(true);
                CreateCube();
            }

            if(_isGameRunning)
            {
                var totalSeconds = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
                _mainUi.SetSurvived($"Survived: " + GetSurvivedFormat(TimeSpan.FromSeconds(totalSeconds)));
            }
        }

        public void CreateCube()
        {
            var cubeSideStates = new CubeSideState[]
                                       {
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Bomb,
                                           CubeSideState.Reload,
                                       };


            var cube = Cube.Create(new CubeActions(OnCountdownChanged, OnBombClicked),
                                   new CubeSideCreation(
                                       cubeSideStates = cubeSideStates.ShuffleArray()),
                                   _cubeParent.transform,
                                   positions[_cubes.Count]);

            cube.StartCountDowns(8);

            _cubes.Add(cube);
        }

        private void OnBombClicked(Cube obj)
        {
            GameOver();
        }

        public void CreateMenuCube(bool gameOver)
        {
            if(gameOver)
            {
                CameraMovement.Instance.SetCamToTop();
            }

            var cubeSideStates = new CubeSideState[]
                           {
                                           CubeSideState.Tutorial3,
                                           gameOver ? CubeSideState.GameOver : CubeSideState.Empty,
                                           CubeSideState.Tutorial2,
                                           CubeSideState.Tutorial1,
                                           CubeSideState.Empty,
                                           CubeSideState.Start,
                           };

            _menuCube = Cube.Create(new CubeActions(OnCountdownChanged, OnBombClicked),
                                   new CubeSideCreation(cubeSideStates),
                                   _cubeParent.transform,
                                   positions[_cubes.Count]);

            _menuCube.transform.localScale = new Vector3(5, 5, 5);
        }

        private void OnCountdownChanged(Cube cubeOnCountdownChanged)
        {
            var minCountDownIndex = cubeOnCountdownChanged.GetMinCubeCountdown();

            if (minCountDownIndex <= 0)
            {
                //cube.Destroy();
                //_cubes.Remove(cube);
                GameOver();
            }
            else if (minCountDownIndex < 4)
            {
                cubeOnCountdownChanged.SetCubeState(CubeState.Red);
            }
            else if (minCountDownIndex < 6)
            {
                cubeOnCountdownChanged.SetCubeState(CubeState.Yellow);
            }
            else
            {
                cubeOnCountdownChanged.SetCubeState(CubeState.Default);
            }
        }

        private void GameOver()
        {
            _mainUi.gameObject.SetActive(false);
            _mainUi.SetSurvived(string.Empty);
            _isGameRunning = false;
            var survied = DateTime.UtcNow - _startTime;
            var survivedTotalSeconds = (int)survied.TotalSeconds;
            GameState.SetLastSurvivedTotalSeconds(survivedTotalSeconds);
            
            if(GameState.GetSurvivedTotalSeconds() < survivedTotalSeconds)
            {
                GameState.SetSurvivedTotalSeconds(survivedTotalSeconds);
            }

            foreach (var cube in _cubes)
            {
                cube.Destroy();
            }
            _cubes.Clear();

            CreateMenuCube(true);
            isMenu = true;
        }

        public void StartCountDown()
        {
            foreach (var cube in _cubes)
            {
                cube.StartCountDowns(8);
            }
        }
        private static string GetSurvivedFormat(TimeSpan survived)
            => $"{survived.Minutes} min {survived.TotalSeconds} sec{(survived.TotalSeconds > 1 ? "s" : string.Empty)}";
    }
}
