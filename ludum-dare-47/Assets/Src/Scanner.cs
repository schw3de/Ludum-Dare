using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld47
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField]
        private  Transform _scannerPosition;

        [SerializeField]
        private GameObject _laserfield;

        private Timer _scanTimmer;
        private RaycastHit2D _lastHit;


        private void Awake()
        {
            _scanTimmer = new Timer(TimeSpan.FromSeconds(Features.Instance.ScannerSpeedSeconds));
        }

        public void Update()
        {
            if(!_scanTimmer.IsFinished())
            {
                return;
            }

            _laserfield.SetActive(true);

            Vector2 pos = _scannerPosition.position;
            var hit = Physics2D.Raycast(pos, Vector2.up, 2.0f, LayerMask.GetMask(LayerMasks.Articles));
            if(hit.collider != null && hit.collider.gameObject.tag == Tags.Barcode && _lastHit.point != hit.point)
            {
                _laserfield.SetActive(false);
                _scanTimmer.Start();
                var articleGo = hit.collider.gameObject;
                Game.Instance.ArticleScanned(articleGo.GetComponentInParent<Article>());
                _lastHit = hit;
                Debug.Log($"Got scanned! {articleGo.name}");
            }

            Debug.DrawRay(_scannerPosition.position, Vector2.up * 2.0f, Color.green);
        }

        //void OnDrawGizmosSelected()
        //{
        //    if (_scannerPosition != null)
        //    {
        //        // Draws a blue line from this transform to the target
        //        Gizmos.color = Color.blue;
        //        _scannerPosition.position + new Vector2(0, 2.0f);
        //        Gizmos.DrawLine(transform.position, );
        //    }
        //}
    }
}
