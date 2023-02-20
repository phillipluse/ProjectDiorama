using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObjectNonGrid : BaseObject, ISnap
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

        public void Snap(Vector3 worldPosition)
        {
           MoveTo(worldPosition);
           SetState(ObjectState.Snapped);

           foreach (IBaseObjectModule module in ObjectModules)
           {
               if (module is ISnap s)
               {
                   s.Snap(worldPosition);
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
    }
}
