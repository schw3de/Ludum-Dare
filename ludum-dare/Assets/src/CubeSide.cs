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

        private Timer _timer = new Timer(TimeSpan.FromSeconds(3));
        private (GameObject go, SpriteRenderer renderer) _spriteGo;

        private CubeSideActions _cubeSideActions;
        private TextMeshProUGUI _textMeshPro;
        private SpriteRenderer _sprite;
        //private SpriteRenderer _spriteBomb;
        //private SpriteRenderer _spriteReload;

        private void Update()
        {
            if (RunCountdown && _timer.IsFinished())
            {
                SetCountdownIndex(CountDownIndex--);
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

            SetCubeSideState(cubeSideState);

            //switch (cubeSideState)
            //{
            //    case CubeSideState.Countdown:
            //        var spriteCountdown = CreateChildGameObject(gameObject, "Sprite", boxSideInfo);
            //        _spriteCountDown = spriteCountdown.AddComponent<SpriteRenderer>();
            //        break;
            //    case CubeSideState.Bomb:
            //        var spriteBomb = CreateChildGameObject(gameObject, "Sprite Bomb", boxSideInfo);
            //        _spriteBomb = spriteBomb.AddComponent<SpriteRenderer>();
            //        _spriteBomb.sprite = GameAssets.Instance.BombSprite;
            //        break;
            //    case CubeSideState.Reload:
            //        var spriteReload = CreateChildGameObject(gameObject, "Sprite Reload", boxSideInfo);
            //        _spriteReload = spriteReload.AddComponent<SpriteRenderer>();
            //        _spriteReload.sprite = GameAssets.Instance.ReloadSprite;
            //        break;
            //}

            // Text Game Object
            //var textGameObject = CreateChildGameObject(gameObject, "Text", boxSideInfo);
            //_textMeshPro = textGameObject.AddComponent<TextMeshProUGUI>();
            //_textMeshPro.font = tmp_FontAsset;
            //_textMeshPro.enableAutoSizing = true;
            //_textMeshPro.fontSizeMin = 0;
            //_textMeshPro.text = sideIndex.ToString();
            //var rectTransform = (_textMeshPro.transform as RectTransform);
            //rectTransform.sizeDelta = new Vector2(1, 1);

            //var spriteCountdown = CreateChildGameObject(gameObject, "Sprite Countdown", boxSideInfo);
            //_spriteCountDown = spriteCountdown.AddComponent<SpriteRenderer>();
            //spriteRenderer.sprite = GameAssets.Instance.Bomb;


            //var spriteGo = CreateChildGameObject(gameObject, "Sprite", boxSideInfo);
            //var spriteRenderer = spriteGo.AddComponent<SpriteRenderer>();
            //spriteRenderer.sprite = GameAssets.Instance.Bomb;
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
            }
        }

        private void SetCountdownIndex(int countdownIndex)
        {
            Debug.Log($"CountdownIndex: {countdownIndex}");
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
            Debug.Log($"OnMouseDown! {gameObject.name} State:{CubeSideState}");
            _cubeSideActions.OnLeftClick(this);
        }

        private void OnMouseOver()
        {
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
    }
}
