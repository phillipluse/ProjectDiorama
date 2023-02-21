using System;
using UnityEngine;

namespace ProjectDiorama
{
    /// <summary>
    /// Handles movement and rotations of object component
    /// Handles object component visual
    /// Handles state of object component and communicates to Base Object
    /// </summary>
    public class FreeMoveComponent : MonoBehaviour, IBaseObjectModule, ISnap
    {
        [Header("References")]
        [SerializeField] GameObject _visualObject;
        [SerializeField] ObjectVisual _visual;
        
        [Header("Properties")]
        [SerializeField] float _moveHeightOffset;
        [SerializeField] LayerMask _triggerExclusionLayers;
        
        BaseObject _baseObject;
        TriggerCheck _triggerCheck;
        Vector3 _startSize;
        Rigidbody _rigidbody;

        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
            
            var bounds = _visualObject.GetComponent<Renderer>().bounds;
            _startSize = bounds.size;
            MoveVisualObject(MovingOffset);
            
            AddRigidbody();
            AddTriggerCheck();

            _visual.Init();
        }
        
        public void Tick()
        {
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
            MoveVisualObject(MovingOffset);
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
            RemoveTriggerCheck();
            Destroy(_rigidbody);
            if (_baseObject.CurrentState.IsSnapped()) return; 
            MoveVisualObject(NormalOffset);
        }

        public void OnObjectStateEnter(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Normal: _visual.SetToGhost(); break;
                case ObjectState.Warning: _visual.SetToWarning(); break;
                case ObjectState.None: break;
                case ObjectState.Snapped: break;
                default: throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        public void Snap(Transform toTransform, Transform fromTransform, Transform currentTransform)
        {
            VisualObjectHeight(toTransform.position.y);
        }

        public void UnSnap()
        {
            MoveVisualObject(MovingOffset);
        }

        void AddTriggerCheck()
        {
            _triggerCheck = _visualObject.AddComponent<TriggerCheck>();
            _triggerCheck.TriggerEnter += OnTriggerEntered;
            _triggerCheck.TriggerExit += OnTriggerExited;
            _triggerCheck.TriggerStay += OnTriggerStayed;
        }
        
        void RemoveTriggerCheck()
        {
            _triggerCheck.TriggerEnter -= OnTriggerEntered;
            _triggerCheck.TriggerExit -= OnTriggerExited;
            _triggerCheck.TriggerStay -= OnTriggerStayed;

            Destroy(_triggerCheck);
        }
        
        void AddRigidbody()
        {
            _rigidbody = _visualObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        void OnTriggerEntered(Collider other)
        {
            if (_triggerExclusionLayers.Contains(other.gameObject.layer)) return;
            if (_baseObject.CurrentState.IsSnapped()) return;
            _baseObject.SetState(ObjectState.Warning);
        }

        void OnTriggerStayed(Collider other)
        {
            if (_triggerExclusionLayers.Contains(other.gameObject.layer)) return;
            if (_baseObject.CurrentState.IsSnapped()) return;
            _baseObject.SetState(ObjectState.Warning);
        }

        void OnTriggerExited(Collider other)
        {
            if (_triggerExclusionLayers.Contains(other.gameObject.layer)) return;
            _baseObject.SetState(ObjectState.Normal);
        }

        void MoveVisualObject(Vector3 position)
        {
            _visualObject.transform.localPosition = position;
        }

        void VisualObjectHeight(float height)
        {
            var newHeight = new Vector3(0.0f, height, 0.0f);
            _visualObject.transform.localPosition = newHeight;
        }

        Vector3 MovingOffset => new(0.0f, _moveHeightOffset + _startSize.y / 2, 0.0f);
        Vector3 NormalOffset => new(0.0f, _startSize.y / 2, 0.0f);
    }
}
