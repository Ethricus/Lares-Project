using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares
{

    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _input;
        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;
        private Coroutine _moveCoroutine;

        private Vector3 _inclineHeight;

        public bool IsGrounded
        {
            private set
            {
                _isGrounded = value;
                if (!_isGrounded)
                    _movementForce *= _inAirMovementModifier;
                else
                    _movementForce /= _inAirMovementModifier;
            }

            get { return _isGrounded; }
        }

        private bool _isGrounded;

        private int _groundColliderCount;
        private bool _canJump => (_isGrounded && _jumpCount == 0) || (!_isGrounded && _jumpCount == 1);
        private int _jumpCount = 0;
        private bool _isMovementLocked;
        private bool _isSprinting;

        [Header("Raycast Settings")]
        [SerializeField] private float _raycastDistance;

        [Header("Move Settings")]
        [SerializeField] private float _movementForce;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _inAirMovementModifier;

        [Header("References")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private GameObject _rotationPoint;
        [SerializeField] private GameObject _playerModel;

        [Header("New incline detection vars -- change this later")]
        [SerializeField] private RaycastHit _inclineHitOut;
        [SerializeField] private float _maxRaycastHitDistance = 1.0f;
        [SerializeField] private float _maxInclineAngle = 60.0f;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _input.currentActionMap.FindAction("MovePlayer").performed += OnPlayerStartMoving;
            _input.currentActionMap.FindAction("MovePlayer").canceled += OnPlayerStopMoving;
            _input.currentActionMap.FindAction("Jump").performed += Jump;
            _input.currentActionMap.FindAction("Sprint").performed += OnPlayerSprint;
            _input.currentActionMap.FindAction("Sprint").canceled += OnPlayerSprint;
        }

        private void OnPlayerSprint(InputAction.CallbackContext context) { _isSprinting = !_isSprinting; }

        private void OnPlayerStartMoving(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();

            _moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
            _moveCoroutine ??= StartCoroutine(Movement());
        }

        private void OnPlayerStopMoving(InputAction.CallbackContext context)
        {
            _moveDirection = Vector3.zero;
        }

        private IEnumerator Movement()
        {
            

            while (_moveDirection != Vector3.zero)
            {
                while (_isMovementLocked)
                {
                    yield return new WaitForFixedUpdate();
                }

                _rotationPoint.transform.rotation = Quaternion.Slerp(_rotationPoint.transform.rotation,
                    Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0), _rotationSpeed);

                if (OnIncline())
                    _rigidbody.AddForce(GetInclineForwardDirection() * 20, ForceMode.Force);
                
                float currentMovementForce = _movementForce;
                float currentMaxSpeed = _maxSpeed;

                if (_isSprinting) 
                    currentMovementForce *= _sprintSpeedMultiplier; currentMaxSpeed *= _sprintSpeedMultiplier; 

                _rigidbody.AddForce(_rotationPoint.transform.forward * (_moveDirection.z * currentMovementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                _rigidbody.AddForce(_rotationPoint.transform.right * (_moveDirection.x * currentMovementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);

                _playerModel.transform.rotation = Quaternion.Slerp(_playerModel.transform.rotation,
                    Quaternion.LookRotation(new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z)),
                    _rotationSpeed);

                _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, currentMaxSpeed);


                _rigidbody.useGravity = !OnIncline();
                yield return new WaitForFixedUpdate();
            }
            _moveCoroutine = null;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!_canJump) { return; }
            _rigidbody.AddForce(new Vector3(0f, _jumpForce - _rigidbody.linearVelocity.y, 0f), ForceMode.Impulse);
            _jumpCount++;

        }

        private bool OnIncline()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _inclineHitOut, _maxRaycastHitDistance))
            {
                float angle = Vector3.Angle(Vector3.up, _inclineHitOut.normal);
                bool angleAboveInclineRange = angle < _maxInclineAngle && angle != 0.0f;
                return angleAboveInclineRange;
            }
            return false;
        }

        private Vector3 GetInclineForwardDirection()
        {
            return Vector3.ProjectOnPlane(_rigidbody.linearVelocity, _inclineHitOut.normal);
        }

        #region Collisions Checks
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _groundColliderCount++;
                _jumpCount = 0;
                _isGrounded = true;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, _raycastDistance))
            {
                _inclineHeight = hit.normal;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _groundColliderCount--;
                if (_groundColliderCount <= 0)
                    _isGrounded = false;
            }
        }

        #endregion
        private void FixedUpdate()
        { 
            _playerModel.transform.localPosition = Vector3.zero;
            _rigidbody.useGravity = !OnIncline();
        }

        private void OnDrawGizmos()
        {
            //draw the raycast incline 
            Debug.DrawLine(transform.position, transform.position + Vector3.down * _maxRaycastHitDistance);
        }
    }
}