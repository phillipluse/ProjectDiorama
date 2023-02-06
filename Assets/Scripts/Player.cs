using UnityEngine;

namespace ProjectDiorama
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerPosition _playerPosition;
        [SerializeField] GameObject _tempShortcutObject1;
        [SerializeField] GameObject _tempShortcutObject2;
        [SerializeField] GameObject _tempShortcutObject3;
        [SerializeField] GameObject _tempShortcutObject4;

        BaseObject _selectedBaseObject;
        bool _isSelectPressed;
        bool _isRotatePressed;
        
        void Update()
        {
            _playerPosition.Tick();
            CheckIfMoving();
            CheckIfRotate();
            CheckForSelectPress();
        }

        public void SetInput(ref PlayerFrameInput input)
        {
            if (input.IsOneButtonPressedThisFrame)
            {
                CreateObject(_tempShortcutObject1);
                return;
            }
            
            if (input.IsTwoButtonPressedThisFrame)
            {
                CreateObject(_tempShortcutObject2);
                return;
            }            
            
            if (input.IsThreeButtonPressedThisFrame)
            {
                CreateObject(_tempShortcutObject3);
                return;
            }

            if (input.IsFourButtonPressedThisFrame)
            {
                CreateObject(_tempShortcutObject4);
                return;
            }

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

        void CheckIfMoving()
        {
            if (!HasActiveObject) return;
            _selectedBaseObject.Move(_playerPosition.Position);
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

        void CreateObject(GameObject go)
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

        public bool HasActiveObject => _selectedBaseObject != null;
        public Vector2 Position => _playerPosition.Position;
    }
}
