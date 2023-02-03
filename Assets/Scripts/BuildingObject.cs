using System;
using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BuildingObject : MonoBehaviour, IBaseObjectModule, ISelectable
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;

        BaseObject _baseObject;
        ObjectSettings _settings;
        Vector3 _targetLocalPosition;
        IEnumerator _moveCO;
        bool _isMoveCRRunning;

        public void SetUp()
        {
            // var collider = gameObject.GetComponent<Collider>();
            // var bounds = collider.bounds;
            // var size = bounds.size;
            // var footprint = new Vector2(size.x, size.z);
            // _settings = new ObjectSettings(footprint, _directionOffsetSettings);
        }

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var col = gameObject.GetComponent<Collider>();
            var bounds = col.bounds;
            var size = bounds.size;
            _settings = new ObjectSettings(size);
            
            MoveTo(_settings.Offset);

            _visual.Init();
        }

        public void OnHoverEnter()
        {
            _visual.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _visual.OnHoverExit();
        }

        public void OnPlaced()
        {
            gameObject.layer = LayerMask.NameToLayer("PlacedObject");
            if (!_isMoveCRRunning) return;
            MoveTo(_targetLocalPosition);
            StopCoroutine(_moveCO);
        }

        public void OnMove()
        {
           
        }

        public void OnRotate(RotationDirection dir)
        {
            _settings.SettingsUpdate(dir);
            
            _targetLocalPosition = _settings.Offset;
            if (_isMoveCRRunning) return;
            _moveCO = MoveCO();
            StartCoroutine(_moveCO);
        }

        public void Tick()
        {
        }

        public void OnObjectStateEnter(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Normal: _visual.SetToNormal(); break;
                case ObjectState.Warning: _visual.SetToWarning(); break;
                default: throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        IEnumerator MoveCO()
        {
            _isMoveCRRunning = true;
            
            while (transform.localPosition != _targetLocalPosition)
            {
                var factor = 20.0f;
                var newPosition = Vector3.Lerp(transform.localPosition, _targetLocalPosition, Time.deltaTime * factor);
                MoveTo(newPosition);
                yield return null;
            }

            _isMoveCRRunning = false;
        }

        void MoveTo(Vector3 position)
        {
            transform.localPosition = position;
        }

        public ObjectSettings GetSettings() => _settings;

        public BaseObject GetBaseObject() => _baseObject;
    }
}
