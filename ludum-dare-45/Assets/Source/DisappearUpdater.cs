using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.MRnothing
{
    public class DisappearUpdater
    {
        private float addValue = 0.01f;

        private Material material;

        private bool disappear = true;

        private const string disolvePowerProperty = "_DissolvePower";

        private bool alwaysAppear;

        private Transform scaleTransform;

        public DisappearUpdater(Material material, Transform transform)
        {
            this.material = material;
            this.material.SetFloat(disolvePowerProperty, 0);
            this.scaleTransform = transform;
        }

        public void Disappear()
        {
            if(this.alwaysAppear)
            {
                return;
            }

            this.disappear = true;
        }

        public void Appear()
        {
            this.disappear = false;
        }

        public void Update()
        {
            //return;
            float value = this.material.GetFloat(disolvePowerProperty);
            if (!this.disappear && value != 1)
            {
                value += this.addValue;
                value = Math.Min(1.0f, value);

                this.material.SetFloat(disolvePowerProperty, value);
            }
            else if(this.disappear && value != 0)
            {
                value -= this.addValue;
                value = Math.Max(0.0f, value);
                this.material.SetFloat(disolvePowerProperty, value);
            }
        }

        public void AppearForEver()
        {
            this.alwaysAppear = true;
            this.Appear();
        }
    }
}
