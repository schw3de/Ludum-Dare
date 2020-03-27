using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace schw3de.LD35.Source
{
    public class SelectionTool : MonoBehaviour
    {
        public void SetValidSelection(bool isValid)
        {
            if(isValid)
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
