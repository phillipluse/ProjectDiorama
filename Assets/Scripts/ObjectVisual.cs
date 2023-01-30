using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectVisual : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] MeshRenderer _meshRenderer;
        
        [Header("Properties")]
        [SerializeField] Material _normalMaterial;
        [SerializeField] Material _warningMaterial;

        public void SetToNormal()
        {
            _meshRenderer.material = _normalMaterial;
        }

        public void SetToWarning()
        {
            _meshRenderer.material = _warningMaterial;
        }
    }
}
