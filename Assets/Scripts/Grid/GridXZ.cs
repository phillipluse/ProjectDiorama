using UnityEngine;

namespace ProjectDiorama
{
    public class GridXZ<TGridObject>
    {
        TGridObject[,] _gridObjectArray;
        readonly int _width;
        readonly int _length;
        readonly float _cellSize;
        TGridObject _nullGridObject;
        
        public GridXZ(int width, int length, float cellSize, TGridObject gridObject)
        {
            _width = width;
            _length = length;
            _cellSize = cellSize;
            _gridObjectArray = new TGridObject[width, length];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    _gridObjectArray[x, z] = gridObject;
                }
            }
        }
        
        public void UpdateGridArrayOnTilePlaced(Vector3 worldPosition, TGridObject gridObject)
        {
            var gridPosition = GetGridPosition(worldPosition);
            _gridObjectArray[gridPosition.X, gridPosition.Z] = gridObject;
        }
        
        public void UpdateGridArrayOnTilePlaced(GridPositionXZ gridPositionXZ, TGridObject gridObject)
        {
            _gridObjectArray[gridPositionXZ.X, gridPositionXZ.Z] = gridObject;
        }

        public Vector3 GetWorldPosition(GridPositionXZ gridPositionXZ)
        {
            return new Vector3(gridPositionXZ.X, 0.0f, gridPositionXZ.Z) * _cellSize;
        }

        public GridPositionXZ GetGridPosition(Vector3 worldPosition)
        {
            return new GridPositionXZ
            (
                Mathf.FloorToInt(worldPosition.x / _cellSize),
                Mathf.FloorToInt(worldPosition.z / _cellSize)
            );
        }

        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            var gridPosition = GetGridPosition(worldPosition);
            return IsValidGridPosition(gridPosition) 
                ? _gridObjectArray[gridPosition.X, gridPosition.Z] 
                : _nullGridObject;
        }
        
        public TGridObject GetGridObject(GridPositionXZ gridPositionXZ)
        {
            return IsValidGridPosition(gridPositionXZ) 
                ? _gridObjectArray[gridPositionXZ.X, gridPositionXZ.Z] 
                : _nullGridObject;
        }
        
        public bool IsValidGridPosition(Vector3 worldPosition)
        {
            return worldPosition.x >= 0.0f && 
                   worldPosition.z >= 0.0f && 
                   worldPosition.x < _width && 
                   worldPosition.z < _length;
        }

        public bool IsValidGridPosition(GridPositionXZ gridPositionXZ)
        {
            return gridPositionXZ.X >= 0.0f && 
                   gridPositionXZ.Z >= 0.0f && 
                   gridPositionXZ.X < _width && 
                   gridPositionXZ.Z < _length;
        }
    }
}
