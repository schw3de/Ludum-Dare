using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace schw3de.ld47
{
    public class Game : Singleton<Game>
    {
        [SerializeField]
        private Button _checkoutButton;

        public void Awake()
        {
            _checkoutButton.onClick.RemoveAllListeners();
            _checkoutButton.onClick.AddListener(() => Checkout());
        }

        private void Checkout()
        {
            CustomerQueue.Instance.CustomerGotPaid();
        }
    }
}
