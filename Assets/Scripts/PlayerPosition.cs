using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerPosition : MonoBehaviour
    {
        [SerializeField] LayerMask _gridLayerMask;
        [SerializeField] LayerMask _objectLayerMask;
        
        public bool IsPlayerOverGrid { get; private set; }
        BaseObject _currentBaseObject;
        
        RaycastHit _hit;

        public void Tick()
        {
            IsPlayerOverGrid = MousePosition.IsPositionOverLayerMask(_gridLayerMask, out _hit);

            CheckIfOverSelectableObject();
        }

        void CheckIfOverSelectableObject()
        {
            if (IsOverSelectableObject(out ISelectable selectable))
            {
                var baseObject = selectable.GetBaseObject();
                
                if (_currentBaseObject == baseObject) return;
                if (_currentBaseObject)
                {
                    _currentBaseObject.OnHoverExit();
                }

                _currentBaseObject = baseObject;
                baseObject.OnHoverEnter();
                return;
            }

            if (!_currentBaseObject) return;
            _currentBaseObject.OnHoverExit();
            _currentBaseObject = null;
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

        public BaseObject CurrentBaseObject => _currentBaseObject;
        public Vector3 Position => IsPlayerOverGrid ? _hit.point : Vector3.zero;
        public bool IsOverSelectable => _currentBaseObject != null;
    }
}
