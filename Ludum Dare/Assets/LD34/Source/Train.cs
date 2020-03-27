using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class Train : Singleton<Train>
    {
        private float MovementSpeed = 10.0f;

        private bool startMoving;

        private void Update()
        {
            if (!startMoving)
                return;

            this.transform.Translate(this.transform.up * this.MovementSpeed * Time.deltaTime);
        }

        public void StartMoving()
        {
            this.startMoving = true;
        }
    }
}
