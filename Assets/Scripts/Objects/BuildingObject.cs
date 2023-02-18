using System;
using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace ProjectDiorama
{
    public class BuildingObject : MonoBehaviour, IBaseObjectModule, ISelectable
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;
        [SerializeField] float _moveHeightOffset;
        [SerializeField] MMF_Player _feedbackPlayer;

        const int MAX_NUM_OF_TURNS = 4;
        int _turnNumber;
        
        BaseObject _baseObject;
        ObjectSettings _settings;
        IEnumerator _moveCO;
        Vector3 _tempLocalPosition;
        Vector3 _placedLocationPosition;
        Vector3 _movingOffset;
        bool _isMoveCRRunning;

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var col = gameObject.GetComponent<Collider>();
            var bounds = col.bounds;
            var size = bounds.size;
            _settings = new ObjectSettings(size);

            _movingOffset = new Vector3(0.0f, _moveHeightOffset, 0.0f);

            var offset = _settings.ObjectOffset(RotationDirection.Up);
            
            MoveTo(offset + _movingOffset);

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
            _placedLocationPosition = transform.localPosition;
            MoveTo( _placedLocationPosition + _movingOffset);
            _visual.OnSelect();
        }

        public void OnDeSelect()
        {
            MoveTo(_placedLocationPosition);
            _settings.UpdateRotatedSize(_baseObject.PlacedRotationDirection);
            _visual.OnPlaced();
        }

        public void OnPlaced()
        {
            gameObject.layer = LayerMask.NameToLayer("PlacedObject");
            MoveTo(transform.localPosition - _movingOffset);

            var parentScale = transform.parent.localScale;
            parentScale.y = 0;
            transform.parent.localScale = parentScale;
            
            if (!_isMoveCRRunning) return;
            
            MoveTo(_tempLocalPosition - _movingOffset);
            StopCoroutine(_moveCO);
            _isMoveCRRunning = false;
        }

        public void OnMove() { }

        public void OnRotate(RotationDirection dir)
        {
            _settings.UpdateRotatedSize(dir);
            var offSet = _settings.ObjectOffset(dir);
            
            _tempLocalPosition = offSet + _movingOffset;
            if (_isMoveCRRunning) return;
            _moveCO = MoveCO();
            StartCoroutine(_moveCO);
        }

        public void Tick()
        {
            _visual.OnPlaced();
            if (_turnNumber == MAX_NUM_OF_TURNS) return;
            _feedbackPlayer.PlayFeedbacks();
            _turnNumber++;
        }

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
            
            while (transform.localPosition != _tempLocalPosition)
            {
                var factor = 20.0f;
                var newPosition = Vector3.Slerp(transform.localPosition, 
                    _tempLocalPosition, Time.deltaTime * factor);
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
        public Vector2 FootprintSize() => _settings.FootprintGridSize;
    }
}
