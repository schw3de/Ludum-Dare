using schw3de.ld.utils;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public class CubeSide : MonoBehaviour
    {
        private static readonly int sizeOfCube = 2;
        private static readonly float distanceFromCube = 1.01f;

        private static readonly CubeSideState[] MenuStates =
        {
            CubeSideState.Empty,
            CubeSideState.Start,
            CubeSideState.Tutorial1,
            CubeSideState.Tutorial2,
            CubeSideState.Tutorial3
        };

        private static readonly (Vector3 center, Vector3 rotation)[] sidePositions = new[]
        {
            (new Vector3(1, 0, 0), new Vector3(0, 270, 0)),
            (new Vector3(0, 1, 0), new Vector3(90, 0, 0)),
            (new Vector3(0, 0, 1), new Vector3(0, 180, 0)),
            (new Vector3(-1, 0, 0), new Vector3(0, 90, 0)),
            (new Vector3(0, -1, 0), new Vector3(90, 0, 0)),
            (new Vector3(0, 0, -1), new Vector3(0, 0, 0)),
        };

        public int Index;
        public bool RunCountdown = false;
        public int CountDownIndex;
        public CubeSideState CubeSideState;

        private Timer _timer = new Timer(TimeSpan.FromSeconds(2));
        private (GameObject go, SpriteRenderer renderer) _spriteGo;

        private CubeSideActions _cubeSideActions;
        private TextMeshProUGUI _textMeshPro;
        private SpriteRenderer _sprite;
        private bool _isStop;

        private void Update()
        {
            if (_isStop)
            {
                return;
            }

            if (RunCountdown && _timer.IsFinished())
            {
                SetCountdownIndex(CountDownIndex - 1);
                SetCountdownSprite(CountDownIndex);

                if (CountDownIndex <= 0)
                {
                    RunCountdown = false;
                    Debug.Log("Bang!");
                }
                else
                {
                    _timer.Start();
                }

                _cubeSideActions.OnCountdownChanged(this);
            }
        }

        public void Init(int sideIndex,
                         CubeSideState cubeSideState,
                         CubeSideActions cubeSideActions)
        {
            Index = sideIndex;

            _cubeSideActions = cubeSideActions;
            var collider = gameObject.AddComponent<BoxCollider>();

            var boxSideInfo = sidePositions[sideIndex];
            var center = boxSideInfo.center;
            collider.center = center;
            collider.size = GetCubeSizeVector(center);

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            var spriteGo = CreateChildGameObject(gameObject, "Sprite", boxSideInfo);
            _sprite = spriteGo.AddComponent<SpriteRenderer>();

            // Text Game Object
            if (cubeSideState == CubeSideState.GameOver)
            {
                var textGameObject = CreateChildGameObject(gameObject, "Text", boxSideInfo);
                _textMeshPro = textGameObject.AddComponent<TextMeshProUGUI>();
                _textMeshPro.font = GameAssets.Instance.CubeFont;
                _textMeshPro.enableAutoSizing = true;
                _textMeshPro.fontSizeMin = 0;
                _textMeshPro.text = sideIndex.ToString();

                var rectTransform = (_textMeshPro.transform as RectTransform);
                rectTransform.sizeDelta = new Vector2(1, 1.5f);
                _textMeshPro.outlineWidth = 0.2f;
                _textMeshPro.outlineColor = new Color32(0, 0, 0, 255);
            }

            SetCubeSideState(cubeSideState);
        }

        public void Stop()
        {
            _isStop = true;
        }

        private GameObject CreateChildGameObject(GameObject parent, string name, (Vector3 center, Vector3 rotation) boxSideInfo)
        {
            var newGameObject = new GameObject(name);
            newGameObject.transform.SetParent(parent.transform, false);
            newGameObject.transform.localPosition = boxSideInfo.center * distanceFromCube;
            newGameObject.transform.localRotation = Quaternion.Euler(boxSideInfo.rotation);
            return newGameObject;
        }

        public void SetCountdown(int countdownIndex)
        {
            if (CubeSideState != CubeSideState.Countdown)
            {
                return;
            }

            SetCountdownIndex(countdownIndex);
            SetCountdownSprite(CountDownIndex);
            _timer.Start();
            RunCountdown = true;
        }

        public void SetCubeSideState(CubeSideState cubeSideState)
        {
            CubeSideState = cubeSideState;

            switch (cubeSideState)
            {
                case CubeSideState.Countdown:
                    _sprite.sprite = GameAssets.Instance.CubeCountdownSprite.Last();
                    break;
                case CubeSideState.Bomb:
                    _sprite.sprite = GameAssets.Instance.BombSprite;
                    break;
                case CubeSideState.Reload:
                    _sprite.sprite = GameAssets.Instance.ReloadSprite;
                    break;
                case CubeSideState.Empty:
                    _sprite.sprite = null;
                    break;
                case CubeSideState.Start:
                    _sprite.sprite = GameAssets.Instance.MenuStart;
                    break;
                case CubeSideState.Tutorial1:
                    _sprite.sprite = GameAssets.Instance.MenuTutorial1Sprite;
                    break;
                case CubeSideState.Tutorial2:
                    _sprite.sprite = GameAssets.Instance.MenuTutorial2Sprite;
                    break;
                case CubeSideState.Tutorial3:
                    _sprite.sprite = GameAssets.Instance.MenuTutorial3Sprite;
                    break;
                case CubeSideState.GameOver:
                    var survived = TimeSpan.FromSeconds(GameState.GetLastSurvivedTotalSeconds());
                    var highScore = TimeSpan.FromSeconds(GameState.GetSurvivedTotalSeconds());
                    var newHighScore =
                    _textMeshPro.text = $"Game Over!\n\nYou survived:\n{GetSurvivedFormat(survived)}\n\n{ShowHighScore(survived, highScore)}";
                    break;
            }
        }

        private void SetCountdownIndex(int countdownIndex)
        {
            CountDownIndex = countdownIndex;
        }

        private void SetCountdownSprite(int countdown)
        {
            if (CubeSideState != CubeSideState.Countdown)
            {
                return;
            }

            //_textMeshPro.text = countdown.ToString();
            if (countdown == 0)
            {
                _sprite.sprite = null;
            }
            else
            {
                _sprite.sprite = GameAssets.Instance.CubeCountdownSprite[countdown - 1];
            }
        }

        private void OnMouseDown()
        {
            if (_isStop)
            {
                return;
            }

            if (MenuStates.Contains(CubeSideState))
            {
                return;
            }

            Debug.Log($"OnMouseDown! {gameObject.name} State:{CubeSideState}");
            _cubeSideActions.OnLeftClick(this);
        }

        private void OnMouseOver()
        {
            if (_isStop)
            {
                return;
            }

            if (MenuStates.Contains(CubeSideState))
            {
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right clicked!");
                _cubeSideActions.OnRightClick(this);
            }
        }

        private static Vector3 GetCubeSizeVector(Vector3 center)
            => new Vector3(GetCubeSizeParameter(center.x),
                           GetCubeSizeParameter(center.y),
                           GetCubeSizeParameter(center.z));

        private static int GetCubeSizeParameter(float parameter)
            => parameter == 0 ? sizeOfCube : 0;

        private static string GetSurvivedFormat(TimeSpan survived)
            => $"{survived.Minutes} min {survived.TotalSeconds} sec{(survived.TotalSeconds > 1 ? "s" : string.Empty)}";

        public static string ShowHighScore(TimeSpan survived, TimeSpan highscore)
            => survived == highscore ? "New Highscore!" : $"Highscore:\n{GetSurvivedFormat(highscore)}";
    }
}
