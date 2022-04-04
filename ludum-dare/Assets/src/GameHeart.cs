using schw3de.ld.Ui;
using schw3de.ld.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace schw3de.ld
{
    public class GameHeart : Singleton<GameHeart>
    {
        private GameObject _cubeParent;
        private List<Cube> _cubes = new List<Cube>();
        private Cube _menuCube;
        private MainUi _mainUi;
        private bool isMenu = true;
        private static readonly Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),

            new Vector3( 5, 0, 0),
            new Vector3(-5, 0, 0),

            new Vector3(0,  5, 0),
            new Vector3(0, -5, 0),

            new Vector3(0, 0,  5),
            new Vector3(0, 0, -5),

            new Vector3( 5,  5, 0),
            new Vector3(-5, -5, 0),
            new Vector3(-5,  5, 0),
            new Vector3( 5, -5, 0),

            new Vector3(0,  5,  5),
            new Vector3(0, -5, -5),
            new Vector3(0, -5,  5),
            new Vector3(0,  5, -5),

            new Vector3( 5, 0,  5),
            new Vector3(-5, 0, -5),
            new Vector3(-5, 0,  5),
            new Vector3( 5, 0, -5),

            new Vector3( 5,  5,  5),
            new Vector3(-5,  5,  5),
            new Vector3( 5, -5,  5),
            new Vector3( 5,  5, -5),
            new Vector3(-5, -5,  5),
            new Vector3(-5,  5, -5),
            new Vector3( 5, -5, -5),
            new Vector3(-5, -5, -5),
        };

        private DateTime _startTime;
        private bool _isGameRunning = false;
        private Timer _cubeSpawnTimer = new Timer();

        private int _cubeSpawnIndex = 0;
        private TimeSpan[] _cubeTimeSpawns = new TimeSpan[]
        {
            TimeSpan.FromSeconds(3),
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(8),
            TimeSpan.FromSeconds(12),
            TimeSpan.FromSeconds(15)
        };

        private new void Awake()
        {
            base.Awake();
            _cubeParent = new GameObject("CubeParent");
        }

        private void Start()
        {
            _mainUi = MainUi.Instance;
            CreateMenuCube(false);

            // Debug purpose
            //for (int index = 0; index < positions.Length; index++)
            //{
            //    CreateCube();
            //}
        }

        private void Update()
        {
            if (isMenu && Input.GetKeyDown(KeyCode.Return))
            {
                isMenu = false;
                DestroyImmediate(_menuCube.gameObject);
                _menuCube = null;

                _startTime = DateTime.UtcNow;
                _isGameRunning = true;
                _mainUi.gameObject.SetActive(true);
                CreateCube();
            }

            if (_isGameRunning)
            {
                var totalSeconds = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
                _mainUi.SetSurvived($"Survived: " + GetSurvivedFormat(TimeSpan.FromSeconds(totalSeconds)));

                if (_cubeSpawnTimer.IsFinished())
                {
                    _cubeSpawnIndex++;
                    SetCubeSpawnTimer();
                    CreateCube();
                }
            }
        }

        public void CreateCube()
        {
            var cubeSideStates = new CubeSideState[]
                                       {
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Countdown,
                                           CubeSideState.Reload,
                                           CubeSideState.Bomb,
                                           CubeSideState.Reload,
                                       };


            var cube = Cube.Create(new CubeActions(OnCountdownChanged, OnBombClicked),
                                   new CubeSideCreation(
                                       cubeSideStates = cubeSideStates.ShuffleArray()),
                                   _cubeParent.transform,
                                   positions[_cubes.Count]);

            cube.StartCountDowns(8);
            SetCubeSpawnTimer();

            _cubes.Add(cube);
        }

        private void OnBombClicked(Cube cube)
        {
            GameOver(cube);
        }

        public void CreateMenuCube(bool gameOver)
        {
            if (gameOver)
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
                GameOver(cubeOnCountdownChanged);
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

        private void GameOver(Cube cubeDestroyed)
        {
            var survied = DateTime.UtcNow - _startTime;
            var survivedTotalSeconds = (int)survied.TotalSeconds;
            cubeDestroyed.Explode();
            AudioSourceFx.Instance.PlayGameOver();

            CameraMovement.Instance.Block(TimeSpan.FromSeconds(3));
            foreach (var cube in _cubes)
            {
                cube.Stop();
            }

            _mainUi.gameObject.SetActive(false);
            _mainUi.SetSurvived(string.Empty);
            _isGameRunning = false;

            GameState.SetLastSurvivedTotalSeconds(survivedTotalSeconds);
            if (GameState.GetSurvivedTotalSeconds() < survivedTotalSeconds)
            {
                GameState.SetSurvivedTotalSeconds(survivedTotalSeconds);
            }

            StartCoroutine(ContinueWithGame());
        }

        private IEnumerator ContinueWithGame()
        {
            yield return new WaitForSeconds(3);

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

        private void SetCubeSpawnTimer()
        {
            if (_cubeTimeSpawns.Length <= _cubeSpawnIndex)
            {
                _cubeSpawnTimer.Start(_cubeTimeSpawns.Last());

                return;
            }

            _cubeSpawnTimer.Start(_cubeTimeSpawns[_cubeSpawnIndex]);
        }

        private static string GetSurvivedFormat(TimeSpan survived)
            => $"{survived.Minutes} min {survived.Seconds} sec{(survived.Seconds > 1 ? "s" : string.Empty)}";
    }
}
