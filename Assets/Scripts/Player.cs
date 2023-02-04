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

        public void SetInput(ref FrameInput input)
        {
            if (input.IsOneButtonPressedThisFrame && !HasActiveObject)
            {
                CreateObject(_tempShortcutObject1);
                return;
            }
            
            if (input.IsTwoButtonPressedThisFrame && !HasActiveObject)
            {
                CreateObject(_tempShortcutObject2);
                return;
            }            
            
            if (input.IsThreeButtonPressedThisFrame && !HasActiveObject)
            {
                CreateObject(_tempShortcutObject3);
                return;
            }

            if (input.IsFourButtonPressedThisFrame && !HasActiveObject)
            {
                CreateObject(_tempShortcutObject4);
                return;
            }

            if (input.IsRotatePressedThisFrame && !_isRotatePressed && HasActiveObject)
            {
                _isRotatePressed = true;
            }

            if (input.IsSelectPressedThisFrame && !_isSelectPressed)
            {
                _isSelectPressed = true;
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

            if (_playerPosition.IsOverSelectable)
            {
                _selectedBaseObject = _playerPosition.BaseObjectAtPosition;
                _selectedBaseObject.OnSelected();
            }
        }

        void PlaceObject()
        {
            if (_selectedBaseObject.TryToPlaceObject(_playerPosition.Position))
            {
                _selectedBaseObject = null;
            }
        }

        void CreateObject(GameObject go)
        {
            var newGo = Instantiate(go, _playerPosition.Position, Quaternion.identity);
            
            if (newGo.TryGetComponent(out BaseObject baseObject))
            {
                _selectedBaseObject = baseObject;
                baseObject.Init(_playerPosition.Position);
            }
        }

        public bool HasActiveObject => _selectedBaseObject != null;
    }
}
