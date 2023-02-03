using UnityEngine;

namespace ProjectDiorama
{
    public interface ISelectable
    {
        // public void OnHoverEnter();
        // public void OnHoverExit();
        // public void OnSelect();
        // public void OnDeSelect();
        // public bool TryToPlaceObject(Vector3 position);
        // public void Move(Vector3 position);
        // public void Rotate();

        public BaseObject GetBaseObject();
        
        public ObjectSettings GetSettings();
    }
}