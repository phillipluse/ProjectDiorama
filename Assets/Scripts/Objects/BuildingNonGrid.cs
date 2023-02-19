using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class BuildingNonGrid : MonoBehaviour, IBaseObjectModule, ISelectable
    {
        [Header("References")]
        [SerializeField] GameObject _visualObject;
        [SerializeField] ObjectVisual _visual;
        
        [Header("Properties")]
        [SerializeField] float _moveHeightOffset;
        
        BaseObject _baseObject;
        TriggerCheck _triggerCheck;
        Vector3 _startSize;
        Rigidbody _rigidbody;

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var bounds = _visualObject.GetComponent<Renderer>().bounds;
            _startSize = bounds.size;
            
            AddRigidbody();
            AddTriggerCheck();

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
            _visualObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            MoveVisualObject(VerticalOffset);
            AddRigidbody();
            AddTriggerCheck();            
            _visual.OnSelect();
        }

        public void OnDeSelect()
        {
           OnPlaced();
        }

        public void OnPlaced()
        {
            _visualObject.layer = LayerMask.NameToLayer("PlacedObject");
            MoveVisualObject(NormalOffset);
            RemoveTriggerCheck();
            Destroy(_rigidbody);
            _visual.OnPlaced();
        }

        public void OnMove()
        {
        }

        public void OnRotate(RotationDirection dir)
        {
        }

        public void Tick()
        {
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
        
        void AddTriggerCheck()
        {
            _triggerCheck = _visualObject.AddComponent<TriggerCheck>();
            _triggerCheck.TriggerEnter += OnTriggerEntered;
            _triggerCheck.TriggerExit += OnTRiggerExited;
        }
        
        void RemoveTriggerCheck()
        {
            _triggerCheck.TriggerEnter -= OnTriggerEntered;
            _triggerCheck.TriggerExit -= OnTRiggerExited;
            Destroy(_triggerCheck);
        }
        
        void AddRigidbody()
        {
            _rigidbody = _visualObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        void OnTriggerEntered()
        {
            Debug.Log($"{gameObject.name}: enter");
            _baseObject.SetState(ObjectState.Warning);
        }

        void OnTRiggerExited()
        {
            _baseObject.SetState(ObjectState.Normal);
        }

        void MoveVisualObject(Vector3 position)
        {
            _visualObject.transform.localPosition = position;
        }

        public BaseObject GetBaseObject() => _baseObject;

        public ObjectSettings GetSettings()
        {
            throw new System.NotImplementedException();
        }

        public Transform GetTransform() => transform;

        public Vector2 FootprintSize()
        {
            throw new System.NotImplementedException();
        }
        
        Vector3 VerticalOffset => new(0.0f, _moveHeightOffset + _startSize.y / 2, 0.0f);
        Vector3 NormalOffset => new(0.0f, _startSize.y / 2, 0.0f);
    }
}
