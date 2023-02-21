using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerPosition _playerPosition;

        PlayerObjectSelectShortcuts _shortcuts;
        BaseObject _selectedBaseObject;
        bool _isSelectPressed;
        bool _isRotatePressed;

        void OnEnable()
        {
            Events.CreateObjectEvent += CreateObject;
        }

        void OnDisable()
        {
            Events.CreateObjectEvent -= CreateObject;
        }

        void Start()
        {
            _shortcuts = new PlayerObjectSelectShortcuts(this);
            // GameWorld.Controls.PlayerActions.Rotate.performed += _ => ProcessRotate();
            // GameWorld.Controls.PlayerActions.Select.performed += _ => ProcessSelectPress();
            // GameWorld.Controls.PlayerActions.Escape.performed += _ => ProcessEscapePress();
            // GameWorld.Controls.PlayerActions.Delete.performed += _ => ProcessDeletePress();
        }

        void Update()
        {
            _playerPosition.Tick();
            ProcessActiveObject();
            // RotateInput();
            // ProcessSelectPress();
        }

        public void SetInput(ref PlayerFrameInput input)
        {
            if (input.IsRotatePressedThisFrame)
            {
                ProcessRotate();
                return;
            }

            if (input.IsSelectPressedThisFrame)
            {
                ProcessSelectPress();
                return;
            }

            if (input.IsEscapePressedThisFrame)
            {
                ProcessEscapePress();
                return;
            }
            
            if (input.IsDeletePressedThisFrame)
            {
                ProcessDeletePress();
            }
        }

        public void CreateObject(GameObject go)
        {
            var spawnHeight = 20.0f;

            if (HasActiveObject)
            {
                spawnHeight = 3.0f;
                _selectedBaseObject.OnDeSelect();
            }
            
            var spawnPosition = new Vector3(_playerPosition.Position.x, spawnHeight, _playerPosition.Position.z);
            var newGo = Instantiate(go, spawnPosition, Quaternion.identity);
            
            if (newGo.TryGetComponent(out BaseObject baseObject))
            {
                _selectedBaseObject = baseObject;
                baseObject.Init(_playerPosition.Position);
            }
        }

        void ProcessActiveObject()
        {
            if (!HasActiveObject) return;
            _selectedBaseObject.Tick(_playerPosition.Position);
        }

        void ProcessRotate()
        {
            // if (!_isRotatePressed) return;
            if (!HasActiveObject) return;
            _selectedBaseObject.Rotate();
            // _isRotatePressed = false;
        }

        void ProcessSelectPress()
        {
            // if (!_isSelectPressed) return;
            // ProcessSelectAction();
            if (HasActiveObject)
            {
                if (MousePosition.IsOverUI()) return;
                PlaceObject();
                return;
            }

            if (!_playerPosition.IsOverObject) return;
            _selectedBaseObject = _playerPosition.BaseObjectAtPosition;
            _selectedBaseObject.OnSelected();
            // _isSelectPressed = false;
        }

        void ProcessSelectAction()
        {
            if (HasActiveObject)
            {
                PlaceObject();
                return;
            }

            if (!_playerPosition.IsOverObject) return;
            _selectedBaseObject = _playerPosition.BaseObjectAtPosition;
            _selectedBaseObject.OnSelected();
        }

        void ProcessEscapePress()
        {
            if (!HasActiveObject) return;
            _selectedBaseObject.OnDeSelect();
            _selectedBaseObject = null;
        }
        
        void ProcessDeletePress()
        {
            if (!HasActiveObject) return;
            _selectedBaseObject.OnDelete();
            _selectedBaseObject = null;
        }

        void PlaceObject()
        {
            if (_selectedBaseObject.TryToPlaceObject())
            {
                _selectedBaseObject = null;
            }
        }

        public BaseObject ActiveObject => _selectedBaseObject;
        public bool HasActiveObject => _selectedBaseObject != null;
        public bool IsPlayerOverGrid => _playerPosition.IsPlayerOnGrid;
        public Vector3 Position => _playerPosition.Position;
    }
}
