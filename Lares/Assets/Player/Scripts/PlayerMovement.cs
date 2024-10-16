using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _input;
        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;
        private Coroutine _moveCoroutine;
        private int _jumpNo;
        
        [SerializeField] private float _movementForce;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Transform _cameraTransform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _input.currentActionMap.FindAction("MovePlayer").performed += MoveStart;
            _input.currentActionMap.FindAction("MovePlayer").canceled += MoveEnd;
            _input.currentActionMap.FindAction("Jump").performed += Jump;
            _input.currentActionMap.FindAction("Interact").performed += Interact;
            _input.currentActionMap.FindAction("Evade").performed += Evade;
        }

        #region Player Movement

        private void MoveStart(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();
            
            _moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
            _moveCoroutine ??= StartCoroutine(Move());
        }

        private void MoveEnd(InputAction.CallbackContext context)
        {
            _moveDirection = Vector3.zero;
        }

        private IEnumerator Move()
        {
            GameObject rotPoint = transform.GetChild(0).gameObject;
            GameObject model = transform.GetChild(1).gameObject;

            while (_moveDirection != Vector3.zero)
            {
                rotPoint.transform.rotation = Quaternion.Slerp(rotPoint.transform.rotation,
                    Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0), _rotationSpeed);    

                _rigidbody.AddForce(rotPoint.transform.forward * (_moveDirection.z * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                _rigidbody.AddForce(rotPoint.transform.right * (_moveDirection.x * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);

                model.transform.rotation = Quaternion.Slerp(model.transform.rotation,
                    Quaternion.LookRotation(_rigidbody.linearVelocity), _rotationSpeed);

                yield return new WaitForFixedUpdate();
            }
            //_rigidbody.maxAngularVelocity = _rigidbody.maxAngularVelocity / 1.5f;

            _moveCoroutine = null;
        }
    
        #endregion
        
        private void Jump(InputAction.CallbackContext context) 
        { 
            if (_jumpNo < 2)
            {
                _rigidbody.AddForce(new Vector3(0f, _jumpForce - _rigidbody.linearVelocity.y, 0f), ForceMode.Impulse);
                _jumpNo++;
            }
        }
        
        private void Interact(InputAction.CallbackContext context)
        {
        }
        
        private void Evade(InputAction.CallbackContext context)
        {
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            _jumpNo = 0;
        }
    }
}
