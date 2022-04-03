using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{
    public class CubeSideCreation
    {
        public CubeSideState[] CubeSideStates { get; set; }

        public CubeSideCreation(CubeSideState[] cubeSideStates)
        {
            CubeSideStates = cubeSideStates;
        }

        public void ShuffleCubeSideStates()
        {
            CubeSideStates = Randomizer.ShuffleArray(CubeSideStates);
        }
    }
}
