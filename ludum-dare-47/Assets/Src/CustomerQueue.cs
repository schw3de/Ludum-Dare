using schw3de.ld47.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    public class CustomerQueue : Singleton<CustomerQueue>
    {
        [SerializeField]
        private Transform _firstPosition;
        [SerializeField]
        private Transform _payPosition;
        [SerializeField]
        private Transform _leavePosition;
        [SerializeField]
        private List<GameObject> _customers;
        private Customer _nextCustomer;
        private Queue<Guid> _customerIds;

        public Customer ActiveCustomer { get; set; }


        private void CreateCustomer(Transform targetPosition, Func<bool> waitCondition = null)
        {
            if (ActiveCustomer == null)
            {
                ActiveCustomer = Instantiate(_customers.GetRandomItem()).GetComponent<Customer>();
                ActiveCustomer.SetTargetPosition(targetPosition, false, waitCondition);
                ActiveCustomer.Id = _customerIds.Dequeue();
            }
            else
            {
                _nextCustomer = Instantiate(_customers.GetRandomItem()).GetComponent<Customer>();
                _nextCustomer.SetTargetPosition(targetPosition, false, waitCondition);
                _nextCustomer.Id = _customerIds.Dequeue();
            }
        }

        public void Init(List<Guid> customers)
        {
            _customerIds = new Queue<Guid>(customers);
            CreateCustomer(_payPosition);

            if(_customerIds.Any())
            {
                CreateCustomer(_firstPosition, () => !ActiveCustomer.HasArrived);
            }
        }

        public void CustomerHasPaid()
        {
            Debug.Log($"Customer has paid: {ActiveCustomer.Id}");
            ActiveCustomer.SetTargetPosition(_leavePosition, true);

            if(_nextCustomer != null && ActiveCustomer != _nextCustomer)
            {
                ActiveCustomer = _nextCustomer;
                ActiveCustomer.SetTargetPosition(_payPosition, false);
            }

            if (_customerIds.Any())
            {
                CreateCustomer(_firstPosition);
            }
        }
    }
}
