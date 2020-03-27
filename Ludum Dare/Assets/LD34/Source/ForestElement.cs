using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schw3de.Base.Source;
using schw3de.LD34.Source.Events;
using UnityEngine;

namespace schw3de.LD34.Source
{
    public class ForestElement : MonoBehaviour
    {
        public event EventHandler<ForestElementDestroyedEventArgs> OnForestElementDestroyed;

        private bool isDestroying;

        private Animator animator;

        private void Update()
        {
            if (this.transform.localScale.x < 1)
            {
                //this.transform.localScale.
            }
        }

        public void StartDestroy()
        {
            if (this.isDestroying)
                return;

            this.isDestroying = true;

            var args = new ForestElementDestroyedEventArgs();
            args.ForestElement = this;

            if (this.OnForestElementDestroyed != null)
                this.OnForestElementDestroyed(this, args);

            this.animator.SetTrigger("IsDestroy");
        }

        public void FinishDestroy()
        {
            this.animator.ResetTrigger("IsDestroy");
            GameObject.Destroy(gameObject);
        }

        private void Awake()
        {
            this.animator = this.GetComponent<Animator>();
        }
    }
}
