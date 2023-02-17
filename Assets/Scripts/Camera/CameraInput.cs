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
        InputAction _scrollPress;

        bool _isScrollPress;

        void Start()
        {
            var playerControls = new PlayerControls();
            _panMovement = playerControls.CameraActions.CameraMovement;
            _rotationMovement = playerControls.CameraActions.CameraRotation;
            _scroll = playerControls.CameraActions.Scroll;
            _scrollPress = playerControls.CameraActions.ScrollPress;
            playerControls.CameraActions.Enable();

            _scrollPress.performed += _ => _isScrollPress = true;
            _scrollPress.canceled +=  _ => _isScrollPress = false;
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
                ScrollPress = _isScrollPress,
            };
            
            _controller.SetInput(ref frameInput);
        }
    }

    public struct CameraFrameInput
    {
        public Vector2 PanMovement;
        public Vector2 RotationMovement;
        public float ScrollValue;
        public bool ScrollPress;
    }
}
