using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class Wall
    {

        public Wall(IList<MapPosition> freePositions, float duration)
        {
            this.FreePositions = freePositions;

            this.Duration = duration;
        }

        public IList<MapPosition> FreePositions { get; private set; }

        public float Duration { get; private set; }

        public Action WallComplete { get; set; }
    }
}
