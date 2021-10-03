using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld49
{
    public class Ship : Singleton<Ship>
    {

        public Transform Thrusters1;
        public ParticleSystem Thruster1Particles;

        public Transform Thrusters2;
        public ParticleSystem Thrusters2Partocles;

        public Vector2 Force = new Vector2(0,1);
        public float RotationSpeed = 0.5f;

        public decimal FuelCost = 0.1m;
        public decimal Fuel = 100;

        private Rigidbody2D _rigidbody2D;
        private bool _thrust;
        private bool _turnLeft;
        private bool _turnRight;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.Space) && Fuel > 0)
            {
                Debug.Log("Add Force");
                //Thruster1Particles.Play();
                //Vector3 direction = _rigidbody2D.transform.position - transform.position;
                //_rigidbody2D.AddForceAtPosition(direction.normalized * Force, Thrusters2.position);
                //_rigidbody2D.AddForceAtPosition()
                _thrust = true;
                if(Fuel <= 0)
                {
                    Fuel = 0;
                }
                else
                {
                    Fuel -= FuelCost;
                }
            }
            else
            {
                _thrust = false;
                //Thruster1Particles.Pause();
            }

            if(Input.GetKey(KeyCode.A))
            {
                _turnRight = true;
                _turnLeft = false;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                _turnLeft = true;
                _turnRight = false;
            }
            else 
            {
                _turnLeft = false;
                _turnRight = false;
            }
        }

        private void FixedUpdate()
        {
            if(_thrust)
            {
                var force = Force * transform.up;
                Debug.Log($"Force: {force}");
                //_rigidbody2D.AddForce(force);
                //_rigidbody2D.AddForceAtPosition(force, Thrusters1.transform.position);
                _rigidbody2D.AddForce(force);
            }

            if(_turnLeft)
            {
                _rigidbody2D.AddTorque(RotationSpeed);
            }
            else if(_turnRight)
            {
                _rigidbody2D.AddTorque(-RotationSpeed);
            }
        }
    }
}
