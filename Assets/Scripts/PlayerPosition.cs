using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerPosition : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] LayerMask _gridLayerMask;
        [SerializeField] LayerMask _objectLayerMask;

        BaseObject _baseObjectAtPosition;
        bool _isPlayerOverGrid;
        
        RaycastHit _hit;

        void OnEnable()
        {
            Events.AnyObjectSelectedEvent += OnObjectSelect;
        }

        public void Tick()
        {
            _isPlayerOverGrid = MousePosition.IsPositionOverLayerMask(_gridLayerMask, out _hit);

            if (GameWorld.IsObjectBeingPlaced) return;
            CheckIfOverSelectableObject();
        }

        void CheckIfOverSelectableObject()
        {
            if (IsOverSelectableObject(out ISelectable selectable))
            {
                var baseObject = selectable.GetBaseObject();
                ChangeBaseObject(baseObject);
                return;
            }

            Release();
        }

        bool IsOverSelectableObject(out ISelectable selectable)
        {
            if (!MousePosition.IsPositionOverLayerMask(_objectLayerMask, out RaycastHit hit))
            {
                selectable = null;
                return false;
            }
            
            if (hit.transform.gameObject.TryGetComponent(out ISelectable s))
            {
                selectable = s;
                return true;
            }

            selectable = null;
            return false;
        }
        
        bool IsOnSurfaceDirectionUP(Vector3 direction)
        {
            var dot = Vector3.Dot(direction, Vector3.up);
            return dot == 1;
        }
        
        void OnObjectSelect(BaseObject baseObject)
        {
            Release();
        }

        void ChangeBaseObject(BaseObject baseObject)
        {
            if (_baseObjectAtPosition == baseObject) return;
            if (_baseObjectAtPosition)
            {
                _baseObjectAtPosition.OnHoverExit();
            }
            
            _baseObjectAtPosition = baseObject;
            baseObject.OnHoverEnter();
        }

        void Release()
        {
            if (!_baseObjectAtPosition) return;
            _baseObjectAtPosition.OnHoverExit();
            _baseObjectAtPosition = null;
        }

        public BaseObject BaseObjectAtPosition => _baseObjectAtPosition;
        public Vector3 Position => _isPlayerOverGrid ? _hit.point : Vector3.zero;
        public bool IsOverSelectable => _baseObjectAtPosition != null;
    }
}
