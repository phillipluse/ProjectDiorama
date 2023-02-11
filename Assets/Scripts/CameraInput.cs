using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDiorama
{
    public class CameraInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CameraController _controller;
        
        InputAction _panMovement;
        InputAction _rotationMovement;
        InputAction _scroll;

        void Start()
        {
            var playerControls = new PlayerControls();
            _panMovement = playerControls.CameraActions.CameraMovement;
            _rotationMovement = playerControls.CameraActions.CameraRotation;
            _scroll = playerControls.CameraActions.Scroll;
            playerControls.CameraActions.Enable();
        }

        void LateUpdate()
        {
            HandleInput();
        }

        void HandleInput()
        {
            var frameInput = new CameraFrameInput
            {
                PanMovement = _panMovement.ReadValue<Vector2>(),
                RotationMovement = _rotationMovement.ReadValue<Vector2>(),
                ScrollValue = _scroll.ReadValue<float>(),
            };
            
            _controller.SetInput(ref frameInput);
        }
    }

    public struct CameraFrameInput
    {
        public Vector2 PanMovement;
        public Vector2 RotationMovement;
        public float ScrollValue;
    }
}
