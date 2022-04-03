using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{
    public class CubeActions
    {
        public Action<Cube> CubeDestroyed { get; set; }

        public CubeActions(Action<Cube> cubeDestroyed)
        {
            CubeDestroyed = cubeDestroyed;
        }
    }
}
