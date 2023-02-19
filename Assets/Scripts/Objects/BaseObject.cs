using System;
using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public abstract class BaseObject : MonoBehaviour
    {
        public Action<ObjectState> StateChange;
        public ISelectable Selectable { get; private set; }
        public RotationDirection PlacedRotationDirection { get; private set; }
        
        IBaseObjectModule[] _objectModules;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _tempRotationDirection;
        Quaternion _targetRotation;
        IEnumerator _rotateCO;
        bool _isRotateCRRunning;
        bool _isInitialized;
        bool _isSelected;
       
        public virtual void Init(Vector3 position)
        {
            if (_isInitialized) return;
            _objectModules = GetComponentsInChildren<IBaseObjectModule>();
            
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Init(this);
            }

            Selectable = GetSelectable();
            
            _isInitialized = true;
            Events.AnyObjectInitialized(this);
            UIEvents.TurnButtonPressedEvent += OnTurnButtonPressed;
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

        public virtual void OnSelected()
        {
            PlacedRotationDirection = _tempRotationDirection;
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
                MoveBackToStartPositionAndRotation();
                foreach (IBaseObjectModule baseObjectModule in _objectModules)
                {
                    baseObjectModule.OnDeSelect();
                }
           
                Events.AnyObjectDeSelected(this);
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

        public virtual bool TryToPlaceObject()
        {
            if (_currentState == ObjectState.Warning) return false;

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
           
            Events.AnyObjectPlaced(this);
            return true;
        }

        public abstract void Move(Vector3 position);

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

        protected void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        void RotateTo(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        protected virtual void MoveBackToStartPositionAndRotation()
        {
            _tempRotationDirection = PlacedRotationDirection;
            
            var rotation = Quaternion.Euler(0, PlacedRotationDirection.RotationAngle(), 0);
            transform.rotation =  rotation;

            if (_isRotateCRRunning)
            {
                StopCoroutine(_rotateCO);
                _isRotateCRRunning = false;
            }
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

        public void SetState(ObjectState state)
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
        
        void OnTurnButtonPressed()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Tick();
            }
        }
    }
}
