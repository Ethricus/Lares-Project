using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares.Camera.Scripts
{
    //Determine if Camera is in correct position
    //Raycast between Camera and center, ignore the player somehow
    
    [RequireComponent(typeof(PlayerControls))]
    public class CameraController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private UnityEngine.Camera _camera;

        [Header("Camera Settings")] 
        [SerializeField] private Vector3 _cameraCenter;
        [SerializeField] private Vector2 _cameraOffset;
        [SerializeField] private float _distanceFromCenter;

        private PlayerControls _playerControls;

        private Vector2 _inputVector;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void Start()
        {
            transform.localPosition = _cameraCenter;
            transform.localRotation = Quaternion.identity;
            _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
            _playerControls.BaseControls.MoveCamera.performed += OnMoveCameraPerformed;
            _playerControls.BaseControls.MoveCamera.canceled += OnMoveCameraCanceled;
        }

        private void OnDisable()
        {
            _playerControls.BaseControls.MoveCamera.performed -= OnMoveCameraPerformed;
            _playerControls.BaseControls.MoveCamera.canceled -= OnMoveCameraCanceled;

            _playerControls.Disable();
        }
        
        private void LateUpdate()
        {
            if (transform.localPosition != _cameraCenter)
            {
                transform.localPosition = _cameraCenter;
                _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
            }
            
            if (_inputVector == Vector2.zero)
                return;

            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.x += _inputVector.y;
            currentRotation.y -= _inputVector.x;
            
            currentRotation.x = currentRotation.x switch
            {
                >= 180f and < 270f => 270f,
                > 90f and < 180f => 90f,
                _ => currentRotation.x
            };

            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        
        private void OnMoveCameraPerformed(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
        }

        private void OnMoveCameraCanceled(InputAction.CallbackContext context)
        {
            _inputVector = Vector2.zero;
        }
    }
}