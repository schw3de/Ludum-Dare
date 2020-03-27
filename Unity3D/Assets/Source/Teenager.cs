using UnityEngine;
using System.Collections;

namespace Assets.Source
{
    public class Teenager : MonoBehaviour
    {
        public Vector3 RaycastPosition;
        public int NumberOfRaycastPerCircle;
        public float DistanceToOuterRayCastCircle;
        public float DistanceToInnerRayCastCircle;
        public float HowFarCanTeenagerLook;
        public GameManager GameManager;
        public int RotateSpeed;
        public int RotateScaredSpeed;
        public int WalkingSpeed;
        public Transform PositionA;
        public Transform PositionB;

        private Transform targetPosition;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerHasBeenSeen(NumberOfRaycastPerCircle, DistanceToOuterRayCastCircle) || PlayerHasBeenSeen(NumberOfRaycastPerCircle, DistanceToInnerRayCastCircle))
            {
                // Game Over
                GameManager.SetGameState(GameState.GameOver);
            }

            if(this.GameManager.EveryoneScared)
            {
                // look at scared place
                if(!IsRotatedTo(this.GameManager.ScaredPosition))
                {
                    RotateTo(this.GameManager.ScaredPosition, RotateScaredSpeed);
                }
            }
            else
            {
                this.NormalRoute();
            }
        }

        private bool PlayerHasBeenSeen(int numberOfRaycasts, float distanceToCircle)
        {
            RaycastHit hitInfo;

            for (int i = 0; i < numberOfRaycasts; i++)
            {
                float fraction = (float)(i * 1.0) / numberOfRaycasts;

                float angle = fraction * Mathf.PI * 2;

                float x = Mathf.Sin(angle) * distanceToCircle;
                float y = Mathf.Cos(angle) * distanceToCircle;

                // watchingRay = new Ray(transform.position, Vectr
                //Vector3 forward = transform.forward + new Vector3() 

                Debug.DrawRay(transform.position, (new Vector3(x, y, 0) + transform.forward) * HowFarCanTeenagerLook, Color.red, 1);

                if (Physics.Raycast(transform.position, (new Vector3(x, y, 0) + transform.forward) * HowFarCanTeenagerLook, out hitInfo))
                {
                    if(hitInfo.collider.gameObject.name.Equals("Monster"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void MoveTo(Vector3 target)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * WalkingSpeed);
        }

        public void RotateTo(Vector3 target, int speed)
        {
            var targetDirection = target - transform.position;

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            var newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public bool IsMovedTo(Vector3 target)
        {
            return Vector3.Distance(target, this.transform.position) < 1; 
        }

        public bool IsRotatedTo(Vector3 target)
        {
            var targetDirection = target - transform.position;

            int angle = (int)Vector3.Angle(this.transform.forward, targetDirection);

            return angle == 0 || angle == 360;
        }

        public void NormalRoute()
        {
            if (targetPosition == null)
            {
                targetPosition = PositionA;
            }

            if(!IsRotatedTo(targetPosition.position))
            {
                RotateTo(targetPosition.position, RotateSpeed);
            }
            else if(!IsMovedTo(targetPosition.position))
            {
                MoveTo(targetPosition.position);
            }
            else
            {
                if(targetPosition == PositionA)
                {
                    targetPosition = PositionB;
                }
                else
                {
                    targetPosition = PositionA;
                }
            }
        }
    }
}
