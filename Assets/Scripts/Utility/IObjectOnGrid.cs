using UnityEngine;

namespace ProjectDiorama
{
    public interface IObjectOnGrid
    {
        /// <summary> Called the frame that base object is rotated.  To be used to offset placement of object due to rotation.</summary>
        /// <param name="dir"></param>
        public void OnRotate(RotationDirection dir);
        /// <summary> Used to retrieve the object settings for the active base grid object.</summary>
        public ObjectSettings GetSettings();
        /// <summary> Used to retrieve the transform for the active base grid object.</summary>
        public Transform GetTransform();
        /// <summary> Used to retrieve the footprint size for the active base grid object.</summary>
        public Vector2 FootprintSize();
    }
}