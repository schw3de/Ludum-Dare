using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using schw3de.Base.Source;

namespace schw3de.LD34.Source
{
    public class Player : Singleton<Player>
    {
        public float MovementSpeed = 50.0f;

        public float RotationSpeed;

        public float MaxSpeed = 50.0f;

        private Transform target;

        private Rigidbody targetRigidbody;

        public Transform Target
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target = value;
                this.targetRigidbody = this.target.GetComponent<Rigidbody>();
            }
        }

        public Animator Animator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void FixedUpdate()
        {
            this.CheckRotatingDirection();

            this.CheckActions();

            this.CheckMovement();
            //float step = this.RotationSpeed * Time.deltaTime;

            //Vector3 rawMousePosition = Input.mousePosition;

            //rawMousePosition.z = 50.0f;

            //RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(rawMousePosition));

            //foreach (RaycastHit hit in hits)
            //{
            //    if(hit.collider.tag == "Playeraxis")
            //    {
            //        this.Target.LookAt(hit.transform);

            //        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
            //        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            //        mouseWorldPosition.y = 0;

            //        Instantiate<GameObject>(Create).transform.position = mouseWorldPosition;

            //        return;
            //    }
            //}

            //Vector3 lookAt = new Vector3(this.Target.position.x, mousePosition.y, this.Target.position.z);

            //Instantiate<GameObject>(Create).transform.position = mousePosition;

            //this.Target.LookAt(lookAt);

            //if(Input.GetKeyDown(KeyCode.W))
            //{
            //    this.rigidbody.AddForce(this.transform.forward * 100);
            //}
        }

        private void CheckActions()
        {
            this.Animator.ResetTrigger("ChopAxt");

            //Debug.Log(this.Animator.GetBool("ChopAxt"));
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Trigger animation");
                this.Animator.SetTrigger("ChopAxt");

                //Ray ray = new Ray(this.Target.forward, this.Target.forward);
                //Debug.DrawRay(this.Target.forward, this.Target.forward * 1, Color.blue, 1);
                //RaycastHit info;
                //Physics.Raycast(ray, out info);
                //if (info.collider != null)
                //{
                //    Debug.Log(info.collider.tag);
                //    if(info.collider.tag == "Forestelement")
                //    {
                //        var forestelement = info.collider.GetComponent<ForestElement>();
                //        forestelement.Destroy();
                //    }
                //}
            }

            //if(Input.GetMouseButtonUp(0))
            //{
            //    this.Animator.SetBool("ChopAxt", false);
            //}
        }

        private void CheckMovement()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 force = this.Target.forward * this.MovementSpeed; // = however you want to determine that
                                                                          // First find out what your modifier would be if the force
                                                                          // direction was in the direction of the current velocity
                float straightMultiplier = 1 - (this.targetRigidbody.velocity.magnitude / this.MaxSpeed);
                // This value will be 1 if the rigidbody is moving at 0,
                // and 0 if the rigidbody is moving at maxSpeed.
                // Then, find out what the dot product is between the force and the velocity
                float forceDot = Vector3.Dot(this.targetRigidbody.velocity, force);
                // Now, smoothly interpolate between full power and modified power
                // depending on what direction the force is going!
                Vector3 modifiedForce = force * straightMultiplier;
                Vector3 correctForce = Vector3.Lerp(force, modifiedForce, forceDot);

                this.targetRigidbody.AddForce(correctForce);
                // this.targetRigidbody.velocity = this.Target.forward * this.MovementSpeed;
            }
        }

        private void CheckRotatingDirection()
        {
            Vector3 rawMousePosition = Input.mousePosition;

            rawMousePosition.z = 50.0f;

            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(rawMousePosition));

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "Playeraxis")
                {
                    this.Target.LookAt(hit.point);

                    return;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            this.Target = this.transform;
            this.Animator = this.GetComponent<Animator>();
        }
    }
}
