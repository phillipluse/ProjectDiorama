using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObject : MonoBehaviour, ISelectable
    {
        IBaseObjectModule[] _objectModules;
        IObjectSettings _objectSettings;

        ObjectState _currentState = ObjectState.None;
        RotationDirection _currentFacingRotationDirection;
        Vector3 _currentGridWorldPosition;
        Quaternion _targetRotation;
        bool _isRotateCRRunning;
        
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

            _targetRotation = Quaternion.Euler(0, _currentFacingRotationDirection.RotationAngle(), 0);

            Debug.Log($"{_currentFacingRotationDirection.RotationAngle()}");


            // transform.rotation = _targetRotation;
            
            // transform.Rotate(transform.up, angleToRotateBy);

            if (!_isRotateCRRunning)
            {
                StartCoroutine(RotateCR());
            }

            foreach (IBaseObjectModule baseObjectModule in _objectModules)
            {
                baseObjectModule.OnRotate(_currentFacingRotationDirection);
            }
        }

        IEnumerator RotateCR()
        {
            _isRotateCRRunning = true;
            while (transform.rotation != _targetRotation)
            {
                var factor = 20.0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * factor);
                Debug.Log($"IsRotating; Target Rotation: {_targetRotation.eulerAngles}");
                yield return null;
            }

            _isRotateCRRunning = false;
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
