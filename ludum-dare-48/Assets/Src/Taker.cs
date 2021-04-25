using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace schw3de.ld48
{
    public class Taker : Singleton<Taker>
    {
        private bool _isDragging = false;
        private Vector3 _hitPosition;
        private GameObject _draggingObject = null;

        public void StopDragging()
        {
            _isDragging = false;
        }

        private void Update()
        {
            if(!_isDragging && Input.GetKeyDown(KeyCode.Space))
            {
                _hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(_hitPosition, Vector2.zero);
                var thought = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.Thought);
                //var checkIfItsInDragg = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.LetThoughtGoArea);

                if (thought.collider == null || !thought.collider.gameObject.GetComponent<Thought>().CanBeDragged())
                {
                    Debug.Log("No hit");
                }
                else
                {
                    //Hand.Instance.SetDragging();
                    _isDragging = true;
                    Debug.Log($"Hit: {thought.collider.gameObject}");
                    _draggingObject = thought.collider.gameObject;
                    _draggingObject.GetComponent<Thought>().StartDrag();
                    var rigidBody = _draggingObject.GetComponent<Rigidbody2D>();
                    //rigidBody.isKinematic = true;
                    //rigidBody.velocity = Vector2.zero;
                    //rigidBody.angularVelocity = 0;
                }
            }

            if (_isDragging)
            {
                _hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(_hitPosition, Vector2.zero);

                //var checkIfItsInDragg = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.DraggingArea);
                //if (checkIfItsInDragg.collider == null)
                //{
                //    CancelDrag();
                //    return;
                //}

                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _draggingObject.transform.position = new Vector3(mousePosition.x,
                            mousePosition.y,
                            _draggingObject.transform.position.z);
            }
        }

        private void Awake()
        {

        }

        //private void RotateRight_started(InputAction.CallbackContext obj)
        //{
        //    Debug.Log("Rotate right");
        //    if(_isDragging)
        //    {
        //        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //        _draggingObject.transform.RotateAround(mousePosition, new Vector3(0, 0, 1), 45);
        //    }
        //}

        //private void RotateLeft_started(InputAction.CallbackContext obj)
        //{
        //    if(_isDragging)
        //    {
        //        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //        _draggingObject.transform.RotateAround(mousePosition, new Vector3(0, 0, 1), -45);
        //    }
        //}

        //private void Take_canceled(InputAction.CallbackContext obj)
        //{
        //    if(_isDragging)
        //    {
        //        CancelDrag();
        //    }

        //    _isDragging = false;
        //}

        private void CancelDrag()
        {
            var thought = _draggingObject.GetComponent<Thought>();
            //thought.
            //rigidBody.isKinematic = false;
            //_isDragging = false;
            //Hand.Instance.SetUndragging();
        }

        //private void Started(InputAction.CallbackContext obj)
        //{
        //    Debug.Log($"performaed: {obj.performed}");

        //    _hitPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //    RaycastHit2D[] hits = Physics2D.RaycastAll(_hitPosition, Vector2.zero);
        //    var firstArticle = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.Article || x.collider.gameObject.tag == Tags.NextCustomerStopper);
        //    var checkIfItsInDragg = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.DraggingArea);

        //    if (firstArticle.collider == null || checkIfItsInDragg.collider == null)
        //    {
        //        Debug.Log("No hit");
        //    }
        //    else
        //    {
        //        Hand.Instance.SetDragging();
        //        _isDragging = true;
        //        Debug.Log($"Hit: {firstArticle.collider.gameObject}");
        //        _draggingObject = firstArticle.collider.gameObject;
        //        var rigidBody = _draggingObject.GetComponent<Rigidbody2D>();
        //        rigidBody.isKinematic = true;
        //        rigidBody.velocity = Vector2.zero;
        //        rigidBody.angularVelocity = 0;
        //    }

        //}

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
