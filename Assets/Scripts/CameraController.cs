using System;
using UnityEngine;
using Cinemachine;

namespace ProjectDiorama
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        CameraTarget _cameraTarget;

        [SerializeField] CinemachineVirtualCamera _virtualCamera;

        [Header("Elevation Properties")] [SerializeField] [Range(11, 20)]
        float _maxElevation;

        [SerializeField] [Range(0, 10)] float _minElevation;
        [SerializeField] float _elevationChangeSpeed;
        [SerializeField] float _elevationSmoothTime;

        [Header("Zoom Properties")] [SerializeField]
        float _maxZoom;

        [SerializeField] float _minZoom;
        [SerializeField] float _zoomMaxSpeed;
        [SerializeField] float _zoomSmoothTime;
        [SerializeField] float _scaleFactor;

        CinemachineTransposer _transposer;
        float _elevationChangeVelocity;
        float _elevationChangeAdd;
        float _zoomVelocity;
        float _zoomAmountAdd;

        float _fovToAdd;
        float _fovVelocity;

        float _deltaLength;
        Vector3 _newLength;
        float _deltaLengthVelocity;

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
            var newFOV = GetFOV(scrollValue);
            
            if (Mathf.Abs(_fovToAdd) < 0.0001f)
            {
                _fovToAdd = 0.0f;
                _fovVelocity = 0.0f;
                _deltaLength = 0.0f;
                _deltaLengthVelocity = 0.0f;
                return;
            }
            
            newFOV = Mathf.Clamp(newFOV, _minZoom, _maxZoom);
            
            if (IsAtFOVBoundary(newFOV)) return;

            var newPosition = GetPositionTowardsZoomPoint(scrollValue);
            _cameraTarget.transform.position = newPosition;
            _virtualCamera.m_Lens.FieldOfView = newFOV;
        }

        float GetFOV(float scrollValue)
        {
            var isZoomInInput = Mathf.Sign(scrollValue) == -1;
            var fov = _virtualCamera.m_Lens.FieldOfView;
            float tempFOV;
            
            if (isZoomInInput) tempFOV = fov / _scaleFactor;
            else tempFOV = fov * _scaleFactor;
            
            var adjustedTargetAdd = Mathf.Abs(fov - tempFOV) * scrollValue;
            _fovToAdd = Mathf.SmoothDamp(_fovToAdd, adjustedTargetAdd, ref _fovVelocity, _zoomSmoothTime);
            return fov + _fovToAdd;
        }

        Vector3 GetPositionTowardsZoomPoint(float scrollValue)
        {
            var playerPosition = GameWorld.PlayerPosition;
            var currentLength = _cameraTarget.transform.position - playerPosition;

            Vector3 newLength;
            Vector3 targetPosition;

            if (IsZoomingIn())
            {
                newLength = currentLength / _scaleFactor;
                targetPosition = playerPosition + newLength;
            }
            else
            {
                newLength = currentLength * _scaleFactor;
                targetPosition = playerPosition + newLength;
            }
            
            var adjustedDeltaLengthTarget = Mathf.Abs(Vector3.Distance(currentLength, newLength) * scrollValue);

            _deltaLength = Mathf.SmoothDamp(_deltaLength, adjustedDeltaLengthTarget, ref _deltaLengthVelocity,
                _zoomSmoothTime);
            
            var newPosition = Vector3.MoveTowards(_cameraTarget.transform.position, targetPosition, _deltaLength);
            newPosition.y = 0.0f;
            return newPosition;
        }

        bool IsZoomingIn()
        {
            return Mathf.Sign(_fovToAdd) == -1;
        }

        bool IsAtFOVBoundary(float newFOV)
        {
            return newFOV == _minZoom || newFOV == _maxZoom;
        }

        void OnObjectSelected(BaseObject baseObject)
        {
            
        }
    }
}
