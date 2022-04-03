using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace schw3de.ld.utils
{
    [CustomEditor(typeof(GameAssets))]
    public class GameAssetsEditor : Editor
    {
        private GameAssets _gameAssets;

        private void OnEnable()
        {
            // Method 1
            _gameAssets = target as GameAssets;
        }

        public void AssignFields()
        {
            //_gameAssets.CubePrefab = AssetDatabase.LoadAssetAtPath<>
        }
    }
}
