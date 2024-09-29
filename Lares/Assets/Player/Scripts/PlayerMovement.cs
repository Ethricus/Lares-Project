using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares
{
    public class PlayerMovement : MonoBehaviour
    {
        PlayerInput _input;
        Vector3 moveDirection;
        Rigidbody _rigidbody;
        Coroutine _moveCoroutine;
        int _jumpNo = 0;
        [SerializeField] float movementForce;
        [SerializeField] float maxSpeed;
        [SerializeField] float jumpForce;
        [SerializeField] float rotationSpeed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
           
            _input.currentActionMap.FindAction("MovePlayer").performed += MoveStart;
            _input.currentActionMap.FindAction("MovePlayer").canceled += MoveEnd;
            _input.currentActionMap.FindAction("Jump").performed += Jump;
            _input.currentActionMap.FindAction("Interact").performed += Interact;
            _input.currentActionMap.FindAction("Evade").performed += Evade;
        }
        
        #region move player
        void MoveStart(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();
            moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
            if (_moveCoroutine == null) { StartCoroutine(Move()); }
            
        }

        void MoveEnd(InputAction.CallbackContext context)
        {
            moveDirection = Vector3.zero;
        }

        IEnumerator Move()
        {
            GameObject cameraLook = transform.GetChild(0).gameObject;
            GameObject model = transform.GetChild(1).gameObject;

            while (moveDirection != Vector3.zero)
            {
                _rigidbody.MovePosition(transform.position + (moveDirection * movementForce) * Time.fixedDeltaTime);
                Quaternion targetRot = cameraLook.transform.rotation * Quaternion.LookRotation(moveDirection, Vector3.up);
                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRot, rotationSpeed);

                yield return new WaitForFixedUpdate();
            }
            //_rigidbody.maxAngularVelocity = _rigidbody.maxAngularVelocity / 1.5f;
        }

        #endregion

        #region Jump

        void Jump(InputAction.CallbackContext context) 
        { 
            if (_jumpNo < 2)
            {
                _rigidbody.AddForce(new Vector3(0f, jumpForce - _rigidbody.linearVelocity.y, 0f), ForceMode.Impulse);
                _jumpNo++;
            }
        }
        #endregion

        #region Interact
        void Interact(InputAction.CallbackContext context)
        {

        }
        #endregion

        #region Evade
        void Evade(InputAction.CallbackContext context) { }
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            _jumpNo = 0;
        }
    }
}
