using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public enum ObjectState
    {
        None,
        Normal,
        Warning
    }

    public class BaseObject : MonoBehaviour, ISelectable
    {
        IBaseObjectModule[] _objectModules;
        IObjectSettings _objectSettings;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _currentFacingRotationDirection;
        Vector3 _currentGridWorldPosition;
        
        public void OnHoverEnter()
        {
            Debug.Log("Hover Enter");
        }

        public void OnHoverExit()
        {
            Debug.Log("Hover Exit");
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
            transform.position = gridWorldPosition;
            GameWorld.ActiveGrid.SetObjectToGrid(gridWorldPosition, ObjectSettings);
            return true;
        }

        public void Move(Vector3 position)
        {
            if (GameWorld.ActiveGrid.IsPositionOnGrid(position))
            {
                _currentGridWorldPosition = GameWorld.ActiveGrid.GetGridWorldPosition(position);
            }
            var factor = 20.0f;
            transform.position = Vector3.Lerp(transform.position, _currentGridWorldPosition, Time.deltaTime * factor);

            SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
        }

        public void Rotate()
        {
            _currentFacingRotationDirection = _currentFacingRotationDirection.Next();

            var angleToRotateBy = 90.0f;
            transform.Rotate(transform.up, angleToRotateBy);

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnRotate(_currentFacingRotationDirection);
            }
        }

        public void Init()
        {
            _objectModules = GetComponentsInChildren<IBaseObjectModule>();

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.Init();
            }

            _objectSettings = SetObjectSettings();
            
            SetState(ObjectState.Normal);
        }

        bool ObjectCanBePlacedAtPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.CanPlaceObjectAtPosition(position, ObjectSettings);
        }

        IObjectSettings SetObjectSettings()
        {
            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                if (baseObjectModule is not IObjectSettings objectSettings) continue;
                return objectSettings;
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

        public ObjectSettings ObjectSettings => _objectSettings.GetSettings();
    }
}
