using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld
{
    /// <summary>
    /// Thx to for orignal code https://www.emmaprats.com/p/how-to-rotate-the-camera-around-an-object-in-unity3d/
    /// </summary>
    public class CameraMovement : Singleton<CameraMovement>
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private Vector3 _target;
        [SerializeField] private float _distanceToTarget = 10;

        private Vector3 previousPosition;

        private new void Awake()
        {
            base.Awake();
            previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            HandleMovement();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                HandleMovement();
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target.position;
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector3 newPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 270; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 270; // camera moves vertically

            _cam.transform.position = _target;

            _cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            _cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            _cam.transform.Translate(new Vector3(0, 0, -_distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
