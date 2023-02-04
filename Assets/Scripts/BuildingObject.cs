using System;
using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BuildingObject : MonoBehaviour, IBaseObjectModule, ISelectable
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;
        [SerializeField] float _moveHeightOffset;

        BaseObject _baseObject;
        ObjectSettings _settings;
        Vector3 _targetLocalPosition;
        Vector3 _movingOffset;
        IEnumerator _moveCO;
        bool _isMoveCRRunning;

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var col = gameObject.GetComponent<Collider>();
            var bounds = col.bounds;
            var size = bounds.size;
            _settings = new ObjectSettings(size);

            _movingOffset = new Vector3(0.0f, _moveHeightOffset, 0.0f);
            
            MoveTo(_settings.Offset + _movingOffset);

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

        public void OnSelected()
        {
            MoveTo(_settings.Offset + _movingOffset);
            _visual.OnSelect();
        }

        public void OnDeSelect() { }

        public void OnPlaced()
        {
            gameObject.layer = LayerMask.NameToLayer("PlacedObject");
            _visual.OnPlaced();
            MoveTo(transform.localPosition - _movingOffset);
            
            if (!_isMoveCRRunning) return;
            MoveTo(_targetLocalPosition - _movingOffset);
            StopCoroutine(_moveCO);
            _isMoveCRRunning = false;
        }

        public void OnMove() { }

        public void OnRotate(RotationDirection dir)
        {
            _settings.SettingsUpdate(dir);
            
            _targetLocalPosition = _settings.Offset + _movingOffset;
            if (_isMoveCRRunning) return;
            _moveCO = MoveCO();
            StartCoroutine(_moveCO);
        }

        public void Tick() { }

        public void OnObjectStateEnter(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Normal: _visual.SetToGhost(); break;
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
                var newPosition = Vector3.Lerp(transform.localPosition, 
                    _targetLocalPosition, Time.deltaTime * factor);
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
        public Transform GetTransform() => transform;
        public BaseObject GetBaseObject() => _baseObject;
    }
}
