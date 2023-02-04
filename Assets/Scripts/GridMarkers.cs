using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class GridMarkers : MonoBehaviour
    {
        [SerializeField] GameObject _markerPrefab;

        GameObject _marker;

        void Awake()
        {
            Events.AnyObjectInitializedEvent += OnObjectSelected;
            Events.AnyObjectSelectedEvent += OnObjectSelected;
            Events.AnyObjectPlacedEvent += OnObjectPlaced;
        }

        void OnObjectSelected(BaseObject baseObject)
        {
            //parent to this base object
            //Get gridpositions of base object
            //Add a marker at each position
            //Show marker

            transform.parent = baseObject.Selectable.GetTransform();
            transform.localPosition = Vector3.zero;
            _marker = Instantiate(_markerPrefab, transform, false);
            // _marker.transform.localPosition = Vector3.zero;
        }

        void OnObjectPlaced(BaseObject baseObject)
        {
            transform.parent = null;
            _marker.SetActive(false);
        }
        
    }
}
