using UnityEngine;

namespace ProjectDiorama
{
    public class CameraTarget : MonoBehaviour
    {
        [Header("Move Properties")]
        [SerializeField] float _moveSpeed;
        [SerializeField] float _moveSmoothTime;

        [Header("Rotate Properties")]
        [SerializeField] float _rotateSpeed;
        [SerializeField] float _rotateSmoothTime;
        
        Vector3 _moveVelocity = Vector3.zero;
        float _rotationSpeed;
        float _rotateVelocity;
        
        public void Move(Vector2 direction)
        {
            var t = transform;
            var forward = t.forward * (direction.y * _moveSpeed);
            var right = t.right * (direction.x * _moveSpeed);
            var targetPosition = t.position + forward + right;
            
            var newPosition = Vector3.SmoothDamp(t.position, targetPosition, ref _moveVelocity, _moveSmoothTime);
            
            // newPosition.x = Mathf.Clamp(newPosition.x, 0.0f, GameWorld.ActiveGrid.GridWidth);
            // newPosition.z = Mathf.Clamp(newPosition.z, 0.0f, GameWorld.ActiveGrid.GridHeight);
            
            MoveTo(newPosition);
        }

        public void RotateYAxis(float direction)
        {
            var adjustedSpeed = _rotateSpeed * -direction;
            _rotationSpeed = Mathf.SmoothDamp(_rotationSpeed, adjustedSpeed, 
                ref _rotateVelocity, _rotateSmoothTime);
            
            var euler = new Vector3(0.0f, _rotationSpeed, 0.0f);
            var newRotation = Quaternion.Euler(euler);
            
            transform.rotation *= newRotation;
        }

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}
