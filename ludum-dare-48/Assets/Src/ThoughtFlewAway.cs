﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace schw3de.ld48
{
    public class ThoughtFlewAway : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == Tags.Thought)
            {
                collision.GetComponent<Thought>().FlewAway();
            }
        }
    }
}