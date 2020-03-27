using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;

namespace schw3de.LD35.Source
{
    public class StartupCubePosition : MapPosition
    {
        public StartPositionType StartPositionType { get; private set; }

        public CubeType CubeType { get; private set; }

        public StartupCubePosition(int x, int y, StartPositionType type, CubeType cubeType)
            : base(x, y)
        {
            this.StartPositionType = type;
            this.CubeType = cubeType;
        }
    }
}
