using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld49
{
    public class ShipMovement : MonoBehaviour
    {

        public Transform Thrusters1;
        public Transform Thrusters2;

        public Vector2 Force = new Vector2(0,1);

        public Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Vector3 direction = body.transform.position - transform.position;
            //body.AddForceAtPosition(direction.normalized, transform.position);
            //_rigidbody2D.AddForceAtPosition()

        }
    }
}
