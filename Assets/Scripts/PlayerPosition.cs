using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerPosition : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] LayerMask _gridLayerMask;
        [SerializeField] LayerMask _objectLayerMask;

        RaycastHit _hit;
        public bool IsPlayerOnGrid { get; private set; }

        void OnEnable()
        {
            Events.AnyObjectSelectedEvent += OnObjectSelect;
        }

        public void Tick()
        {
            IsPlayerOnGrid = MousePosition.IsPositionOverLayerMask(_gridLayerMask, out _hit);

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
            if (BaseObjectAtPosition == baseObject) return;
            if (BaseObjectAtPosition)
            {
                BaseObjectAtPosition.OnHoverExit();
            }
            
            BaseObjectAtPosition = baseObject;
            baseObject.OnHoverEnter();
        }

        void Release()
        {
            if (!BaseObjectAtPosition) return;
            BaseObjectAtPosition.OnHoverExit();
            BaseObjectAtPosition = null;
        }

        public BaseObject BaseObjectAtPosition { get; private set; }

        public Vector3 Position => IsPlayerOnGrid ? _hit.point : MousePosition.GetScreenToWorldPoint();
        public bool IsOverSelectable => BaseObjectAtPosition != null;
    }
}
