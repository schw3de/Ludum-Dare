using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/CustomerData", order = 1)]
    public class CustomerData : ScriptableObject
    {
        public Sprite Sprite;
    }
}
