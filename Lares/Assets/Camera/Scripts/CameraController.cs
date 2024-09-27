using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares.Camera.Scripts
{
    //Determine if Camera is in correct position
    //Raycast between Camera and center, ignore the player somehow
    
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
            if (!Application.isPlaying) return;

            _playerControls = new PlayerControls();
        }

        private void Start()
        {
            transform.localPosition = _cameraCenter;
            _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
        }
        
        private void OnEnable()
        {
            if (!Application.isPlaying) return;

            _playerControls.Enable();
            _playerControls.BaseControls.MoveCamera.performed += OnMoveCameraPerformed;
        }

        private void OnDisable()
        {
            if (!Application.isPlaying) return;
            
            _playerControls.Disable();
            _playerControls.BaseControls.MoveCamera.performed -= OnMoveCameraPerformed;
        }
        
        private void LateUpdate()
        {
            if (!Application.isPlaying) return;
         
            transform.localPosition = _cameraCenter;
            _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
            
            _inputVector.y = Mathf.Clamp(_inputVector.y, -90f, 90);
            switch (_inputVector.x)
            {
                case >= 360f:
                    _inputVector.x -= 360f;
                    break;
                case <= -360f:
                    _inputVector.x += 360f;
                    break;
            }
            
            transform.localRotation = Quaternion.Euler(_inputVector.y, -_inputVector.x, 0);
        }

        private void OnMoveCameraPerformed(InputAction.CallbackContext context)
        {
            _inputVector += context.ReadValue<Vector2>();
        }
    }
}