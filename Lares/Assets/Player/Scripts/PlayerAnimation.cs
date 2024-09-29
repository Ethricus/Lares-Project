using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Lares
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayerInput _input;
        Animator _animator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _input = GetComponentInParent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _input.currentActionMap.FindAction("MovePlayer").performed += MoveStart;
            _input.currentActionMap.FindAction("MovePlayer").canceled += MoveEnd;
        }

        void MoveStart(InputAction.CallbackContext context)
        {
            _animator.SetBool("Moving", true);

        }

        void MoveEnd(InputAction.CallbackContext context)
        {
            _animator.SetBool("Moving", false);
        }
    }
}
