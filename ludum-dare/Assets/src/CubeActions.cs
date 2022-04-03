using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{
    public class CubeActions
    {
        public Action<Cube> CountdownChanged { get; set; }
        public Action<Cube> BombClicked { get; set; }

        public CubeActions(Action<Cube> countdownChanged, Action<Cube> bombClicked)
        {
            CountdownChanged = countdownChanged;
            BombClicked = bombClicked;
        }
    }
}
