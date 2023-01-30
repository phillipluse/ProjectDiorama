using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ProjectDiorama
{
    public static class MousePosition
    {
        static Camera _camera;
        static RaycastHit _hit;
        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        const float MAX_DISTANCE = 100.0f;
        
        public static Vector3 GetScreenToWorldPoint()
        {
            var mouseScreenPosition = ScreenPosition;
            var v = new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.nearClipPlane);
            return Camera.ScreenToWorldPoint(v);
        }
        
        public static bool IsOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        
        public static bool IsWithinGameWindow()
        {
            var screenRect = new Rect(0,0, Screen.width, Screen.height);
            return screenRect.Contains(ScreenPosition);
        }
        
        public static bool IsPositionOverLayerMask(LayerMask layerMask, out RaycastHit hit)
        {
            var ray = Camera.ScreenPointToRay(ScreenPosition);
            if (Physics.Raycast(ray, out hit, MAX_DISTANCE, layerMask))
            {
                return true;
            }

            return false;
        }

        public static RaycastHit MousePositionRaycastHit()
        {
            var ray = Camera.ScreenPointToRay(ScreenPosition);
            return Physics.Raycast(ray, out RaycastHit newHit, MAX_DISTANCE) ? newHit : new RaycastHit();
        }
        
        public static Vector2 ScreenPosition => Mouse.current.position.ReadValue();

        static bool IsAnyObjectHitThisFrame => _hit.collider != null;
    }
}
