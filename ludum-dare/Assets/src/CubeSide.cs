using schw3de.ld.utils;
using System;
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

        private Timer _timer = new Timer(TimeSpan.FromSeconds(1));
        private (GameObject go, SpriteRenderer renderer) _spriteGo;

        private TextMeshProUGUI _textMeshPro;
        private Action<CubeSide> _onMouseDown;
        private Action<CubeSide> _onCountdownChange;
        private SpriteRenderer _spriteCountDown;

        private void Update()
        {
            if (RunCountdown && _timer.IsFinished())
            {
                CountDownIndex--;
                SetText(CountDownIndex);

                if (CountDownIndex <= 0)
                {
                    RunCountdown = false;
                    Debug.Log("Bang!");
                }
                else
                {
                    _timer.Start();
                }

                _onCountdownChange(this);
            }
        }

        public void Init(int sideIndex, TMP_FontAsset tmp_FontAsset, Action<CubeSide> onMouseDown, Action<CubeSide> onCountdownChange)
        {
            Index = sideIndex;
            _onMouseDown = onMouseDown;
            _onCountdownChange = onCountdownChange;
            var collider = gameObject.AddComponent<BoxCollider>();

            var boxSideInfo = sidePositions[sideIndex];
            var center = boxSideInfo.center;
            collider.center = center;
            collider.size = GetCubeSizeVector(center);

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            // Text Game Object
            //var textGameObject = CreateChildGameObject(gameObject, "Text", boxSideInfo);
            //_textMeshPro = textGameObject.AddComponent<TextMeshProUGUI>();
            //_textMeshPro.font = tmp_FontAsset;
            //_textMeshPro.enableAutoSizing = true;
            //_textMeshPro.fontSizeMin = 0;
            //_textMeshPro.text = sideIndex.ToString();
            //var rectTransform = (_textMeshPro.transform as RectTransform);
            //rectTransform.sizeDelta = new Vector2(1, 1);

            var spriteCountdown = CreateChildGameObject(gameObject, "Sprite Countdown", boxSideInfo);
            _spriteCountDown = spriteCountdown.AddComponent<SpriteRenderer>();
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
            CountDownIndex = countdownIndex;
            SetText(CountDownIndex);
            _timer.Start();
            RunCountdown = true;
        }

        private void SetText(int countdown)
        {
            //_textMeshPro.text = countdown.ToString();
            if (countdown == 0)
            {
                _spriteCountDown.sprite = null;
            }
            else
            {
                _spriteCountDown.sprite = GameAssets.Instance.CubeCountdown[countdown - 1];
            }
        }

        private void OnMouseDown()
        {
            Debug.Log($"OnMouseDown! {gameObject.name}");
            _onMouseDown(this);
        }

        private static Vector3 GetCubeSizeVector(Vector3 center)
            => new Vector3(GetCubeSizeParameter(center.x),
                           GetCubeSizeParameter(center.y),
                           GetCubeSizeParameter(center.z));

        private static int GetCubeSizeParameter(float parameter)
            => parameter == 0 ? sizeOfCube : 0;
    }
}
