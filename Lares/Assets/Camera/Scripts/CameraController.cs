using System;
using System.Diagnostics;
using UnityEngine;

namespace Lares.Camera.Scripts
{
    [ExecuteAlways, ]
    public class CameraController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private UnityEngine.Camera _camera;

        [Header("Camera Settings")] 
        [SerializeField] private Vector3 _cameraCenter;
        [SerializeField] private Vector2 _cameraOffset;
        [SerializeField] private float _distanceFromCenter;
        
        private void Start()
        {
            transform.localPosition = _cameraCenter;
            _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
        }

        private void Update()
        {
            EditorUpdate();
            
            //Determine if Camera is in correct position
            //Raycast between Camera and center, ignore the player somehow
        }

        [Conditional("UNITY_EDITOR")]
        private void EditorUpdate()
        {
            transform.localPosition = _cameraCenter;
            _camera.transform.localPosition = new Vector3(_cameraOffset.x, _cameraOffset.y, -_distanceFromCenter);
        }
    }
}