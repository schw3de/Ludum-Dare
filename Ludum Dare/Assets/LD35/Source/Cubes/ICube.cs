using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public interface ICube
    {
        Action<ICube> GotHit { get; set; }

        void ResetHit();

        void MoveTo(Vector3 position, Action finishedMoving);

        void Select();

        void Deselect();
    }
}
