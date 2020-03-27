using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.Base.Source
{
    public class Map<T> where T : class
    {
        private T[,] map;

        public Map(int xSize, int ySize)
        {
            this.map = new T[xSize, ySize];
        }

        private Map(T[,] map)
        {
            this.map = map;
        }

        public int XSize
        {
            get { return this.map.GetLength(0); }
        }

        public int YSize
        {
            get { return this.map.GetLength(1); }
        }

        public T Get(MapPosition position)
        {
            if (!this.PositionExists(position))
                throw new IndexOutOfRangeException("Position is not inside the map!");

            return this.map[position.X, position.Y];
        }

        public void Set(MapPosition position, T insert)
        {
            if (!this.PositionExists(position))
                throw new IndexOutOfRangeException("Position is not inside the map!");

            //if (this.map[position.X, position.Y] != null)
            //    throw new NotSupportedException("Position is already been taken");

            this.map[position.X, position.Y] = insert;
        }

        public bool PositionExists(MapPosition position)
        {
            return (position.X >= 0
                    && position.Y >= 0
                    && position.X < this.XSize
                    && position.Y < this.YSize);
        }

        public bool IsOccupied(MapPosition position)
        {
            if (!this.PositionExists(position))
                throw new IndexOutOfRangeException("Position is not inside the map!");

            return this.Get(position) != null;
        }

        public Vector3 GetMapPositionToWorldPosition(MapPosition position)
        {
            return new Vector3(position.X, position.Y);
        }

        public bool AreObjectsStillConnected(MapPosition from, MapPosition to)
        {
            Map<T> cloned = this.Clone();

            cloned.Set(to, cloned.Get(from));
            cloned.Set(from, null);

            IList<MapPosition> occupiedPositions = cloned.GetOccupiedPositions();

            if (occupiedPositions.Count() == 1)
                return true;

            foreach (MapPosition occupiedPosition in occupiedPositions)
            {
                if (!cloned.HasNeighbour(occupiedPosition))
                    return false;
            }

            return true;
        }

        public bool HasNeighbour(MapPosition position)
        {
            foreach(MapDirection direction in Enum.GetValues(typeof(MapDirection)))
            {
                MapPosition neighbor = position.MoveTo(direction);

                if (!this.PositionExists(neighbor) || this.Get(neighbor) == null)
                    continue;

                return true;
            }

            return false;
        }

        public IList<MapPosition> GetOccupiedPositions()
        {
            IList<MapPosition> foundOccupiedPositions = new List<MapPosition>();

            for (int x = 0; x < this.XSize; x++)
                for (int y = 0; y < this.YSize; y++)
                    if (this.map[x, y] != null)
                        foundOccupiedPositions.Add(new MapPosition(x, y));

            return foundOccupiedPositions;
        }

        public IList<T> GetObjects()
        {
            IList<T> result = new List<T>();

            foreach(MapPosition position in this.GetOccupiedPositions())
            {
                result.Add(this.Get(position));
            }

            return result;
        }

        private Map<T> Clone()
        {
            return new Map<T>(this.map.Clone() as T[,]);
        }
    }
}
