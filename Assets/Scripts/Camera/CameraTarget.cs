using UnityEngine;

namespace ProjectDiorama
{
    public class CameraTarget : MonoBehaviour
    {
        [Header("Move Properties")]
        [SerializeField] float _moveSpeed;
        [SerializeField] float _moveSmoothTime;
        [Tooltip("How far camera will pan in the X direction off of grid")]
        [SerializeField] int _borderWidth;
        [Tooltip("How far camera will pan in the Z direction off of grid")]
        [SerializeField] int _borderHeight;

        [Header("Rotate Properties")]
        [SerializeField] float _rotateSpeed;
        [SerializeField] float _rotateSmoothTime;
        
        Vector3 _moveVelocity = Vector3.zero;
        float _rotationAmount;
        float _rotateVelocity;
        
        public void Move(Vector2 directionInput, float currentFOV, float maxFOV)
        {
            if(_moveVelocity.IsApproximateTo(Vector3.zero) && directionInput.IsApproximateTo(Vector2.zero)) return;
            
            var multiplier = currentFOV / maxFOV;
            var speed = _moveSpeed * multiplier;
            
            var t = transform;
            var forward = t.forward * (speed * directionInput.y);
            var right = t.right * (speed * directionInput.x);
            var targetPosition = t.position + forward + right;
            
            var newPosition = Vector3.SmoothDamp(t.position, targetPosition, ref _moveVelocity, _moveSmoothTime);
            newPosition.y = 0.0f;

            var delta = Vector3.Distance(newPosition, targetPosition);
            const float tolerance = 0.001f;
            if (delta.Abs() < tolerance)
            {
                _moveVelocity = Vector3.zero;
                return;
            }

            newPosition.x = newPosition.x.Clamp(0.0f - _borderWidth, GameWorld.ActiveGrid.GridWidth + _borderWidth);
            newPosition.z = newPosition.z.Clamp(0.0f - _borderHeight, GameWorld.ActiveGrid.GridHeight + _borderHeight);
            
            MoveTo(newPosition);
        }

        public void RotateYAxis(float directionInput)
        {
            if (directionInput.IsApproximateTo(0.0f) &&
                _rotateVelocity.IsApproximateTo(0.0f))
            {
                return;
            }
            
            //directions switch?
            if (directionInput != 0.0f)
            {
                if (directionInput.Sign() == _rotationAmount.Sign())
                {
                    _rotationAmount = 0.0f;
                    _rotateVelocity = 0.0f;
                }
            }
            
            var adjustedRotationAmount = _rotateSpeed * -directionInput;
            
            _rotationAmount = Mathf.SmoothDamp(_rotationAmount, adjustedRotationAmount, 
                ref _rotateVelocity, _rotateSmoothTime);

            const float tolerance = 0.0001f;
            if (_rotationAmount.Abs() < tolerance)
            {
                _rotationAmount = 0.0f;
                _rotateVelocity = 0.0f;
                return;
            }

            var t = transform;
            var euler = new Vector3(0.0f, _rotationAmount * Time.deltaTime, 0.0f);
            var newRotation = t.TransformRotation(Quaternion.Euler(euler));
            t.rotation = newRotation;
        }

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public bool IsPanning => !_moveVelocity.IsApproximateTo(Vector3.zero);
    }
}
