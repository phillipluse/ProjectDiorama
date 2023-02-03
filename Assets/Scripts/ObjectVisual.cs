using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectVisual : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] Outline _outline;
        
        [Header("Properties")]
        [SerializeField] Material _normalMaterial;
        [SerializeField] Material _warningMaterial;

        public void Init()
        {
            _outline.enabled = false;
        }

        public void OnHoverEnter()
        {
            _outline.enabled = true;
        }

        public void OnHoverExit()
        {
            _outline.enabled = false;
        }

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
