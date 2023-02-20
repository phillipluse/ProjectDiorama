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

        void Awake()
        {
            _shortcuts = new PlayerObjectSelectShortcuts(this);
        }

        void Update()
        {
            _playerPosition.Tick();
            CheckIfMoving();
            CheckIfRotate();
            CheckForSelectPress();
        }

        public void SetInput(ref PlayerFrameInput input)
        {
            if (input.IsRotatePressedThisFrame && !_isRotatePressed && HasActiveObject)
            {
                _isRotatePressed = true;
                return;
            }

            if (input.IsSelectPressedThisFrame && !_isSelectPressed)
            {
                _isSelectPressed = true;
                return;
            }

            if (input.IsEscapePressedThisFrame && HasActiveObject)
            {
                if (_isRotatePressed) _isRotatePressed = false;
                _selectedBaseObject.OnDeSelect();
                _selectedBaseObject = null;
                return;
            }
            
            if (input.IsDeletePressedThisFrame && HasActiveObject)
            {
                _selectedBaseObject.OnDelete();
                _selectedBaseObject = null;
                return;
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

        void CheckIfMoving()
        {
            if (!HasActiveObject) return;
            _selectedBaseObject.Tick(_playerPosition.Position);
        }

        void CheckIfRotate()
        {
            if (!_isRotatePressed) return;
            _selectedBaseObject.Rotate();
            _isRotatePressed = false;
        }

        void CheckForSelectPress()
        {
            if (!_isSelectPressed) return;
            ProcessSelectAction();
            _isSelectPressed = false;
        }

        void ProcessSelectAction()
        {
            if (HasActiveObject)
            {
                PlaceObject();
                return;
            }

            if (!_playerPosition.IsOverSelectable) return;
            _selectedBaseObject = _playerPosition.BaseObjectAtPosition;
            _selectedBaseObject.OnSelected();
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
