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
        [SerializeField] float _scaleFactor;

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
            // Debug.Log($"{_zoomAmountAdd}");
            // var currentZoomAmount = _virtualCamera.m_Lens.FieldOfView;
            // var newZoomAmount = currentZoomAmount + _zoomAmountAdd;
            // newZoomAmount = Mathf.Clamp(newZoomAmount, _minZoom, _maxZoom);


            // if (Mathf.Abs(_zoomAmountAdd) <= 0.00001f) return;
            if (scrollValue == 0) return;

            var isZoomIn = Mathf.Sign(scrollValue) == -1;
            var fov = _virtualCamera.m_Lens.FieldOfView;
            var scaleFactor = _scaleFactor;
            Debug.Log($"Scale Factor: {scaleFactor}");

            var playerPosition = GameWorld.PlayerPosition;
            Debug.Log($"Player Position: {playerPosition}");
            
            var currentLength = _cameraTarget.transform.position - playerPosition;
            Debug.Log($"Old Length2: {currentLength}");

            var newFOV = 0.0f;
            Vector3 newLength;
            if (isZoomIn)
            {
                newLength = currentLength / (scaleFactor);
                newFOV = fov /= scaleFactor;
            }
            else
            {
                newLength = currentLength * scaleFactor;
                newFOV = fov *= scaleFactor;
            }
            Debug.Log($"New Length: {newLength}");

            newFOV = Mathf.Clamp(newFOV, _minZoom, _maxZoom);

            var deltaLength = 0.0f;
            if (newFOV != _minZoom && newFOV != _maxZoom && isZoomIn)
            {
                deltaLength = Vector3.Distance(currentLength,newLength);
            }
            
            Debug.Log($"Delta: {deltaLength}");
            
            var v = Vector3.MoveTowards(_cameraTarget.transform.position, playerPosition, deltaLength);
            Debug.Log($"New Position: {v}");
            _cameraTarget.transform.position = v;

            _virtualCamera.m_Lens.FieldOfView = newFOV;
        }

        void OnObjectSelected(BaseObject baseObject)
        {
            
        }
    }
}
