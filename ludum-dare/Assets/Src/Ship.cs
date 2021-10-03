using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace schw3de.ld49
{
    public class Ship : Singleton<Ship>
    {

        public Transform Thrusters1;
        public ParticleSystem Thruster1Particles;

        public Transform Thrusters2;
        public ParticleSystem Thruster2Particles;

        public ParticleSystem ExplosionParticles;

        public Vector2 Force = new Vector2(0,1);
        public float RotationSpeed = 0.5f;

        public decimal FuelCost = 0.001m;
        public decimal Fuel = 1000;

        public float VelocityImpact = 5f;

        public AudioClip ThrustAudio;
        public AudioClip ExplosionAudio;
        public AudioClip WinAudio;

        public TextMeshProUGUI OutComeTitle;
        public TextMeshProUGUI OutComeSubtitle;
        public GameObject Menu;

        public Button RetryButton;

        private Rigidbody2D _rigidbody2D;
        private bool _thrust;
        private bool _turnLeft;
        private bool _turnRight;
        private bool _noControl;

        private AudioSource _audioSource;
        private DateTime _delayBetweenThrusts;
        private DateTime _showMenuIn = DateTime.MinValue;

        private void Start()
        {
            Menu.SetActive(false);
            _audioSource = GetComponentInChildren<AudioSource>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            ExplosionParticles.Stop();
            Thruster1Particles.Play();
            Thruster2Particles.Play();


            RetryButton.Apply(OnRetry);
        }

        void Update()
        {
            if(_showMenuIn != DateTime.MinValue && _showMenuIn < DateTime.UtcNow)
            {
                Menu.SetActive(true);
            }

            // Debug.Log(_rigidbody2D.velocity);
            if(!_noControl && Input.GetKey(KeyCode.Space) && Fuel > 0)
            {
                if(!Thruster1Particles.isPlaying)
                {
                    Thruster1Particles.Play();
                    Thruster2Particles.Play();
                }

                if (_delayBetweenThrusts < DateTime.UtcNow)
                {
                    _audioSource.PlayOneShot(ThrustAudio);
                    _delayBetweenThrusts = DateTime.UtcNow.AddSeconds(0.5f);
                }


                _thrust = true;
                if(Fuel <= 0)
                {
                    Fuel = 0;
                }
                else
                {
                    Fuel -= FuelCost;
                }
            }
            else
            {
                _thrust = false;
                Thruster1Particles.Stop();
                Thruster2Particles.Stop();
            }

            if(Input.GetKey(KeyCode.A))
            {
                _turnRight = true;
                _turnLeft = false;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                _turnLeft = true;
                _turnRight = false;
            }
            else 
            {
                _turnLeft = false;
                _turnRight = false;
            }
        }

        private void FixedUpdate()
        {
            if(_thrust)
            {
                var force = Force * transform.up;
                _rigidbody2D.AddForce(force);
            }

            if(_turnLeft)
            {
                _rigidbody2D.AddTorque(RotationSpeed);
            }
            else if(_turnRight)
            {
                _rigidbody2D.AddTorque(-RotationSpeed);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                _noControl = true;
                Debug.Log($"Collision! {Math.Abs(collision.relativeVelocity.y)}");
                if (Math.Abs(collision.relativeVelocity.y) > VelocityImpact)
                {
                    // Game Over lost
                    ExplosionParticles.Play();
                    _audioSource.PlayOneShot(ExplosionAudio);
                    // Tried something dramatic._rigidbody2D.AddForce(new Vector2(3f, 300f), ForceMode2D.Impulse);
                    OutComeTitle.text = "Game Over!";
                    OutComeSubtitle.text = "You crashed the ship! Next time you will do better!";
                    _showMenuIn = DateTime.UtcNow.AddSeconds(1);
                }
                else
                {
                    // Win
                    _audioSource.PlayOneShot(WinAudio);
                    OutComeTitle.text = "Success!";
                    OutComeSubtitle.text = "Congratulation! You are a great pilot!";
                    _showMenuIn = DateTime.UtcNow.AddSeconds(0.5f);
                }
            }
        }

        private void OnRetry()
        {
            SceneManager.LoadScene("Level");
        }

    }
}
