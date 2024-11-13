using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares.Player.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        private PlayerInput _input;
        private Animator _animator;
        private bool _isSprinting = false;
        private Rigidbody _rigidbody;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _input = GetComponentInParent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponentInParent<Rigidbody>();
            _input.currentActionMap.FindAction("Sprint").performed += SprintStart;
            _input.currentActionMap.FindAction("Sprint").canceled += SprintEnd;

        }

        private void FixedUpdate()
        {
            if (Mathf.Abs( Vector3.Magnitude(_rigidbody.linearVelocity)) > 0.5)
            {
                _animator.SetBool("Moving", true);
            }
            else
            {
                _animator.SetBool("Moving", false);
            }
        }

        void MoveStart(InputAction.CallbackContext context)
        {
            _animator.SetBool("Moving", true);

        }

        void MoveEnd(InputAction.CallbackContext context)
        {
            _animator.SetBool("Moving", false);
        }

        void SprintStart(InputAction.CallbackContext context) 
        {
            _animator.SetBool("Sprinting", true);
        }

        void SprintEnd(InputAction.CallbackContext context)
        {
            _animator.SetBool("Sprinting", false);
        }

        //void OnAnimatorMove()
        //{
        //    if (_animator)
        //    {
        //        Vector3 newPosition = transform.position;
        //        newPosition.x += _animator.GetFloat("Moving") * Time.deltaTime;
        //        transform.position = newPosition;
        //    }
        //}

    }
}
