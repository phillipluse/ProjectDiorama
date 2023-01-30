using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ProjectDiorama
{
    public class GridHandler : MonoBehaviour
    {
        public static Action<ObjectSettings, List<GridPositionXZ>> ObjectSetToGrid;

        [Header("References")]
        [SerializeField] Logger _logger;
        
        [Header("Properties")]
        [SerializeField] int _gridWidth;
        [SerializeField] int _gridLength;
        [SerializeField] int _cellSize = 1;

        PlacedGridObjects _placedGridObjects;
        GridXZ<GridObject> _grid;
        GridObject _emptyGridObject;
        GridObject _occupiedGridObject;
        GridObject _noneGridObject;
        GridCheck _gridCheck;
        
        void Awake()
        {
            _emptyGridObject = new GridObject(GridObjectState.Empty);
            _occupiedGridObject = new GridObject(GridObjectState.Occupied);
            _noneGridObject = new GridObject(GridObjectState.None);
            _grid = new GridXZ<GridObject>(_gridWidth, _gridLength, _cellSize, _emptyGridObject);
            _placedGridObjects = new PlacedGridObjects();
            _gridCheck = new GridCheck(this);
        }

        public void SetObjectToGrid(Vector3 worldPosition, ObjectSettings settings)
        {
            var startPosition = GetGridPosition(worldPosition);
            var placedTileList = ListPool<GridPositionXZ>.Get();
            
            if (!settings.IsObjectSingleTile)
            {
                var positions = ListPool<GridPositionXZ>.Get();
                positions.GetAllPositions(settings.RotatedSize, startPosition, settings.Directions);

                foreach (GridPositionXZ position in positions)
                {
                    AddGridObjectToGridArray(position, _occupiedGridObject);
                    placedTileList.Add(position);
                }
                
                ListPool<GridPositionXZ>.Release(positions);
            }
            else
            {
                AddGridObjectToGridArray(startPosition, _occupiedGridObject);
                placedTileList.Add(startPosition);
            }

            // Log($"Tile Set To Grid");
            ObjectSetToGrid?.Invoke(settings, placedTileList);
            AddToPlacedTiles(placedTileList);
            ListPool<GridPositionXZ>.Release(placedTileList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool CanPlaceObjectAtPosition(Vector3 worldPosition, ObjectSettings settings)
        {
            if (!IsPositionOnGrid(worldPosition)) return false;
            return settings.IsObjectSingleTile ? 
                _gridCheck.SingleTileCheck(worldPosition, settings) : 
                _gridCheck.MultipleTileCheck(worldPosition, settings);
        }

        /// <summary>
        /// Returns world position of the grid position.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        public Vector3 GetGridWorldPosition(Vector3 worldPosition)
        {
            var gridPosition = _grid.GetGridPosition(worldPosition);
            return _grid.GetWorldPosition(gridPosition);
        }
        
        /// <summary>
        /// Tries to get a grid object at position. Returns NoneGridObject if no grid object at position.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        public GridObject GetGridObject(Vector3 worldPosition)
        {
            var gridObject = _grid.GetGridObject(worldPosition);
            return gridObject ?? _noneGridObject;
        }
        
        /// <summary>
        /// Tries to get a grid object at position. Returns NoneGridObject if no grid object at position.
        /// </summary>
        /// <param name="gridPositionXZ"></param>
        /// <returns></returns>
        public GridObject GetGridObject(GridPositionXZ gridPositionXZ)
        {
            var gridObject = _grid.GetGridObject(gridPositionXZ);
            return gridObject ?? _noneGridObject;
        }

        public GridPositionXZ GetGridPosition(Vector3 worldPosition)
        {
            return _grid.GetGridPosition(worldPosition);
        }

        void AddGridObjectToGridArray(GridPositionXZ gridPositionXZ, GridObject gridObject)
        {
            _grid.UpdateGridArrayOnTilePlaced(gridPositionXZ, gridObject);
        }

        public bool IsPositionOnGrid(GridPositionXZ positionXZ)
        {
            return _grid.IsValidGridPosition(positionXZ);
        } 
        
        public bool IsPositionOnGrid(Vector3 worldPosition)
        {
            return _grid.IsValidGridPosition(worldPosition);
        }

        List<GridPositionXZ> RemovePositionsNotOnGrid(List<GridPositionXZ> neighborPositions)
        {
            for (int i = neighborPositions.Count - 1; i >= 0; i--)
            {
                var gp = neighborPositions[i];
                if (!_grid.IsValidGridPosition(gp))
                {
                    neighborPositions.Remove(gp);
                }
            }

            return neighborPositions;
        }
        
        void AddToPlacedTiles(List<GridPositionXZ> gridPositions)
        {
            foreach (var position in gridPositions)  
            {
                _placedGridObjects.Add(_grid.GetGridObject(position));
            }
        }
        
        void Log(object message)
        {
            _logger.Log(message);
        }
        
        public int GridObjectCount => _placedGridObjects.Count;
        public List<GridObject> PlacedGridObjects => _placedGridObjects.GridObjects();
        public int CellSize => _cellSize;

    }
}
