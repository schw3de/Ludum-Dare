using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace schw3de.ld47
{
    public class Taker : Singleton<Taker>
    {
        private bool _isDragging = false;
        private Vector3 _hitPosition;
        private GameObject _draggingObject = null;

        private void Update()
        {
            if(_isDragging)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _draggingObject.transform.position = new Vector3(mousePosition.x,
                            mousePosition.y,
                            _draggingObject.transform.position.y);
            }
        }

        private void Awake()
        {
            Controls.Instance.Asset.Cashier.Take.started += Started;
            Controls.Instance.Asset.Cashier.Take.canceled += Take_canceled;
            Controls.Instance.Asset.Cashier.RotateLeft.started += RotateLeft_started;
            Controls.Instance.Asset.Cashier.RotateRight.started += RotateRight_started;
        }

        private void RotateRight_started(InputAction.CallbackContext obj)
        {
            Debug.Log("Rotate right");
            if(_isDragging)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _draggingObject.transform.RotateAround(mousePosition, new Vector3(0, 0, 1), 45);
            }
        }

        private void RotateLeft_started(InputAction.CallbackContext obj)
        {
            if(_isDragging)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _draggingObject.transform.RotateAround(mousePosition, new Vector3(0, 0, 1), -45);
            }
        }

        private void Take_canceled(InputAction.CallbackContext obj)
        {
            if(_isDragging)
            {
                var rigidBody = _draggingObject.GetComponent<Rigidbody2D>();
                rigidBody.isKinematic = false;
            }

            _isDragging = false;
        }

        private void Started(InputAction.CallbackContext obj)
        {
            Debug.Log($"performaed: {obj.performed}");
   
            _hitPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D[] hits = Physics2D.RaycastAll(_hitPosition, Vector2.zero);
            var firstArticle = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.Article);

            if (firstArticle.collider == null)
            {
                Debug.Log("No hit");
            }
            else
            {
                _isDragging = true;
                Debug.Log($"Hit: {firstArticle.collider.gameObject}");
                _draggingObject = firstArticle.collider.gameObject;
                var rigidBody = _draggingObject.GetComponent<Rigidbody2D>();
                rigidBody.isKinematic = true;
                rigidBody.velocity = Vector2.zero;
                rigidBody.angularVelocity = 0;
            }

        }

        void OnDrawGizmosSelected()
        {
            if(_isDragging)
            {
                // Draw a semitransparent blue cube at the transforms position
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(_hitPosition, new Vector3(0.1f, 0.1f, 0.1f));
            }
        }
    }
}
