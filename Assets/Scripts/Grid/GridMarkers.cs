using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class GridMarkers : MonoBehaviour
    {
        [SerializeField] GridMarker _marker;

        void OnEnable()
        {
            Events.AnyObjectInitializedEvent += Init;
            Events.AnyObjectSelectedEvent += Init;
            Events.AnyObjectPlacedEvent += Release;
            Events.AnyObjectDeSelectedEvent += Release;
            Events.AnyObjectDeletedEvent += Release;
        }

        void OnDisable()
        {
            Events.AnyObjectInitializedEvent -= Init;
            Events.AnyObjectSelectedEvent -= Init;
            Events.AnyObjectPlacedEvent -= Release;
            Events.AnyObjectDeSelectedEvent -= Release;
            Events.AnyObjectDeletedEvent -= Release;
        }

        void Init(BaseObject baseObject)
        {
            if (baseObject is not BaseObjectGrid) return;
            var t = transform;
            var selectableT = baseObject.Selectable.GetTransform();

            t.parent = selectableT;

            var footPrintSize = baseObject.Selectable.FootprintSize();
            var yPos = 0.0f;
            var pos = new Vector3(-footPrintSize.x / 2, yPos, -footPrintSize.y / 2);
            Quaternion rot = new Quaternion().Zero();

            t.SetLocalPositionAndRotation(pos, rot);
            baseObject.StateChange += OnBaseObjectStateChange;
            
            _marker.Init(baseObject);
            _marker.Show();
        }

        void Release(BaseObject baseObject)
        {
            baseObject.StateChange -= OnBaseObjectStateChange;
            transform.parent = null;
            _marker.Hide();
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        void OnBaseObjectStateChange(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.None: _marker.SetNormal(); 
                    break;
                case ObjectState.Normal: _marker.SetNormal(); 
                    break;
                case ObjectState.Warning: _marker.SetWarning();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
