using UnityEngine;

namespace ProjectDiorama
{
    /// <summary> Handles free movement of Base Object </summary>
    public class FreeMoveBaseObject : BaseObject, ISnap
    {
        Vector3 _tempWorldPosition;
        Vector3 _placedWorldPosition;
        
        public override void OnSelected()
        {
            base.OnSelected();
            
            _placedWorldPosition = _tempWorldPosition;
        }

        protected override void Move(Vector3 position)
        {
            if (CurrentState.IsSnapped()) return;
            _tempWorldPosition = position;
            MoveTo(position);
        }
        
        protected override void MoveBackToStartPositionAndRotation()
        {
            base.MoveBackToStartPositionAndRotation();
            
            _tempWorldPosition = _placedWorldPosition;
            MoveTo(_placedWorldPosition);
        }

        public void Snap(Transform toTransform, Transform fromTransform, Transform currentTransform)
        {
            if (IsRotating)
            {
                StopRotation();
                RotateToTarget();
            }
            
            var toWorldPosition = toTransform.position;
            var fromWorldPosition = fromTransform.position;
            var currentWorldPosition = currentTransform.position;
            
            var position = DistanceToMove(toWorldPosition, fromWorldPosition, currentWorldPosition);
            MoveTo(position);
            SetState(ObjectState.Snapped); 
            
            foreach (IBaseObjectModule module in ObjectModules) 
            {
               if (module is ISnap s)
               {
                   s.Snap(toTransform, fromTransform, currentTransform);
               }
            }
        }

        public void UnSnap()
        {
            SetState(ObjectState.None);
            foreach (IBaseObjectModule module in ObjectModules)
            {
                if (module is ISnap s)
                {
                    s.UnSnap();
                }
            }
        }
        
        Vector3 DistanceToMove(Vector3 toWorldPosition, Vector3 fromWorldPosition, Vector3 currentWorldPosition)
        {
            var distanceToMove = toWorldPosition - fromWorldPosition;
            return currentWorldPosition + distanceToMove;
        }
    }
}
