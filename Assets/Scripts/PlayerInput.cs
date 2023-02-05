using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDiorama
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Player _player;
        
        PlayerControls _playerControls;
        InputAction _select;
        InputAction _oneButton;
        InputAction _twoButton;
        InputAction _threeButton;
        InputAction _fourButton;
        InputAction _rotate;
        InputAction _escape;
        InputAction _delete;
        
        void Awake()
        {
            _playerControls = new PlayerControls();
            _select = _playerControls.PlayerActions.Select;
            _oneButton = _playerControls.PlayerActions.OneButton;
            _twoButton = _playerControls.PlayerActions.TwoButton;
            _threeButton = _playerControls.PlayerActions.ThreeButton;
            _fourButton = _playerControls.PlayerActions.FourButton;
            _rotate = _playerControls.PlayerActions.Rotate;
            _escape = _playerControls.PlayerActions.Escape;
            _delete = _playerControls.PlayerActions.Delete;
            _playerControls.Enable();
        }
        
        void Update()
        {
            HandleInput();
        }

        void HandleInput()
        {
            var frameInput = new FrameInput
            {
                IsSelectPressedThisFrame = _select.WasPressedThisFrame(),
                IsOneButtonPressedThisFrame = _oneButton.WasPressedThisFrame(),
                IsTwoButtonPressedThisFrame = _twoButton.WasPressedThisFrame(),
                IsThreeButtonPressedThisFrame = _threeButton.WasPressedThisFrame(),
                IsFourButtonPressedThisFrame = _fourButton.WasPressedThisFrame(),
                IsRotatePressedThisFrame = _rotate.WasPressedThisFrame(),
                IsEscapePressedThisFrame = _escape.WasPressedThisFrame(),
                IsDeletePressedThisFrame = _delete.WasPressedThisFrame(),
            };
            
            _player.SetInput(ref frameInput);
        }
    }
    
    public struct FrameInput
    {
        public bool IsSelectPressedThisFrame;
        public bool IsOneButtonPressedThisFrame;
        public bool IsTwoButtonPressedThisFrame;
        public bool IsThreeButtonPressedThisFrame;
        public bool IsFourButtonPressedThisFrame;
        public bool IsRotatePressedThisFrame;
        public bool IsEscapePressedThisFrame;
        public bool IsDeletePressedThisFrame;
    }
}
