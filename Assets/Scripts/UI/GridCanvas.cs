using UnityEngine;

namespace ProjectDiorama
{
    public class GridCanvas : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;

        void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public bool IsVisible => _canvas.enabled;
    }
}
