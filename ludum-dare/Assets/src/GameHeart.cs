using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld
{
    public class GameHeart : Singleton<GameHeart>
    {
        public GameObject PrefabCube;
        public TMP_FontAsset CubeFont;

        private GameObject _cubeParent;
        private List<Cube> _cubes = new List<Cube>();

        private new void Awake()
        {
            base.Awake();
            Debug.Log("Awake GameHeart");
            _cubeParent = new GameObject("CubeParent");
        }

        public void CreateCubes()
        {
            var cubeGo = Instantiate(PrefabCube, _cubeParent.transform);
            cubeGo.SetActive(true);

            _cubes.Add(CubeCreator.CreateCube(cubeGo, CubeFont));
        }

        public void StartCountDown()
        {
            foreach(var cube in _cubes)
            {
                cube.StartCountDowns(9);
            }
        }
    }
}
