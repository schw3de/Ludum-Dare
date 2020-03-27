using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using UnityEngine;

namespace schw3de.LD35.Source.Cubes
{
    public class BaseCube : MonoBehaviour, ICube
    {
        [SerializeField]
        private Material selectedMaterial;

        [SerializeField]
        private Material defaultMaterial;

        [SerializeField]
        private Material hitMaterial;

        private bool isHit;

        public Action<ICube> GotHit { get; set; }

        public void MoveTo(Vector3 position, Action finishedMoving)
        {
            LerpAnimation.Instance.Move(this.gameObject, position, 0.15f, LerpAnimationType.Curve, finishedMoving);
        }

        public void ResetHit()
        {
            this.isHit = false;

            this.gameObject.GetComponent<Renderer>().material = this.defaultMaterial;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (this.isHit || collider.gameObject.tag != "WallCube")
                return;

            this.isHit = true;

            this.gameObject.GetComponent<Renderer>().material = this.hitMaterial;

            this.GotHit(this);
        }

        public void Select()
        {
            this.gameObject.GetComponent<Renderer>().material = this.selectedMaterial;
        }

        public void Deselect()
        {
            this.gameObject.GetComponent<Renderer>().material = this.defaultMaterial;
        }
    }
}
