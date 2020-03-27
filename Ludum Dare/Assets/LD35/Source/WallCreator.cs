using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class WallCreator : Singleton<WallCreator>
    {
        [SerializeField]
        private GameObject defaultWallCubePrefab;

        [SerializeField]
        private Transform wallTarget;

        [SerializeField]
        private Transform wallTargetFinished;

        public void Create(int xSize, int ySize, Wall wall, Action wallFinished)
        {
            GameObject wallGameObject = new GameObject("Wall");
            wallGameObject.transform.position = this.transform.position;

            for(int x = 0; x < xSize; x++)
                for(int y = 0; y < ySize; y++)
                {
                    if (wall.FreePositions.Any(freePosition => freePosition.X == x && freePosition.Y == y))
                        continue;

                    GameObject createdWallCube = Instantiate(this.defaultWallCubePrefab, new Vector3(x, y), Quaternion.identity) as GameObject;
                    createdWallCube.name = string.Format("WallCube ({0}, {1})", x, y);
                    createdWallCube.transform.SetParent(wallGameObject.transform, false);
                }

            LerpAnimation.Instance.Move(wallGameObject, wallTarget.position, wall.Duration, LerpAnimationType.Curve, () => this.WallFinished(wallGameObject, wallFinished));
        }

        private void WallFinished(GameObject wall, Action wallFinished)
        {
            LerpAnimation.Instance.Move(wall, this.wallTargetFinished.position, 1, LerpAnimationType.Curve, () =>
            {
                Destroy(wall);
                wallFinished();
            });
        }
    }
}
