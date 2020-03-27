using Ditzelgames;
using System;
using UnityEngine;

namespace schw3de.MRnothing
{
    public class Player : MonoBehaviour
    {

        public float Speed = 20000;
        public float JumpPower = 2;
        public float MaxRun = 0.5f;
        public Vector2 MaxVelocity = new Vector2(2, 0);
        public GameObject[] BodyParts;
        public Transform PlayerBar;
        public AudioClip[] Footsteps;
        public AudioClip JumpSound;

        private CharacterController2D characterController2D;
        private float horizontalMovement;
        private bool jump;
        private DisappearUpdater disappearUpdater;
        public Material material;
        private DateTime disappearWhen;
        private double secondsToAppear;
        private DisappearManager disappearManger;
        private Animator animator;
        private bool IsDead = false;
        private Rigidbody2D rigidbody2D;
        private Vector3 velocity;
        private Collider2D collider;
        private bool blockControls;
        private AudioSource audioSource;



        public void OnLand()
        {
            Debug.Log("OnLand");
            this.animator.SetBool("IsJumping", false);
        }

        public void BlockControls()
        {
            this.animator.SetBool("IsJumping", false);
            this.animator.SetFloat("Speed", 0);
            Destroy(this.rigidbody2D);
            this.blockControls = true;
        }

        public void Die()
        {
            if(this.IsDead || this.blockControls)
            {
                return;
            }

            this.IsDead = true;
            this.disappearUpdater.AppearForEver();
            this.characterController2D.enabled = false;
            this.animator.enabled = false;
            this.ApplyRidgedBodiesOnBodyParts();
            //this.rigidbody2D.mass = 0;
            //this.rigidbody2D.gravityScale = 0;
            //this.rigidbody2D.drag = 300;
            Destroy(this.rigidbody2D);
            this.collider.enabled = false;
        }

        public void PlayFootstep()
        {
            AudioClip footstepSound = this.Footsteps[UnityEngine.Random.Range(0, this.Footsteps.Length)];
            this.audioSource.PlayOneShot(footstepSound);
        }

        public void PlayJump()
        {
            this.audioSource.PlayOneShot(this.JumpSound);
        }

        private void ApplyRidgedBodiesOnBodyParts()
        {
            foreach(GameObject bodyPart in this.BodyParts)
            {
                bodyPart.AddComponent<PolygonCollider2D>();
                bodyPart.AddComponent<Rigidbody2D>();
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            this.rigidbody2D = GetComponent<Rigidbody2D>();
            this.characterController2D = GetComponent<CharacterController2D>();
            this.animator = this.GetComponent<Animator>();
            this.collider = this.GetComponent<Collider2D>();
            //Debug.Log(this.material.HasProperty("Disappear_Value"));
            //this.material.SetFloat("Vector1_175434C0", 1.0f);
            this.disappearUpdater = new DisappearUpdater(this.material, this.PlayerBar);
            this.disappearManger = new DisappearManager(this.Disappear);
            Game.Instance.RegisteredCurrentPlayer = this;
            this.audioSource = this.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            this.disappearUpdater.Update();

            var ticks = (this.disappearWhen - DateTime.UtcNow).Ticks;
            // 8this.PlayerBar.localScale = new Vector3(DateTime)

            if(this.IsDead || this.blockControls)
            {
                return;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                Game.Instance.ReloadCurrentLevel();
            }

//#if debug
            //if(Input.GetKeyDown(KeyCode.H))
            //{
            //    this.disappearUpdater.Disappear();
            //}

            //if(Input.GetKeyDown(KeyCode.J))
            //{
            //    this.disappearUpdater.Appear();
            //}
//#endif

            this.horizontalMovement = Input.GetAxis("Horizontal") * this.Speed;
            this.animator.SetFloat("Speed", Mathf.Abs(this.horizontalMovement));
            this.jump = Input.GetButtonDown("Jump");
            if(jump)
            {
                Debug.Log("OnJump");
                this.animator.SetBool("IsJumping", true);
            }
        }

        private void FixedUpdate()
        {
            if(this.IsDead)
            {
                return;
            }

            if (this.secondsToAppear != 0)
            {
                double scaleValue = (DateTime.UtcNow - this.disappearWhen).TotalSeconds / this.secondsToAppear;

                if (scaleValue < 0)
                {
                    this.PlayerBar.localScale = new Vector3((float)Math.Abs(scaleValue), 1, 1);
                }
            }

            if(this.blockControls)
            {
                return;
            }

            this.characterController2D.Move(this.horizontalMovement * Time.deltaTime, false, this.jump);
            this.jump = false;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collector collector = collision.gameObject.GetComponent<Collector>();
            if (collector != null)
            {
                this.disappearUpdater.Appear();
                this.disappearWhen =  DateTime.UtcNow.AddSeconds(collector.SecondsToAppear);
                this.secondsToAppear = (this.disappearWhen - DateTime.UtcNow).TotalSeconds;
                this.disappearManger.SetTimeToDisappear(secondsToAppear);
                Debug.Log($"In {secondsToAppear} seconds to disappear");
            }
        }

        private void Disappear()
        {
            Debug.Log("Disappear now plz");
            this.disappearUpdater.Disappear();
            //throw new NotImplementedException();
        }
    }

}
