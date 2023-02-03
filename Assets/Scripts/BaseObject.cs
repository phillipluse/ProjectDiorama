using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObject : MonoBehaviour
    {
        IBaseObjectModule[] _objectModules;
        ObjectSettings _objectSettings;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _currentFacingRotationDirection;
        Vector3 _currentGridWorldPosition;
        Quaternion _targetRotation;
        IEnumerator _rotateCO;
        bool _isRotateCRRunning;
        
        public void OnHoverEnter()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnHoverEnter();
            }
        }

        public void OnHoverExit()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnHoverExit();
            }
        }

        public void OnSelect()
        {
            Events.AnyObjectOnSelected(gameObject);
            Init();
        }

        public void OnDeSelect()
        {
        }

        public bool TryToPlaceObject(Vector3 position)
        {
            if (_currentState == ObjectState.Warning) return false;
            var gridWorldPosition = GameWorld.ActiveGrid.GetGridWorldPosition(position);
            MoveTo(gridWorldPosition);

            if (_isRotateCRRunning)
            {
                RotateTo(_targetRotation);
                StopCoroutine(_rotateCO);
            }

            GameWorld.ActiveGrid.SetObjectToGrid(gridWorldPosition, _objectSettings);
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnPlaced();
            }
            
            return true;
        }

        public void Move(Vector3 position)
        {
            if (GameWorld.ActiveGrid.IsPositionOnGrid(position))
            {
                _currentGridWorldPosition = GameWorld.ActiveGrid.GetGridWorldPosition(position);
            }
            
            const float factor = 20.0f;
            var newPosition = Vector3.Lerp(transform.position, 
                _currentGridWorldPosition, Time.deltaTime * factor);
            MoveTo(newPosition);
            
            SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
        }

        public void Rotate()
        {
            _currentFacingRotationDirection = _currentFacingRotationDirection.Next();
            _targetRotation = Quaternion.Euler(0, _currentFacingRotationDirection.RotationAngle(), 0);

            if (!_isRotateCRRunning)
            {
                _rotateCO = RotateCO();
                StartCoroutine(_rotateCO);
            }

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnRotate(_currentFacingRotationDirection);
            }
        }

        IEnumerator RotateCO()
        {
            _isRotateCRRunning = true;
            while (transform.rotation.eulerAngles.y != _targetRotation.eulerAngles.y)
            {
                const float factor = 20.0f;
                var newRotation = Quaternion.Lerp(transform.rotation, 
                    _targetRotation, Time.deltaTime * factor);
                RotateTo(newRotation);
                yield return null;
            }

            _isRotateCRRunning = false;
        }

        void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        void RotateTo(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        void Init()
        {
            _objectModules = GetComponentsInChildren<IBaseObjectModule>();

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Init(this);
            }

            _objectSettings = GetObjectSettings();
            
            SetState(ObjectState.Normal);
        }

        bool ObjectCanBePlacedAtPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.CanPlaceObjectAtPosition(position, _objectSettings);
        }

        ObjectSettings GetObjectSettings()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                if (baseObjectModule is not ISelectable selectable) continue;
                return selectable.GetSettings();
            }

            return null;
        }

        void SetState(ObjectState state)
        {
            if (_currentState == state) return;
            _currentState = state;
            OnStateEnter(state);
        }

        void OnStateEnter(ObjectState state)
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnObjectStateEnter(state);
            }
        }
    }
}
