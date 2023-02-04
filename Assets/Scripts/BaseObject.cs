using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObject : MonoBehaviour
    {
        public ISelectable Selectable { get; private set; }
        public Vector3 CurrentGridWorldPosition { get; private set; }
        
        IBaseObjectModule[] _objectModules;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _currentFacingRotationDirection;
        Quaternion _targetRotation;
        IEnumerator _rotateCO;
        bool _isRotateCRRunning;
        bool _isInitialized;
       
        public void Init(Vector3 position)
        {
            if (_isInitialized) return;
            _objectModules = GetComponentsInChildren<IBaseObjectModule>();
          
            SetState(ObjectState.Normal);
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Init(this);
            }

            Selectable = GetObjectSettings();
            
            if (IsOnGrid(position))
            {
                CurrentGridWorldPosition = GetGridWorldPosition(position);
                SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
            }
            
            Events.AnyObjectInitialized(this);
            _isInitialized = true;
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
            RemoveFromGrid();
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnSelected();
            }
            
            Events.AnyObjectSelected(this);
        }

        public void OnDeSelect()
        {
        }

        public bool TryToPlaceObject(Vector3 position)
        {
            if (_currentState == ObjectState.Warning) return false;
            MoveTo(CurrentGridWorldPosition);

            if (_isRotateCRRunning)
            {
                RotateTo(_targetRotation);
                StopCoroutine(_rotateCO);
                _isRotateCRRunning = false;
            }
            
            AddToGrid();
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnPlaced();
            }
            
            Events.AnyObjectPlaced(this);
            return true;
        }

        public void Move(Vector3 position)
        {
            if (IsOnGrid(position))
            {
                CurrentGridWorldPosition = GetGridWorldPosition(position);
                SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
            }
            
            const float factor = 20.0f;
            var newPosition = Vector3.Lerp(transform.position, 
                CurrentGridWorldPosition, Time.deltaTime * factor);
            
            MoveTo(newPosition);
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

        ISelectable GetObjectSettings()
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

        bool IsOnGrid(Vector3 position)
        {
            return GameWorld.ActiveGrid.IsPositionOnGrid(position);
        }

        Vector3 GetGridWorldPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.GetGridWorldPosition(position);
        }

        void AddToGrid()
        {
            GameWorld.ActiveGrid.AddObjectToGrid(CurrentGridWorldPosition, Selectable.GetSettings());
        }

        void RemoveFromGrid()
        {
            GameWorld.ActiveGrid.RemoveObjectFromGrid(CurrentGridWorldPosition, Selectable.GetSettings());
        }
    }
}
