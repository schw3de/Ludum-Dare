using UnityEngine;
using UnityEngine.InputSystem;

namespace schw3de.ld47
{
    public class Treadmill : MonoBehaviour
    {
        [SerializeField]
        private bool _alwaysOn = false;

        private bool _isOn = false;

        public void ApplyFeature()
        {
            if (_alwaysOn)
            {
                return;
            }

            Controls.Instance.Asset.Cashier.ActivateTreadmill.started -= ActivateTreadmill_performed; 
            Controls.Instance.Asset.Cashier.ActivateTreadmill.canceled -= ActivateTreadmill_performed;
            
            if (!Features.Instance.AutomaticTreadmill)
            {
                Controls.Instance.Asset.Cashier.ActivateTreadmill.started += ActivateTreadmill_performed;
                Controls.Instance.Asset.Cashier.ActivateTreadmill.canceled += ActivateTreadmill_performed;
            }
        }

        private void ActivateTreadmill_performed(InputAction.CallbackContext context)
        {
            Debug.Log($"Context Started: {context.started} Context Cancelled: {context.canceled}");
            _isOn = context.started;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_alwaysOn || ((collision.gameObject.tag == Tags.Article || collision.gameObject.tag == Tags.NextCustomerStopper) && _isOn))
            {
                var currentVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Features.Instance.TreadmillSpeed, currentVelocity.y);
            }
        }
    }
}