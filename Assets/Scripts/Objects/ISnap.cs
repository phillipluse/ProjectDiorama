using UnityEngine;

namespace ProjectDiorama
{
    public interface ISnap
    {
        public void Snap(Vector3 worldPosition);
        public void UnSnap();
    }
}