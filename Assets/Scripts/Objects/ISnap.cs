using UnityEngine;

namespace ProjectDiorama
{
    public interface ISnap
    {
        public void Snap(Transform toTransform, Transform fromTransform, Transform currentTransform);
        public void UnSnap();
    }
}