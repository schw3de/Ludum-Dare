using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;

namespace schw3de.LD35.Source
{
    public class Level1 : ILevel
    {
        public IList<StartupCubePosition> GetStartup()
        {
            return new List<StartupCubePosition>()
                {
                    new StartupCubePosition(4, 6, StartPositionType.Default, CubeType.Default),
                    new StartupCubePosition(5, 4, StartPositionType.Default, CubeType.Default),
                    new StartupCubePosition(5, 5, StartPositionType.Default, CubeType.Default),
                    new StartupCubePosition(6, 6, StartPositionType.Default, CubeType.Default),
                    new StartupCubePosition(5, 6, StartPositionType.Selected, CubeType.Default),
                };
        }

        public IList<Wall> GetWalls()
        {
            IList<Wall> walls = new List<Wall>();

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(5, 6),
                    new MapPosition(3, 5),
                    new MapPosition(4, 5),
                    new MapPosition(5, 5),
                    new MapPosition(6, 5),
                    new MapPosition(7, 5),
                    new MapPosition(5, 4)
                },
                1500.0f));

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(0, 10),
                    new MapPosition(1, 9),
                    new MapPosition(2, 9),
                    new MapPosition(3, 9),
                    new MapPosition(1, 8),
                    new MapPosition(2, 8),
                    new MapPosition(3, 8),
                    new MapPosition(3, 7),
                    new MapPosition(4, 8),
                    new MapPosition(4, 7),
                    new MapPosition(4, 6),
                    new MapPosition(5, 6),
                    new MapPosition(5, 7),
                },
                3000.0f));

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(6, 2),
                    new MapPosition(7, 2),
                    new MapPosition(8, 2),
                    new MapPosition(6, 3),
                    new MapPosition(7, 3),
                    new MapPosition(8, 3),
                    new MapPosition(6, 4),
                    new MapPosition(7, 4),
                    new MapPosition(8, 4),
                },
                4000.0f));

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(7, 0),
                    new MapPosition(8, 0),
                    new MapPosition(9, 0),
                    new MapPosition(8, 1),
                    new MapPosition(8, 2),
                    new MapPosition(7, 3),
                    new MapPosition(8, 3),
                    new MapPosition(9, 3),
                    new MapPosition(8, 4),
                    new MapPosition(7, 5),
                    new MapPosition(8, 5),
                    new MapPosition(9, 5),
                },
                1000.0f));

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(1, 1),
                    new MapPosition(2, 1),
                    new MapPosition(7, 1),
                    new MapPosition(8, 1),
                    new MapPosition(9, 1),
                    new MapPosition(1, 2),
                    new MapPosition(2, 2),
                    new MapPosition(7, 2),
                    new MapPosition(8, 2),
                    new MapPosition(9, 2),
                    new MapPosition(7, 3),
                    new MapPosition(8, 3),
                    new MapPosition(9, 3),
                },
                5000.0f));

            walls.Add(new Wall(
                new List<MapPosition>
                {
                    new MapPosition(5, 4),
                    new MapPosition(4, 5),
                    new MapPosition(5, 5),
                    new MapPosition(6, 5),
                    new MapPosition(5, 6),
                },
                3000.0f));

            return walls;
        }
    }
}
