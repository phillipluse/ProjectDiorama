using System;
using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace ProjectDiorama
{
    public class BuildingGridObject : MonoBehaviour, IBaseObjectModule, ISelectable
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;
        [SerializeField] GameObject _visualObject;
        [SerializeField] MMF_Player _feedbackPlayer;

        [Header("Properties")]
        [SerializeField] float _moveHeightOffset;

        const int MAX_NUM_OF_TURNS = 4;
        int _turnNumber;
        
        BaseObject _baseObject;
        ObjectSettings _settings;
        IEnumerator _moveCO;
        Vector3 _tempLocalPosition;
        bool _isMoveCRRunning;

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var bounds = _visualObject.GetComponent<Renderer>().bounds;
            _settings = new ObjectSettings(bounds.size);

            MoveVisualObject(VerticalOffset);

            var offset = _settings.ObjectOffset(RotationDirection.Up);
            offset.y = 0;
            MoveTo(offset);

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
            MoveVisualObject(VerticalOffset);
            _visual.OnSelect();
        }

        public void OnDeSelect()
        {
            MoveVisualObject(NormalOffset);
            Debug.Log($"{_baseObject.PlacedRotationDirection}");
            _settings.UpdateRotatedSize(_baseObject.PlacedRotationDirection);
            _tempLocalPosition = _settings.ObjectOffset(_baseObject.PlacedRotationDirection);
            if (_isMoveCRRunning)
            {
                StopCoroutine(_moveCO);
                _isMoveCRRunning = false;
            }
            MoveTo(_tempLocalPosition);
            _visual.OnPlaced();
        }

        public void OnPlaced()
        {
            _visualObject.layer = LayerMask.NameToLayer("PlacedObject");
            MoveVisualObject(NormalOffset);
            _visual.OnPlaced();

            // var parentScale = transform.parent.localScale;
            // parentScale.y = 0;
            // transform.parent.localScale = parentScale;
            
            if (!_isMoveCRRunning) return;
            
            MoveTo(_tempLocalPosition);
            StopCoroutine(_moveCO);
            _isMoveCRRunning = false;
        }

        public void OnMove() { }

        public void OnRotate(RotationDirection dir)
        {
            _settings.UpdateRotatedSize(dir);
            _tempLocalPosition = _settings.ObjectOffset(dir);
            if (_isMoveCRRunning) return;
            _moveCO = MoveCO();
            StartCoroutine(_moveCO);
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

        public void Tick()
        {
            // _visual.OnPlaced();
            // if (_turnNumber == MAX_NUM_OF_TURNS) return;
            // _feedbackPlayer.PlayFeedbacks();
            // _turnNumber++;
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

        void MoveVisualObject(Vector3 position)
        {
            _visualObject.transform.localPosition = position;
        }

        void MoveTo(Vector3 position)
        {
            transform.localPosition = position;
        }

        public ObjectSettings GetSettings() => _settings;
        public Transform GetTransform() => transform;
        public BaseObject GetBaseObject() => _baseObject;
        public Vector2 FootprintSize() => _settings.FootprintGridSize;
        Vector3 VerticalOffset => new(0.0f, _moveHeightOffset + _settings.StartObjectSize.y / 2, 0.0f);
        Vector3 NormalOffset => new(0.0f, _settings.StartObjectSize.y / 2, 0.0f);
    }
}
