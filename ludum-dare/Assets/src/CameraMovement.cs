using schw3de.ld.utils;
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
        private float _distanceToTarget = 50;

        private Vector3 _previousPosition;
        private Vector3 _originalPosition;
        private Timer _blockTimer = new Timer();

        private new void Awake()
        {
            base.Awake();
            _originalPosition = _cam.transform.position;
            _cam.transform.LookAt(_target);
            //previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            //previousPosition = new Vector3(200, 200, 0);
            //HandleMovement();
        }

        void Update()
        {
            if(!_blockTimer.IsFinished())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                HandleMovement();
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target.position;
            _previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            HandleMovement();
        }

        public void SetCamToTop()
        {
            _cam.transform.position = new Vector3(0, _distanceToTarget, 0);
            _cam.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        private void HandleMovement()
        {
            Vector3 newPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = _previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 270; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 270; // camera moves vertically

            _cam.transform.position = _target;

            _cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            _cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            _cam.transform.Translate(new Vector3(0, 0, -_distanceToTarget));

            _previousPosition = newPosition;
        }

        public void Block(TimeSpan blockTime) => _blockTimer.Start(blockTime);
    }
}
