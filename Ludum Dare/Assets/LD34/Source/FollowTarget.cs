using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class FollowTarget : Singleton<FollowTarget>
    {
        public Transform Target { get; set; }

        private float smooth = 5;

        private void FixedUpdate()
        {
            if (this.Target == null)
                return;

            float x = Mathf.Lerp(transform.position.x, this.Target.position.x, Time.deltaTime * smooth);
            float z = Mathf.Lerp(transform.position.z + 2, this.Target.position.z, Time.deltaTime * smooth);

            Vector3 moveTo = new Vector3(x, this.transform.position.y, z);

            this.transform.position = moveTo;
        }
    }
}
