using UnityEngine;
using UnityEngine.Rendering;

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
        [SerializeField] Material _ghostMaterial;

        public void Init()
        {
            _outline.enabled = false;
            SetToGhost();
        }

        public void OnHoverEnter()
        {
            _outline.enabled = true;
        }

        public void OnHoverExit()
        {
            _outline.enabled = false;
        }

        public void OnSelect()
        {
            SetToGhost();
        }

        public void OnPlaced()
        {
            SetToNormal();
        }

        public void SetToNormal()
        {
            SetMaterial(_normalMaterial);
            ShadowOn();
        }

        public void SetToWarning()
        {
            SetMaterial(_warningMaterial);
        }

        public void SetToGhost()
        {
            SetMaterial(_ghostMaterial);
            ShadowOff();
        }

        void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        void ShadowOn()
        {
            _meshRenderer.shadowCastingMode = ShadowCastingMode.On;
        }

        void ShadowOff()
        {
            _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }
}
