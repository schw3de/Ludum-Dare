using TMPro;
using UnityEditor;
using UnityEngine;

namespace schw3de.ld.utils
{
    internal class CubeUtils
    {
        //private static readonly int sizeOfCube = 2;
        //private static readonly float distanceFromCube = 1.01f;

        //private static readonly (Vector3 center, Vector3 rotation)[] sidePositions = new[]
        //{
        //    (new Vector3(1, 0, 0), new Vector3(0, 270, 0)),
        //    (new Vector3(0, 1, 0), new Vector3(90, 0, 0)),
        //    (new Vector3(0, 0, 1), new Vector3(0, 180, 0)),
        //    (new Vector3(-1, 0, 0), new Vector3(0, 90, 0)),
        //    (new Vector3(0, -1, 0), new Vector3(90, 0, 0)),
        //    (new Vector3(0, 0, -1), new Vector3(0, 0, 0)),
        //};

        [MenuItem("schw3de/Create Cube")]
        public static void CreateCube()
        {
            var cube = Selection.activeTransform;

            if(cube.name != "cube")
            {
                Debug.Log("Use this only on the cube!");
                return;
            }

            DeleteChildren(cube);

            //Cube.CreateCube(cube.gameObject, GameAssets.Instance.CubeFont);

            //for (int sideIndex = 1; sideIndex <= sidePositions.Length; sideIndex++)
            //{
            //    var boxSideDetection = new GameObject($"SideIndex-{sideIndex}");
            //    boxSideDetection.transform.SetParent(box, false);
            //    var collider = boxSideDetection.AddComponent<BoxCollider>();

            //    var boxSideInfo = sidePositions[sideIndex - 1];
            //    var center = boxSideInfo.center;
            //    collider.center = center;
            //    collider.size = GetCubeSizeVector(center);

            //    boxSideDetection.AddComponent<SideTrigger>();

            //    var canvas = boxSideDetection.AddComponent<Canvas>();
            //    canvas.renderMode = RenderMode.WorldSpace;

            //    var textGameObject = new GameObject($"TextSideIndex-{sideIndex}");
            //    textGameObject.transform.SetParent(boxSideDetection.transform, false);

            //    var textMeshPro = textGameObject.AddComponent<TextMeshProUGUI>();
            //    textMeshPro.enableAutoSizing = true;
            //    textMeshPro.fontSizeMin = 0;
            //    textMeshPro.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Fonts/prstart SDF.asset");
            //    textMeshPro.text = sideIndex.ToString();

            //    var rectTransform = (textMeshPro.transform as RectTransform);
            //    rectTransform.sizeDelta = new Vector2(1, 1);
            //    rectTransform.localPosition = center * distanceFromCube;
            //    rectTransform.localRotation = Quaternion.Euler(boxSideInfo.rotation);
            //}
        }

        private static void DeleteChildren(Transform cube)
        {
            // Why is this not working? :-/
            //foreach(Transform child in box.transform)
            //{
            //    GameObject.DestroyImmediate(child.gameObject);
            //}

            while (cube.childCount > 0)
            {
                GameObject.DestroyImmediate(cube.GetChild(0).gameObject);
            }

        }
    }
}
