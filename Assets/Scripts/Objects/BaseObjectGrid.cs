using UnityEngine;

namespace ProjectDiorama
{
    public class BaseObjectGrid : BaseObject
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
            RemoveFromGrid();
        }

        public override bool TryToPlaceObject()
        {
            if (!base.TryToPlaceObject()) return false;
            
            MoveTo(_tempGridWorldPosition);
            AddToGrid(_tempGridWorldPosition);
            return true;
        }
        
        public override void Move(Vector3 position)
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
            base.MoveBackToStartPositionAndRotation();
            
            _tempGridWorldPosition = _placedGridWorldPosition;
            MoveTo(_placedGridWorldPosition);
            AddToGrid(_placedGridWorldPosition);
        }
        
        bool ObjectCanBePlacedAtPosition(Vector3 position)
        {
            return GameWorld.ActiveGrid.CanPlaceObjectAtPosition(position, Selectable.GetSettings());
        }

        void AddToGrid(Vector3 worldPosition)
        {
            GameWorld.ActiveGrid.AddObjectToGrid(worldPosition, Selectable.GetSettings());
        }

        void RemoveFromGrid()
        {
            GameWorld.ActiveGrid.RemoveObjectFromGrid(_tempGridWorldPosition, Selectable.GetSettings());
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
