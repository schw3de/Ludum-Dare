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

        private GameObject _cubeParent;
        private List<Cube> _cubes = new List<Cube>();

        private static readonly Vector3[] positions = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(5, 0, 0),
            new Vector3(0, 5, 0),
            new Vector3(0, 0, 5),
        };

        private new void Awake()
        {
            base.Awake();
            Debug.Log("Awake GameHeart");
            _cubeParent = new GameObject("CubeParent");

            CreateCube();
            StartCountDown();
        }

        public void CreateCube()
        {
            var cubeGo = Instantiate(PrefabCube, _cubeParent.transform);
            cubeGo.transform.localPosition = positions[_cubes.Count];
            cubeGo.SetActive(true);

            _cubes.Add(CubeCreator.CreateCube(cubeGo, GameAssets.Instance.CubeFont));
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
