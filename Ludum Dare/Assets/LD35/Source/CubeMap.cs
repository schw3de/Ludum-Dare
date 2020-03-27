using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using schw3de.LD35.Source.Cubes;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class CubeMap : Singleton<CubeMap>
    {
        [SerializeField]
        private GameObject cubeDefaultPrefab;

        [SerializeField]
        private GameObject selectionToolPrefab;

        private SelectionTool selectionTool;

        private Map<ICube> map;

        private MapPosition selectedCube;

        private MapPosition selectionToolPosition;

        private CubeMapMode currentmode;

        private bool isBlocked;

        public Action CubeGotHit { get; set; }

        public void ToggleMode()
        {
            this.currentmode = this.currentmode == CubeMapMode.MoveCube ? CubeMapMode.SelectCube : CubeMapMode.MoveCube;

            this.UpdateMode();
        }

        public void HandleDirection(CubeDirectionMovement direction)
        {
            if (this.isBlocked)
                return;

            switch (this.currentmode)
            {
                case CubeMapMode.MoveCube:
                    this.MoveCube(direction);
                    break;

                case CubeMapMode.SelectCube:
                    this.MoveSelection(direction);
                    break;
            }
        }

        public void Reset()
        {
            foreach (Transform child in this.transform)
                Destroy(child.gameObject);
        }

        public void CreateCubes(int sizeX, int sizeY, IList<StartupCubePosition> positions)
        {
            this.map = new Map<ICube>(sizeX, sizeY);

            this.CreateCubes(positions);

            this.UpdateMode();
        }

        public bool IsSelectionValid()
        {
            if (this.currentmode == CubeMapMode.MoveCube)
                return true;

            return this.currentmode == CubeMapMode.SelectCube && this.map.IsOccupied(this.selectionToolPosition);
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void CreateCubes(IList<StartupCubePosition> positions)
        {
            foreach (StartupCubePosition startup in positions)
            {
                if (startup.StartPositionType == StartPositionType.Selected)
                {
                    this.selectedCube = startup;
                    this.selectionTool = GameObjectInstantiation.Instance.Instantiate<SelectionTool>(this.selectionToolPrefab, this.GetMapPositionToWorldPositionSelectionTool(startup), this.transform);
                    this.selectionToolPosition = startup;
                }

                ICube createdCube = GameObjectInstantiation.Instance.Instantiate<ICube>(this.cubeDefaultPrefab, this.map.GetMapPositionToWorldPosition(startup), this.transform);

                createdCube.GotHit = this.CubeDestroyed;

                this.map.Set(startup, createdCube);
            }

            this.SetMode(CubeMapMode.MoveCube);
        }

        public void ResetHits()
        {
            foreach(ICube cube in this.map.GetObjects())
            {
                cube.ResetHit();
            }
        }

        private void SetMode(CubeMapMode mode)
        {
            this.currentmode = mode;
            this.UpdateMode();
        }

        private void MoveCube(CubeDirectionMovement direction)
        {
            MapPosition to = this.GetMapPosition(this.selectedCube, direction);

            if (!this.map.PositionExists(to) || this.map.IsOccupied(to) || !this.map.AreObjectsStillConnected(this.selectedCube, to))
                return;

            this.isBlocked = true;

            this.map.Get(this.selectedCube).MoveTo(this.map.GetMapPositionToWorldPosition(to), () => this.isBlocked = false);
            this.map.Set(to, this.map.Get(this.selectedCube));
            this.map.Set(this.selectedCube, null);
            this.selectedCube = to;
        }

        private void MoveSelection(CubeDirectionMovement direction)
        {
            MapPosition to = this.GetMapPosition(this.selectionToolPosition, direction);

            if (!this.map.PositionExists(to))
                return;

            this.isBlocked = true;

            LerpAnimation.Instance.Move(
                this.selectionTool.gameObject,
                this.GetMapPositionToWorldPositionSelectionTool(to),
                0.25f,
                LerpAnimationType.Curve,
                () =>
                {
                    this.isBlocked = false;
                    this.selectionTool.SetValidSelection(this.map.IsOccupied(this.selectionToolPosition));
                });
            this.selectionToolPosition = to;
        }

        private MapPosition GetMapPosition(MapPosition mapPosition, CubeDirectionMovement direction)
        {
            switch (direction)
            {
                case CubeDirectionMovement.MoveUp:
                    return mapPosition.MoveTo(MapDirection.North);

                case CubeDirectionMovement.MoveDown:
                    return mapPosition.MoveTo(MapDirection.South);

                case CubeDirectionMovement.MoveLeft:
                    return mapPosition.MoveTo(MapDirection.West);

                case CubeDirectionMovement.MoveRight:
                    return mapPosition.MoveTo(MapDirection.East);

                default:
                    throw new NotSupportedException("I cant move there. :|");
            }
        }

        private Vector3 GetMapPositionToWorldPositionSelectionTool(MapPosition position)
        {
            return this.map.GetMapPositionToWorldPosition(position) + new Vector3(0, 0, -1);
        }

        private void UpdateMode()
        {
            switch (this.currentmode)
            {
                case CubeMapMode.MoveCube:
                    this.selectedCube = this.selectionToolPosition;
                    this.map.Get(this.selectedCube).Select();
                    this.selectionTool.gameObject.SetActive(false);
                    break;

                case CubeMapMode.SelectCube:
                    this.selectionToolPosition = this.selectedCube;
                    this.map.Get(this.selectedCube).Deselect();
                    this.selectionTool.gameObject.transform.position = this.GetMapPositionToWorldPositionSelectionTool(this.selectedCube);
                    this.selectionTool.SetValidSelection(this.map.IsOccupied(this.selectionToolPosition));
                    this.selectionTool.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void CubeDestroyed(ICube cube)
        {
            this.CubeGotHit();
        }
    }
}
