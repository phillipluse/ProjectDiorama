using UnityEngine;
using Cinemachine;

namespace ProjectDiorama
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")] [SerializeField] CameraTarget _cameraTarget;
        [SerializeField] CinemachineVirtualCamera _virtualCamera;

        [Header("Elevation Properties")] 
        [SerializeField] [Range(11, 20)] float _maxElevation;
        [SerializeField] [Range(0, 10)] float _minElevation;
        [SerializeField] float _elevationChangeSpeed;
        [SerializeField] float _elevationSmoothTime;

        [Header("Zoom Properties")] 
        [SerializeField] float _maxZoom;
        [SerializeField] float _minZoom;
        [SerializeField] float _zoomSmoothTime;
        [SerializeField] [Range(1.0f, 2.0f)] float _scaleFactor;

        CinemachineTransposer _transposer;
        Vector3 _newLength;
        float _elevationChangeAdd;
        float _elevationChangeVelocity;
        float _zoomAmountAdd;
        float _zoomVelocity;
        float _fovToAdd;
        float _fovVelocity;
        float _deltaLength;
        float _deltaLengthVelocity;

        void Awake()
        {
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        public void SetInput(ref CameraFrameInput input)
        {
            var currentFOV = _virtualCamera.m_Lens.FieldOfView;
            _cameraTarget.Move(input.PanMovement, currentFOV, _maxZoom);
            _cameraTarget.RotateYAxis(input.RotationMovement.x);
            
            ElevationChange(input.RotationMovement.y);
            Zoom(input.ScrollValue);
        }

        void ElevationChange(float directionInput)
        {
            if (directionInput.IsApproximateTo(0.0f) && 
                _elevationChangeAdd.IsApproximateTo(0.0f))
            {
                return;
            }
            
            //directions switch?
            if (directionInput != 0.0f)
            {
                if (directionInput.Sign() != _elevationChangeAdd.Sign())
                {
                    _elevationChangeAdd = 0.0f;
                    _elevationChangeVelocity = 0.0f;
                }
            }

            var adjustedSpeed = _elevationChangeSpeed * directionInput;
            _elevationChangeAdd = Mathf.SmoothDamp(_elevationChangeAdd, adjustedSpeed,
                ref _elevationChangeVelocity, _elevationSmoothTime);

            const float tolerance = 0.0001f;
            if (_elevationChangeAdd.Abs() < tolerance)
            {
                _elevationChangeAdd = 0.0f;
                _elevationChangeVelocity = 0.0f;
                return;
            }

            var currentElevation = _transposer.m_FollowOffset.y;
            var newElevation = currentElevation + _elevationChangeAdd * Time.deltaTime;
            newElevation = newElevation.Clamp(_minElevation, _maxElevation);

            _transposer.m_FollowOffset.y = newElevation;
        }

        void Zoom(float scrollInput)
        {
            if (scrollInput.IsApproximateTo(0.0f) &&
                _fovToAdd.IsApproximateTo(0.0f))
            {
                return;
            }
            
            //directions switch?
            if (scrollInput != 0.0f)
            {
                if (scrollInput.Sign() != _fovToAdd.Sign())
                {
                    ResetZoomParameters();
                }
            }
            
            const float tolerance = 0.0001f;
            var currentFOV = _virtualCamera.m_Lens.FieldOfView;
            _fovToAdd = GetFOVToAdd(scrollInput, currentFOV);

            if (_fovToAdd.Abs() < tolerance)
            {
                ResetZoomParameters();
                return;
            }
            
            var newFOV = currentFOV + _fovToAdd;
            newFOV = newFOV.Clamp(_minZoom, _maxZoom);
            if (IsAtFOVBoundary(newFOV))
            {
                ResetZoomParameters();
                return;
            }

            _virtualCamera.m_Lens.FieldOfView = newFOV;

            if (_cameraTarget.IsPanning)
            {
                _deltaLength = 0.0f;
                _deltaLengthVelocity = 0.0f;
                return;
            }
            
            var newPosition = GetPositionTowardsZoomPoint(scrollInput);

            if (_deltaLength.Abs() < tolerance)
            {
                _deltaLength = 0.0f;
                _deltaLengthVelocity = 0.0f;
                return;
            }
            
            _cameraTarget.MoveTo(newPosition);
        }

        float GetFOVToAdd(float scrollInput, float currentFOV)
        {
            var isZoomInInput = Mathf.Sign(scrollInput) == -1;
            float tempFOV;

            if (isZoomInInput) tempFOV = currentFOV / _scaleFactor;
            else tempFOV = currentFOV * _scaleFactor;

            var deltaFOV = currentFOV - tempFOV;
            var adjustedTargetAdd = deltaFOV.Abs() * scrollInput;
            
            return Mathf.SmoothDamp(_fovToAdd, adjustedTargetAdd, ref _fovVelocity, _zoomSmoothTime);
        }

        Vector3 GetPositionTowardsZoomPoint(float scrollInput)
        {
            var playerPosition = GameWorld.PlayerPosition;
            var currentLength = _cameraTarget.transform.position - playerPosition;

            Vector3 newLength;

            if (IsZoomingIn()) newLength = currentLength / _scaleFactor;
            else newLength = currentLength * _scaleFactor;

            var adjustedDeltaLengthTarget = Vector3.Distance(currentLength, newLength) * scrollInput;

            _deltaLength = Mathf.SmoothDamp(_deltaLength, adjustedDeltaLengthTarget.Abs(), ref _deltaLengthVelocity,
                _zoomSmoothTime);

            Vector3 targetPosition = playerPosition + newLength;
            var newPosition = Vector3.MoveTowards(_cameraTarget.transform.position, targetPosition, _deltaLength);
            newPosition.y = 0.0f;
            return newPosition;
        }

        bool IsZoomingIn()
        {
            return _fovToAdd.Sign() == -1;
        }

        bool IsAtFOVBoundary(float newFOV)
        {
            var tolerance = 0.001f;
            return newFOV.IsEqualWithTolerance(_minZoom, tolerance) || 
                   newFOV.IsEqualWithTolerance(_maxZoom, tolerance);
        }
        
        void ResetZoomParameters()
        {
            _fovToAdd = 0.0f;
            _fovVelocity = 0.0f;
            _deltaLength = 0.0f;
            _deltaLengthVelocity = 0.0f;
        }
    }
}
