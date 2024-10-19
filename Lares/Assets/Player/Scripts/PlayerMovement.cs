using Lares.BattleSystem;
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
        private bool _lockMovement;
        
        [SerializeField] private float _movementForce;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _sprintSpeedMultiplier;

        [SerializeField] private Transform _cameraTransform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _input.currentActionMap.FindAction("MovePlayer").performed += MoveStart;
            _input.currentActionMap.FindAction("MovePlayer").canceled += MoveEnd;
            _input.currentActionMap.FindAction("Jump").performed += Jump;
            _input.currentActionMap.FindAction("Interact").performed += Interact;
            _input.currentActionMap.FindAction("Sprint").performed += SprintStart;
            _input.currentActionMap.FindAction("Sprint").canceled += SprintEnd;
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
                while (_lockMovement)
                {
                    yield return new WaitForFixedUpdate();
                }
                rotPoint.transform.rotation = Quaternion.Slerp(rotPoint.transform.rotation,
                    Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0), _rotationSpeed);    

                _rigidbody.AddForce(rotPoint.transform.forward * (_moveDirection.z * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                _rigidbody.AddForce(rotPoint.transform.right * (_moveDirection.x * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);

                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, 
                    Quaternion.LookRotation(new Vector3( _rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z)), 
                    _rotationSpeed);

                yield return new WaitForFixedUpdate();
            }

            _moveCoroutine = null;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_lockMovement) { return; }
            if (_jumpNo < 2)
            {
                _rigidbody.AddForce(new Vector3(0f, _jumpForce - _rigidbody.linearVelocity.y, 0f), ForceMode.Impulse);
                _jumpNo++;
            }
        }

        private void SprintStart(InputAction.CallbackContext context)
        {
            _movementForce += _sprintSpeedMultiplier; 
        }

        private void SprintEnd(InputAction.CallbackContext context)
        {
            _movementForce -= _sprintSpeedMultiplier;
        }

        #endregion

        #region Player Battle Actions
        private void Evade(InputAction.CallbackContext context)
        {
        }

        //these need to get the data from what attack is assigned to this control, it will take the attack data struct and
        //use it to perform the attack 
        private void LightAttack(InputAction.CallbackContext context)
        {
            
        }
        private void HeavyAttack(InputAction.CallbackContext context)
        {
        }
        private void MagicAttack1(InputAction.CallbackContext context)
        {
        }
        private void MagicAttack2(InputAction.CallbackContext context)
        {
        }

        private void PerformAttack(AttackData attackData)
        {

        }
        #endregion

        private void Interact(InputAction.CallbackContext context)
        {
        }
        
        
        private void OnCollisionEnter(Collision collision)
        {
            _jumpNo = 0;
        }
    }
}
