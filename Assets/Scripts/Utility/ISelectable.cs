using UnityEngine;

namespace ProjectDiorama
{
    public interface ISelectable
    {
        public BaseObject GetBaseObject();
        public ObjectSettings GetSettings();
        public Transform GetTransform();
        public Vector2 FootprintSize();
    }
}