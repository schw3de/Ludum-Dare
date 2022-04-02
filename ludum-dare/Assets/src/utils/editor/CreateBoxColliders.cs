using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace schw3de.ld.utils
{
    internal class CreateBoxColliders
    {
        private static readonly int sizeOfCube = 2;

        private static readonly (Vector3 center, Vector3 size)[] sidePositions = new[]
        {
            (new Vector3(1, 0, 0), new Vector3(0, sizeOfCube, sizeOfCube)),
            (new Vector3(0, 1, 0), new Vector3(sizeOfCube, 0, sizeOfCube)),
            (new Vector3(0, 0, 1), new Vector3(sizeOfCube, sizeOfCube, 0)),
            (new Vector3(-1, 0, 0), new Vector3(0, sizeOfCube, sizeOfCube)),
            (new Vector3(0, -1, 0), new Vector3(sizeOfCube, sizeOfCube, sizeOfCube)),
            (new Vector3(0, 0, -1), new Vector3(sizeOfCube, sizeOfCube, sizeOfCube)),
        };

        [MenuItem("schw3de/CreateBoxColliders")]
        public static void CreateBoxCollidersExecute()
        {
            var box = Selection.activeTransform;
            for (int sideIndex = 1; sideIndex <= sidePositions.Length; sideIndex++)
            {
                var boxSideDetection = new GameObject();
                boxSideDetection.name = $"SideIndex-{sideIndex}";
                boxSideDetection.transform.SetParent(box, false);
                var collider = boxSideDetection.AddComponent<BoxCollider>();

                var center = sidePositions[sideIndex - 1].center;
                collider.center = center;
                collider.size = GetCubeSizeVector(center);

                boxSideDetection.AddComponent<SideTrigger>();
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
