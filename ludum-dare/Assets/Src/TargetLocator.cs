using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld49
{

    // Thx to https://www.youtube.com/watch?v=U1SdjGUFSAI
    public class TargetLocator : MonoBehaviour
    {
        public Transform Target;
        public float HideDistance;

        private void Update()
        {
            var direction = Target.position - transform.position;

            if(direction.magnitude < HideDistance)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);

                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
