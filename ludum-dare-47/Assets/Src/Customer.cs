using System;
using TMPro;
using UnityEngine;

namespace schw3de.ld47.utils
{
    public class Customer : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1.0f;
        [SerializeField]
        private TextMeshPro _satificationTimer;

        private Vector3 _targetPosition;
        private bool _destoryOnArrival;
        private Func<bool> _currentWaitCondition;
        private DateTime _endTimeSatisfaction;
        private bool _stopSatisfactionTimer;

        public bool HasArrived { get; private set; }

        public Guid Id { get; set; }

        public double Satification => Math.Round((_endTimeSatisfaction - DateTime.UtcNow).TotalSeconds);

        public void SetTargetPosition(Transform target, bool destroyOnArrival, Func<bool> currentWaitCondition = null)
        {
            _currentWaitCondition = currentWaitCondition;
            _targetPosition = transform.position.ChangeX(target.position.x);
            _destoryOnArrival = destroyOnArrival;
            HasArrived = false;
        }

        public void SetSatisfaction(TimeSpan timespan)
        {
            _endTimeSatisfaction = DateTime.UtcNow + timespan;
        }

        public void StopSatisfactionTimer()
        {
            _stopSatisfactionTimer = true;
        }

        private void Awake()
        {
            _satificationTimer.text = string.Empty;
        }

        private void Update()
        {
            if(!_stopSatisfactionTimer && _endTimeSatisfaction != default)
            {
                _satificationTimer.text = Satification.ToString() + " sec(s)";
            }

            if(_currentWaitCondition != null && _currentWaitCondition())
            {
                return;
            }

            _currentWaitCondition = null;

            if (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);
            }
            else if (_destoryOnArrival)
            {
                Destroy(gameObject);
            }
            else
            {
                HasArrived = true;
            }
        }
    }
}
