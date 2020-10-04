using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public List<CustomerData> Customers;
    }
}
