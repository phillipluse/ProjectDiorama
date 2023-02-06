using System;
using UnityEngine;
using Cinemachine;

namespace ProjectDiorama
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CameraTarget _cameraTarget;
        [SerializeField] CinemachineVirtualCamera _virtualCamera;

        [Header("Elevation Properties")]
        [SerializeField][Range(11,20)] float _maxElevation;
        [SerializeField][Range(0,10)] float  _minElevation;
        [SerializeField] float _elevationChangeSpeed;
        [SerializeField] float _elevationSmoothTime;

        [Header("Zoom Properties")]
        [SerializeField] float _maxZoom;
        [SerializeField] float _minZoom;
        [SerializeField] float _zoomMaxSpeed;
        [SerializeField] float _zoomSmoothTime;

        CinemachineTransposer _transposer;
        float _elevationChangeVelocity;
        float _elevationChangeAdd;
        float _zoomVelocity;
        float _zoomAmountAdd;

        void Awake()
        {
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        void OnEnable()
        {
            Events.AnyObjectSelectedEvent += OnObjectSelected;
        }

        public void SetInput(ref CameraFrameInput input)
        {
           // _cameraTarget.Move(input.PanMovement);
           _cameraTarget.RotateYAxis(input.RotationMovement.x);
           ElevationChange(input.RotationMovement.y);
           Zoom(input.ScrollValue);
        }

        void ElevationChange(float direction)
        {
            var adjustedSpeed = _elevationChangeSpeed * direction;
            _elevationChangeAdd = Mathf.SmoothDamp(_elevationChangeAdd, adjustedSpeed, 
                ref _elevationChangeVelocity, _elevationSmoothTime);
            
            var currentElevation = _transposer.m_FollowOffset.y;
            var newElevation = currentElevation + _elevationChangeAdd;
            newElevation = Mathf.Clamp(newElevation, _minElevation, _maxElevation);
            
            _transposer.m_FollowOffset.y = newElevation;
        }

        void Zoom(float scrollValue)
        {
            // var adjustedSpeed = _zoomMaxSpeed * scrollValue;
            // _zoomAmountAdd = Mathf.SmoothDamp(_zoomAmountAdd, adjustedSpeed, ref _zoomVelocity, _zoomSmoothTime);
            //
            // var currentZoomAmount = _virtualCamera.m_Lens.FieldOfView;
            // var newZoomAmount = currentZoomAmount + _zoomAmountAdd;
            // newZoomAmount = Mathf.Clamp(newZoomAmount, _minZoom, _maxZoom);


            // if (Mathf.Abs(_zoomAmountAdd) <= 0.00001f) return;
            if (scrollValue == 0) return;
            var fov = _virtualCamera.m_Lens.FieldOfView;
            var scaleFactor = fov / (fov - 1);
            
            var oldLength = MousePosition.GetScreenToWorldPoint() - MousePosition.Camera.transform.position;
            var newLength = oldLength / scaleFactor;
            var deltaLength = oldLength - newLength;
            deltaLength.y = 0;
            Debug.Log($"{deltaLength}");
            _cameraTarget.transform.position -= deltaLength;

            // _cameraTarget.transform.position += direction;

            _virtualCamera.m_Lens.FieldOfView -= 1;
            // _virtualCamera.m_Lens.FieldOfView = newZoomAmount;
        }

        void OnObjectSelected(BaseObject baseObject)
        {
            
        }
    }
}
