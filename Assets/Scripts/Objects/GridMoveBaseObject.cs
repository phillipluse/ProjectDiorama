using UnityEngine;

namespace ProjectDiorama
{
    /// <summary> Handles grid based movement of Base Object </summary>
    public class GridMoveBaseObject : BaseObject
    {
        Vector3 _tempGridWorldPosition;
        Vector3 _placedGridWorldPosition;
        public override void Init(Vector3 position)
        {
            base.Init(position);
            _tempGridWorldPosition = GetGridWorldPosition(position);
            SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            
            _placedGridWorldPosition = _tempGridWorldPosition;
            RemoveFromGrid(_placedGridWorldPosition);
        }

        public override bool TryToPlaceObject()
        {
            if (!base.TryToPlaceObject()) return false;
            
            MoveTo(_tempGridWorldPosition);
            AddToGrid(_tempGridWorldPosition);
            return true;
        }

        protected override void Move(Vector3 position)
        {
            Vector3 newPosition;
            const float factor = 20.0f;
            
            if (IsOnGrid(position))
            {
                _tempGridWorldPosition = GetGridWorldPosition(position);
                SetState(ObjectCanBePlacedAtPosition(position) ? ObjectState.Normal : ObjectState.Warning);
                newPosition = Vector3.Lerp(transform.position, _tempGridWorldPosition, Time.deltaTime * factor);
            }
            else
            {
                _tempGridWorldPosition = position;
                SetState(ObjectState.Warning);
                newPosition = position;
            }
            
            MoveTo(newPosition);
        }

        protected override void MoveBackToStartPositionAndRotation()
        {
            _tempGridWorldPosition = _placedGridWorldPosition;
            MoveTo(_placedGridWorldPosition);
            AddToGrid(_placedGridWorldPosition);
            
            base.MoveBackToStartPositionAndRotation();
        }
        
        bool ObjectCanBePlacedAtPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.CanPlaceObjectAtPosition(position, ObjectOnGrid.GetSettings());
        }

        void AddToGrid(Vector3 worldPosition)
        {
            GameWorld.ActiveGrid.AddObjectToGrid(worldPosition, ObjectOnGrid.GetSettings());
        }

        void RemoveFromGrid(Vector3 worldPosition)
        {
            GameWorld.ActiveGrid.RemoveObjectFromGrid(worldPosition, ObjectOnGrid.GetSettings());
        }

        static bool IsOnGrid(Vector3 position)
        {
            return GameWorld.ActiveGrid.IsPositionOnGrid(position);
        }
        
        static Vector3 GetGridWorldPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.GetGridWorldPosition(position);
        }
    }
}
