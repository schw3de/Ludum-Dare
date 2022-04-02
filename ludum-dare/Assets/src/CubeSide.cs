using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Init(int sideIndex, TMP_FontAsset tmp_FontAsset)
        {
            Index = sideIndex;
            var collider = gameObject.AddComponent<BoxCollider>();

            var boxSideInfo = sidePositions[sideIndex];
            var center = boxSideInfo.center;
            collider.center = center;
            collider.size = GetCubeSizeVector(center);

            gameObject.AddComponent<SideTrigger>();

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            var textGameObject = new GameObject($"Text");
            textGameObject.transform.SetParent(gameObject.transform, false);

            var textMeshPro = textGameObject.AddComponent<TextMeshProUGUI>();
            textMeshPro.enableAutoSizing = true;
            textMeshPro.fontSizeMin = 0;
            textMeshPro.font = tmp_FontAsset;
            textMeshPro.text = sideIndex.ToString();

            var rectTransform = (textMeshPro.transform as RectTransform);
            rectTransform.sizeDelta = new Vector2(1, 1);
            rectTransform.localPosition = center * distanceFromCube;
            rectTransform.localRotation = Quaternion.Euler(boxSideInfo.rotation);
        }

        private static Vector3 GetCubeSizeVector(Vector3 center)
            => new Vector3(GetCubeSizeParameter(center.x),
                           GetCubeSizeParameter(center.y),
                           GetCubeSizeParameter(center.z));

        private static int GetCubeSizeParameter(float parameter)
            => parameter == 0 ? sizeOfCube : 0;
    }
}
