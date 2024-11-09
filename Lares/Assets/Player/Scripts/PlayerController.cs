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

        private bool _isGrounded
        {
            set
            {
                _isGroundedInternal = value;
                if (!_isGroundedInternal)
                    _movementForce *= _inAirMovementModifier;
                else
                    _movementForce /= _inAirMovementModifier;
            }

            get { return _isGroundedInternal; }
        }

        private bool _isGroundedInternal;

        private int groundColliderCount;
        private bool _canJump => (_isGrounded && _jumpCount == 0) || (!_isGrounded && _jumpCount == 1);
        private int _jumpCount = 0;
        //private bool _isOnIncline => OnIncline();
        private bool _isMovementLocked;

        [Header("Raycast Settings")]
        [SerializeField] float raycastDistance;

        [Header("Move Settings")]
        [SerializeField] private float _movementForce;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _inAirMovementModifier;

        [Header("Incline Settings")]
        [SerializeField] private float _maxincline;
        [SerializeField] private AnimationCurve _inclineForce;
        [SerializeField] private Vector2 _nonInclineRange;

        [SerializeField] private Transform _cameraTransform;


        [Header("New incline detection vars -- change this later")]
        [SerializeField] private RaycastHit _inclineHitOut;
        [SerializeField] private float _maxRaycastHitDistance = 1.0f;
        [SerializeField] private float _maxInclineAngle = 60.0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _input.currentActionMap.FindAction("MovePlayer").performed += OnPlayerStartMoving;
            _input.currentActionMap.FindAction("MovePlayer").canceled += OnPlayerStopMoving;
            _input.currentActionMap.FindAction("Jump").performed += Jump;
            //    //_input.currentActionMap.FindAction("Interact").performed += Interact;
            _input.currentActionMap.FindAction("Sprint").performed += OnPlayerStartSprint;
            _input.currentActionMap.FindAction("Sprint").canceled += OnPlayerStopSprint;
        }

        private void OnPlayerStartSprint(InputAction.CallbackContext context) { _movementForce *= _sprintSpeedMultiplier; _maxSpeed *= _sprintSpeedMultiplier; }
        private void OnPlayerStopSprint(InputAction.CallbackContext context) { _movementForce /= _sprintSpeedMultiplier; _maxSpeed /= _sprintSpeedMultiplier; }

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
            GameObject rotPoint = transform.GetChild(0).gameObject;
            GameObject model = transform.GetChild(1).gameObject;

            while (_moveDirection != Vector3.zero)
            {
                while (_isMovementLocked)
                {
                    yield return new WaitForFixedUpdate();
                }

                rotPoint.transform.rotation = Quaternion.Slerp(rotPoint.transform.rotation,
                    Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0), _rotationSpeed);

                if (OnIncline())
                {
                    _rigidbody.AddForce(GetInclineForwardDirection() * 20, ForceMode.Force);
                }

                _rigidbody.AddForce(rotPoint.transform.forward * (_moveDirection.z * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                _rigidbody.AddForce(rotPoint.transform.right * (_moveDirection.x * _movementForce * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);

                model.transform.rotation = Quaternion.Slerp(model.transform.rotation,
                    Quaternion.LookRotation(new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z)),
                    _rotationSpeed);

                _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, _maxSpeed);


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
                groundColliderCount++;
                _jumpCount = 0;
                _isGrounded = true;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
            {
                _inclineHeight = hit.normal;
            }
        }

        private void OnCollisionExit(Collision collision)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {
                groundColliderCount--;
                if (groundColliderCount <= 0)
                    _isGrounded = false;
            }
        }

        private void LateUpdate()
        {
            transform.GetChild(1).gameObject.transform.localPosition = Vector3.zero;
            _rigidbody.useGravity = !OnIncline();
        }

        private void OnDrawGizmos()
        {
            //draw the raycast incline 
            Debug.DrawLine(transform.position, transform.position + Vector3.down * _maxRaycastHitDistance);
        }

        #endregion
    }
}