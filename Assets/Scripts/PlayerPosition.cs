using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerPosition : MonoBehaviour
    {
        [SerializeField] LayerMask _gridLayerMask;
        
        public bool IsPlayerOverGrid { get; private set; }
        ISelectable _currentISelectable;
        
        RaycastHit _hit;

        public void Tick()
        {
            IsPlayerOverGrid = MousePosition.IsPositionOverLayerMask(_gridLayerMask, out _hit);

            // CheckIfOverSelectableObject();
        }

        void CheckIfOverSelectableObject()
        {
            if (IsOverSelectableObject(out ISelectable selectable))
            {
                if (_currentISelectable == selectable) return;
                if (_currentISelectable != null)
                {
                    _currentISelectable.OnHoverExit();
                }

                _currentISelectable = selectable;
                selectable.OnHoverEnter();
                return;
            }

            if (_currentISelectable == null) return;
            _currentISelectable.OnHoverExit();
            _currentISelectable = null;
        }


        bool IsOverSelectableObject(out ISelectable selectable)
        {
            if (!IsOverObject)
            {
                selectable = null;
                return false;
            }
            
            if (_hit.transform.gameObject.TryGetComponent(out ISelectable s))
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

        public ISelectable CurrentSelectable => _currentISelectable;
        public Vector3 Position => IsPlayerOverGrid ? _hit.point : Vector3.zero;
        public bool IsOverObject => _hit.collider != null;
        public bool IsOverSelectable => _currentISelectable != null;
    }
}
