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
        
        /// <summary>
        /// Returns world position of mouse at camera near clip plane.
        /// </summary>
        /// <returns></returns>
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
        
        public static Vector2 GetScreenSizeInWorldCoords()
        {
            var width = 0.0f;
            var height = 0.0f;

            var clipPlane = Camera.nearClipPlane;

            var p1 = Camera.ViewportToWorldPoint(new Vector3(0, 0, clipPlane));
            var p2 = Camera.ViewportToWorldPoint(new Vector3(1, 0, clipPlane));
            var p3 = Camera.ViewportToWorldPoint(new Vector3(1, 1, clipPlane));

            width = (p2 - p1).magnitude;
            height = (p3 - p2).magnitude;

            Debug.Log($"{width}, {height}");
            return new Vector2(width, height);
        }
        
        public static Vector2 ScreenPosition => Mouse.current.position.ReadValue();

        static bool IsAnyObjectHitThisFrame => _hit.collider != null;
    }
}
