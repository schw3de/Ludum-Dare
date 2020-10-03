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
        public Customer ActiveCustomer { get; set; }

        private void Awake()
        {
            ActiveCustomer = Instantiate(_customers.First()).GetComponent<Customer>();
            ActiveCustomer.SetTargetPosition(_payPosition, false);
            StartCoroutine(InitQueue());
        }

        private IEnumerator InitQueue()
        {
            yield return new WaitUntil(() => ActiveCustomer.HasArrived);
            _nextCustomer = Instantiate(_customers.First()).GetComponent<Customer>();
            _nextCustomer.SetTargetPosition(_firstPosition, false);
        }

        public void CustomerGotPaid()
        {
            ActiveCustomer.SetTargetPosition(_leavePosition, true);
            ActiveCustomer = _nextCustomer;
            ActiveCustomer.SetTargetPosition(_payPosition, false);
            _nextCustomer = Instantiate(_customers.First()).GetComponent<Customer>();
            _nextCustomer.SetTargetPosition(_firstPosition, false);
        }
    }
}
