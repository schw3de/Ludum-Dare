using UnityEngine;

namespace schw3de.MRnothing
{
    public class Parallax : MonoBehaviour
    {
        private float length;
        private float startpos;
        public Transform camera;
        public float parallaxEffect;

        private void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float tmp = camera.position.x * (1 - parallaxEffect);
            float dist = camera.position.x * parallaxEffect;

            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

            if (tmp > startpos + length)
            {
                startpos += length;
            }
            else if (tmp < startpos - length)
            {
                startpos -= length;
            }
        }
    }
}
