using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObjectNonGrid : BaseObject
    {
        Vector3 _tempWorldPosition;
        Vector3 _placedWorldPosition;
        
        public override void OnSelected()
        {
            base.OnSelected();
            
            _placedWorldPosition = _tempWorldPosition;
        }
        
        public override void Move(Vector3 position)
        {
            _tempWorldPosition = position;
            transform.position = position;
        }
        protected override void MoveBackToStartPositionAndRotation()
        {
            base.MoveBackToStartPositionAndRotation();
            
            _tempWorldPosition = _placedWorldPosition;
            MoveTo(_placedWorldPosition);
        }
    }
}
