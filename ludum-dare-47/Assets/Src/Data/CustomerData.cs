using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    [CreateAssetMenu(fileName = "Customer", menuName = "Data/CustomerData", order = 1)]
    public class CustomerData : ScriptableObject
    {
        public int AmountArticles;
    }
}
