using System;
using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObject : MonoBehaviour
    {
        public Action<ObjectState> StateChange;
        public ISelectable Selectable { get; private set; }
        public RotationDirection PlacedRotationDirection { get; private set; }
        
        IBaseObjectModule[] _objectModules;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _tempRotationDirection;
        Quaternion _targetRotation;
        IEnumerator _rotateCO;
        Vector3 _tempGridWorldPosition;
        Vector3 _placedGridWorldPosition;
        bool _isRotateCRRunning;
        bool _isInitialized;
        bool _isSelected;
       
        public void Init(Vector3 position)
        {
            if (_isInitialized) return;
            _objectModules = GetComponentsInChildren<IBaseObjectModule>();
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Init(this);
            }

            Selectable = GetSelectable();
            
            if (IsOnGrid(position))
            {
                _tempGridWorldPosition = GetGridWorldPosition(position);
            }
            
            _isInitialized = true;
            Events.AnyObjectInitialized(this);
            SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
        }
        
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

        public void OnSelected()
        {
            _placedGridWorldPosition = _tempGridWorldPosition;
            PlacedRotationDirection = _tempRotationDirection;
            RemoveFromGrid();
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnSelected();
            }

            _isSelected = true;
            Events.AnyObjectSelected(this);
        }

        public void OnDeSelect()
        {
            if (_isSelected)
            {
                MoveBackToStartPosition();
                return;
            }
            
            Events.AnyObjectDeSelected(this);
            Destroy(gameObject);
        }

        public void OnDelete()
        {
            Events.AnyObjectDeSelected(this);
            Destroy(gameObject);
        }

        public bool TryToPlaceObject()
        {
            if (_currentState == ObjectState.Warning) return false;
            MoveTo(_tempGridWorldPosition);

            if (_isRotateCRRunning)
            {
                RotateTo(_targetRotation);
                StopCoroutine(_rotateCO);
                _isRotateCRRunning = false;
            }
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnPlaced();
            }
            
            AddToGrid(_tempGridWorldPosition);
            Events.AnyObjectPlaced(this);
            return true;
        }

        public void Move(Vector3 position)
        {
            if (IsOnGrid(position))
            {
                _tempGridWorldPosition = GetGridWorldPosition(position);
                SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
            }
            
            const float factor = 20.0f;
            var newPosition = Vector3.Lerp(transform.position, 
                _tempGridWorldPosition, Time.deltaTime * factor);
            
            MoveTo(newPosition);
        }

        public void Rotate()
        {
            _tempRotationDirection = _tempRotationDirection.Next();
            _targetRotation = Quaternion.Euler(0, _tempRotationDirection.RotationAngle(), 0);

            if (!_isRotateCRRunning)
            {
                _rotateCO = RotateCO();
                StartCoroutine(_rotateCO);
            }

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnRotate(_tempRotationDirection);
            }
        }

        IEnumerator RotateCO()
        {
            _isRotateCRRunning = true;
            while (transform.rotation.eulerAngles.y != _targetRotation.eulerAngles.y)
            {
                const float factor = 20.0f;
                var newRotation = Quaternion.Slerp(transform.rotation, 
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
        
        void MoveBackToStartPosition()
        {
            _tempRotationDirection = PlacedRotationDirection;
            _tempGridWorldPosition = _placedGridWorldPosition;
            var rotation = Quaternion.Euler(0, PlacedRotationDirection.RotationAngle(), 0);
            transform.SetPositionAndRotation(_placedGridWorldPosition, rotation);

            if (_isRotateCRRunning)
            {
                StopCoroutine(_rotateCO);
                _isRotateCRRunning = false;
            }

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnDeSelect();
            }

            AddToGrid(_placedGridWorldPosition);
            Events.AnyObjectDeSelected(this);
        }

        ISelectable GetSelectable()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                if (baseObjectModule is not ISelectable selectable) continue;
                return selectable;
            }

            return null;
        }

        void SetState(ObjectState state)
        {
            if (_currentState == state) return;
            _currentState = state;
            OnStateEnter(state);
            StateChange?.Invoke(state);
        }

        void OnStateEnter(ObjectState state)
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnObjectStateEnter(state);
            }
        }
        
        bool ObjectCanBePlacedAtPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.CanPlaceObjectAtPosition(position, Selectable.GetSettings());
        }

        static bool IsOnGrid(Vector3 position)
        {
            return GameWorld.ActiveGrid.IsPositionOnGrid(position);
        }

        static Vector3 GetGridWorldPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.GetGridWorldPosition(position);
        }

        void AddToGrid(Vector3 worldPosition)
        {
            GameWorld.ActiveGrid.AddObjectToGrid(worldPosition, Selectable.GetSettings());
        }

        void RemoveFromGrid()
        {
            GameWorld.ActiveGrid.RemoveObjectFromGrid(_tempGridWorldPosition, Selectable.GetSettings());
        }
    }
}
