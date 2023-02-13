using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDiorama
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Player _player;
        
        InputAction _select;
        InputAction _oneButton;
        InputAction _twoButton;
        InputAction _threeButton;
        InputAction _fourButton;
        InputAction _rotate;
        InputAction _escape;
        InputAction _delete;
        
        void Start()
        {
            var playerControls = GameWorld.Controls;
            _select = playerControls.PlayerActions.Select;
            _oneButton = playerControls.PlayerActions.OneButton;
            _twoButton = playerControls.PlayerActions.TwoButton;
            _threeButton = playerControls.PlayerActions.ThreeButton;
            _fourButton = playerControls.PlayerActions.FourButton;
            _rotate = playerControls.PlayerActions.Rotate;
            _escape = playerControls.PlayerActions.Escape;
            _delete = playerControls.PlayerActions.Delete;
        }
        
        void Update()
        {
            HandleInput();
        }

        void HandleInput()
        {
            var frameInput = new PlayerFrameInput
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
    
    public struct PlayerFrameInput
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
