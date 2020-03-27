using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using schw3de.LD34.Source.Events;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class Forest : Singleton<Forest>
    {
        public GameObject PlaneToGenerateForstElements;

        public GameObject Tree;

        public GameObject ParentRandomPoints;

        public event EventHandler<ForestElementCreatedEventArgs> ForestElementCreated;

        public event EventHandler<ForestElementDestroyedEventArgs> ForestElementDestroyed;

        private int amountForestElements;

        private bool startCreating;

        private bool isGenerating;

        private List<Transform> usedPoints = new List<Transform>();

        private List<Transform> randomPoints;

        // private UnityEngine.Random random;

        public void CreateForest()
        {
            this.startCreating = true;
        }

        private void Update()
        {
            if(this.startCreating && !this.isGenerating || this.randomPoints.Count == 0)
                StartCoroutine(this.CreateForestElement());
        }

        private IEnumerator CreateForestElement()
        {
            this.isGenerating = true;

            //float min = this.PlaneToGenerateForstElements.GetComponent<MeshFilter>().mesh.bounds.min;
            //float max = this.PlaneToGenerateForstElements.GetComponent<MeshFilter>().mesh.bounds.max;
            //SomeObject.transform.position = plane.transform.position - new Vector3((Random.Range(min.x * 5, max.x * 5)), plane.transform.position.y, (Random.Range(min.z * 5, max.z * 5)));

            var forestElementGo = Instantiate(this.Tree);

            int maxRandomPoints = this.randomPoints.Count - 1;
            int index = UnityEngine.Random.Range(0, maxRandomPoints);

            Transform randomPoisition = this.randomPoints[index];

            this.randomPoints.RemoveAt(index);
            this.usedPoints.Add(randomPoisition);

            forestElementGo.transform.position = randomPoisition.position;
            
            forestElementGo.transform.localScale.Scale(Vector3.zero);

            var forestElement = forestElementGo.GetComponent<ForestElement>();
            forestElement.OnForestElementDestroyed += OnForestElementDestroyed;

            this.OnForestElementCreated(forestElement);

            yield return new WaitForSeconds(10.0f);

            this.isGenerating = false;
        }

        internal void DisableTreeCreating()
        {
            this.startCreating = false;
        }

        private void OnForestElementDestroyed(object sender, ForestElementDestroyedEventArgs e)
        {
            this.amountForestElements--;

            var args = new ForestElementDestroyedEventArgs();
            args.ForestElement = e.ForestElement;
            args.AmountForestElements = this.amountForestElements;

            if (this.ForestElementDestroyed != null)
                this.ForestElementDestroyed(this, args);
        }

        private void OnForestElementCreated(ForestElement forestElement)
        {
            this.amountForestElements++;

            var args = new ForestElementCreatedEventArgs();
            args.ForestElement = forestElement;
            args.AmountForestElements = this.amountForestElements;

            if (this.ForestElementCreated != null)
                this.ForestElementCreated(this, args);
        }

        protected override void Awake()
        {
            base.Awake();

            this.randomPoints = this.ParentRandomPoints.GetComponentsInChildren<Transform>().ToList();

            // this.random = new UnityEngine.Random();
        }
    }
}
