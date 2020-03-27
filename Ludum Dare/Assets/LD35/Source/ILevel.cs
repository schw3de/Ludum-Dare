using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schw3de.LD35.Source
{
    public interface ILevel
    {
        IList<StartupCubePosition> GetStartup();

        IList<Wall> GetWalls();
    }
}
