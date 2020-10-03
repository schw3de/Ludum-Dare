using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace schw3de.ld47
{
    public class Treadmill : MonoBehaviour
    {
        [SerializeField]
        private bool _registerInput = false;
        private bool _isOn = false;

        private void Awake()
        {
            if(_registerInput)
            {
                Controls.Instance.Asset.Cashier.ActivateTreadmill.started += ActivateTreadmill_performed; // (context) => _isOn = true;
                Controls.Instance.Asset.Cashier.ActivateTreadmill.canceled += ActivateTreadmill_performed; // (context) => _isOn = false;
                
            }
        }

        private void ActivateTreadmill_performed(InputAction.CallbackContext context)
        {
            Debug.Log($"Context Started: {context.started} Context Cancelled: {context.canceled}");
            _isOn = context.started;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnCollisionStay2D(Collision2D collision)
        {
            if(collision.gameObject.tag == Tags.Article && _isOn)
            {
                Vector3 currentPosition = collision.gameObject.transform.position;
                //collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(currentPosition + new Vector3(1,0,0));
                var currentVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, currentVelocity.y);
            }
        }
    }
}