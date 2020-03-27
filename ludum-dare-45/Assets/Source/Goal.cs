using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace schw3de.MRnothing
{
    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Goal Reached");
            Game.Instance.FinishedLevel();

        }
    }
}
