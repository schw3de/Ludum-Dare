using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace schw3de.ld.utils
{
    [CustomEditor(typeof(GameHeart))]
    public class GameHeartEditor : Editor
    {
        private GameHeart _gameHeart;

        private void OnEnable()
        {
            // Method 1
            _gameHeart = target as GameHeart;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create Cubes"))
            {
                _gameHeart.CreateCubes();
            }

            if (GUILayout.Button("Start Countdown"))
            {
                _gameHeart.StartCountDown();
            }


            // Draw default inspector after button...
            base.OnInspectorGUI();
        }
    }
}
