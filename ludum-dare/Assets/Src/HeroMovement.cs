using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace schw3de.ld49
{
    public class HeroMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D _rigigbody;

        private Vector2 _movement;

        private Animator _animator;

        private void Start()
        {
            _rigigbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _animator.Play(HeroAnimations.Hero_Idle.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

        }

        private void FixedUpdate()
        {
            _rigigbody.MovePosition(_rigigbody.position + moveSpeed * _movement * Time.fixedDeltaTime);

            if(_movement.magnitude > 0.01f)
            {
                _animator.Play(HeroAnimations.Hero_Walk_Forward.ToString());
            }
            else
            {
                _animator.Play(HeroAnimations.Hero_Idle.ToString());
            }
        }

        private enum HeroAnimations
        {
            Hero_Idle,
            Hero_Walk_Forward
        }
    }
}