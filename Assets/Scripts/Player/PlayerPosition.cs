using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerPosition : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] LayerMask _gridLayerMask;
        [SerializeField] LayerMask _groundPlaneMask;
        [SerializeField] LayerMask _objectLayerMask;

        RaycastHit _hit;
        public Vector3 Position { get; private set; }

        public bool IsPlayerOnGrid { get; private set; }

        void OnEnable()
        {
            Events.AnyObjectSelectedEvent += OnObjectSelect;
        }

        public void Tick()
        {
            GetPlayerPosition();
            
            if (GameWorld.IsObjectBeingPlaced) return;
            CheckIfOverSelectableObject();
        }

        void GetPlayerPosition()
        {
            RaycastHit hit;
            if (MousePosition.IsPositionOverLayerMask(_gridLayerMask, out hit))
            {
                IsPlayerOnGrid = true;
                Position = hit.point;
            }
            else if (MousePosition.IsPositionOverLayerMask(_groundPlaneMask, out hit))
            {
                IsPlayerOnGrid = false;
                Position = hit.point;
            }
            else
            {
                IsPlayerOnGrid = false;
                Position = Vector3.zero;
            }
        }

        void CheckIfOverSelectableObject()
        {
            if (IsOverSelectableObject(out ISelectable selectable) && !MousePosition.IsOverUI())
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
        public bool IsOverSelectable => BaseObjectAtPosition != null;
    }
}
