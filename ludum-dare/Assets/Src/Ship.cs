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

        public float ThrustForce = 100;
        public float RotationSpeed = 0.5f;

        public decimal FuelCost = 0.05m;
        public decimal Fuel = 100;

        public float VelocityImpact = 5f;

        public AudioClip ThrustAudio;
        public AudioClip ExplosionAudio;
        public AudioClip WinAudio;

        public TextMeshProUGUI OutComeTitle;
        public TextMeshProUGUI OutComeSubtitle;
        public TextMeshProUGUI FuelRecord;
        public GameObject Menu;

        public Button RetryButton;

        private Rigidbody2D _rigidbody2D;
        private bool _thrust;
        private bool _turnLeft;
        private bool _turnRight;
        private bool _noControl;
        private decimal _fuelRecord = 100;

        private AudioSource _audioSource;
        private DateTime _delayBetweenThrusts;
        private DateTime _showMenuIn = DateTime.MinValue;

        private Vector3 _lastPosition;
        private bool _isStandingStill;
        private DateTime _checkStandingStill;

        private void Start()
        {
            _fuelRecord = GetFuelRound(Convert.ToDecimal(PlayerPrefs.GetString("Fuelrecord", "0")));
            FuelRecord.text = FuelToString(_fuelRecord);

            Menu.SetActive(false);
            _audioSource = GetComponentInChildren<AudioSource>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            ExplosionParticles.Stop();
            Thruster1Particles.Play();
            Thruster2Particles.Play();

            RetryButton.Apply(OnRetry);

            var euler = transform.eulerAngles;
            euler.z = UnityEngine.Random.Range(-90.0f, 90.0f);
            transform.eulerAngles = euler;

            transform.position = transform.position.ChangeX(UnityEngine.Random.Range(-18.0f, 18.0f));
        }

        void Update()
        {
            if(_showMenuIn != DateTime.MinValue && _showMenuIn < DateTime.UtcNow)
            {
                Menu.SetActive(true);
            }

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
                    _delayBetweenThrusts = DateTime.UtcNow.AddSeconds(1f);
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

            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _turnRight = true;
                _turnLeft = false;
            }
            else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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
                _rigidbody2D.AddRelativeForce(transform.up * ThrustForce);
            }

            if(_turnLeft)
            {
                _rigidbody2D.AddTorque(RotationSpeed);
            }
            else if(_turnRight)
            {
                _rigidbody2D.AddTorque(-RotationSpeed);
            }

            if(_checkStandingStill < DateTime.UtcNow)
            {
                _isStandingStill = _lastPosition == transform.position;
                _lastPosition = transform.position;
                _checkStandingStill = DateTime.UtcNow.AddSeconds(0.5f);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if(_noControl || !_isStandingStill || collision.gameObject.tag != "Ground")
            {
                return;
            }

            Debug.Log($"Rotation Euler: {transform.rotation.eulerAngles.z}");
            Debug.Log($"Rotation : {transform.rotation.z}");

            if (Math.Abs(transform.rotation.eulerAngles.z) > 1 && Math.Abs(transform.rotation.eulerAngles.z) < 359)
            {
                Debug.Log("Game Over because of euler angles!");
                GameOver();
                return;
            }

            _noControl = true;
            _audioSource.PlayOneShot(WinAudio);
            if (Math.Round(_fuelRecord) < Math.Round(Fuel))
            {
                OutComeTitle.text = "New Fuel Record!";
                OutComeSubtitle.text = $"Congratulation!\nNew Fuelrecord: {FuelToString()}!";
                PlayerPrefs.SetString("Fuelrecord", GetFuelRound().ToString());
            }
            else
            {
                OutComeTitle.text = "Success!";
                OutComeSubtitle.text = $"Congratulation!\nFuel used:{FuelToString()}\nTry to beat your Fuelrecord!";
            }

            _showMenuIn = DateTime.UtcNow.AddSeconds(0.5f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_noControl)
            {
                return;
            }

            if(collision.gameObject.tag == "Gras")
            {
                GameOver();
            }

            if(collision.gameObject.tag == "Ground")
            {
                Debug.Log($"Collision! {Math.Abs(collision.relativeVelocity.y)}");
                if (Math.Abs(collision.relativeVelocity.y) > VelocityImpact)
                {
                    GameOver();
                }
            }
        }

        private void OnRetry()
        {
            SceneManager.LoadScene("Level");
        }

        private void GameOver()
        {
            _noControl = true;
            // Game Over lost
            ExplosionParticles.Play();
            _audioSource.PlayOneShot(ExplosionAudio);
            // Tried something dramatic._rigidbody2D.AddForce(new Vector2(3f, 300f), ForceMode2D.Impulse);
            OutComeTitle.text = "Game Over!";
            OutComeSubtitle.text = "You crashed the ship!\nNext time you will do better!";
            _showMenuIn = DateTime.UtcNow.AddSeconds(2);
        }

        private string FuelToString()
            => FuelToString(GetFuelRound());

        private static string FuelToString(decimal fuel)
            => $"{fuel}%";

        private decimal GetFuelRound(decimal fuel)
            => Math.Round(fuel);

        private decimal GetFuelRound()
            => GetFuelRound(Fuel);
    }
}
