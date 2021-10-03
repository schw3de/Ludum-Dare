using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld49
{
    public class ShipUi : MonoBehaviour
    {
        public TextMeshProUGUI AltitudeText;
        public TextMeshProUGUI FuelText;
        
        private void Update()
        {
            var altidude = (int)Ship.Instance.transform.position.y - LandingPlattform.Instance.transform.position.y;
            //altidude = Math.Floor(Convert.ToDouble(altidude));
            altidude = Mathf.Max(altidude* 10, 0);
            AltitudeText.text = $"{altidude}m";

            FuelText.text = $"{Math.Max(Math.Round(Ship.Instance.Fuel), 0)}%";
        }
    }
}
