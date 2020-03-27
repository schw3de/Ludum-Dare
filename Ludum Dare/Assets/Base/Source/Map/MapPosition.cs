using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schw3de.Base.Source
{
    public class MapPosition
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public MapPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public MapPosition MoveTo(MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.North:
                    return new MapPosition(this.X, this.Y + 1);

                case MapDirection.NorthEast:
                    return new MapPosition(this.X + 1, this.Y + 1);

                case MapDirection.East:
                    return new MapPosition(this.X + 1, this.Y);

                case MapDirection.SouthEast:
                    return new MapPosition(this.X + 1, this.Y - 1);

                case MapDirection.South:
                    return new MapPosition(this.X, this.Y - 1);

                case MapDirection.SouthWest:
                    return new MapPosition(this.X - 1, this.Y - 1);

                case MapDirection.West:
                    return new MapPosition(this.X - 1, this.Y);

                case MapDirection.NorthWest:
                    return new MapPosition(this.X - 1, this.Y + 1);

                default:
                    throw new NotSupportedException(string.Format("Wtf i dont know that \"{0}\" direction :-S", direction));
            }
        }
    }
}
