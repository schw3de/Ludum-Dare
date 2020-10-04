using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace schw3de.ld47
{
    public class Hand : Singleton<Hand>
    {
        [SerializeField]
        private Texture2D _dragging;
        [SerializeField]
        private Texture2D _error;
        [SerializeField]
        private Texture2D _inactive;

        private Texture2D _current;
        private bool _isDragging;

        private void Awake()
        {
            SetInactivate();
        }

        private void Update()
        {
            if(_isDragging)
            {
                return;
            }

            var hitPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D[] hits = Physics2D.RaycastAll(hitPosition, Vector2.zero);
            var checkIfItsInDragg = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.DraggingArea || x.collider.gameObject.tag == Tags.CashierSystem);
            //var checkCashier = hits.FirstOrDefault(x => x.collider.gameObject.tag == Tags.CashierSystem);
            //Debug.Log($"Hits:  {string.Join(",", hits.Select(x => x.collider.gameObject.name))}");
            if (checkIfItsInDragg.collider == null)
            {
                SetError();
            }
            else
            {
                SetInactivate();
            }
        }

        public void SetInactivate()
        {
            SetCursor(_inactive);
        }

        public void SetDragging()
        {
            _isDragging = true;
            SetCursor(_dragging);
        }

        public void SetUndragging()
        {
            _isDragging = false;
        }

        public void SetError()
        {
            SetCursor(_error);
        }

        private void SetCursor(Texture2D texture)
        {
            if(_current == texture)
            {
                return;
            }

            _current = texture;
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
    }
}
