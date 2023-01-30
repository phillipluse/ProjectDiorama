using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectSettings
    {
        public Vector2Int Directions { get; private set; }

        //x = width, y = length (Z axis)
        readonly DirectionOffsetSettings _directionOffsetSettings;
        readonly Vector3 _startObjectSize;
        readonly Vector2Int _footprintGridSize;

        Vector2Int _rotatedFootprintGridSize;

        public ObjectSettings(Vector3 objectSize, DirectionOffsetSettings offsetSettings)
        {
            _startObjectSize = objectSize;
            _directionOffsetSettings = offsetSettings;
            _footprintGridSize = CalculateFootprintGridSize();
            SettingsUpdate(RotationDirection.Up);
        }

        public void SettingsUpdate(RotationDirection dir)
        {
            _rotatedFootprintGridSize = SizePerRotation(dir);
            Directions = SetDirections(dir);
        }

        Vector2Int CalculateFootprintGridSize()
        {
            var cellSize = GameWorld.ActiveGridCellSize;
            var width = Mathf.CeilToInt(_startObjectSize.x / cellSize);
            var length = Mathf.CeilToInt(_startObjectSize.z / cellSize);
            return new Vector2Int(width, length);
        }
        
        public Vector3 GetObjectOffset(RotationDirection dir)
        {
            var cellSize = GameWorld.ActiveGridCellSize;
            return _directionOffsetSettings.ObjectOffset(_footprintGridSize, _startObjectSize.y, dir, Directions, cellSize);
        }
        
        Vector2Int SetDirections(RotationDirection dir)
        {
            return _directionOffsetSettings.DirectionsPerRotation(dir);
        }
        
        Vector2Int SizePerRotation(RotationDirection dir)
        {
            var startSize = _footprintGridSize;
            Vector2Int newSize = dir switch
            {
                RotationDirection.Up     => startSize,
                RotationDirection.Right  => new Vector2Int(startSize.y, startSize.x),
                RotationDirection.Down   => startSize,
                RotationDirection.Left   => new Vector2Int(startSize.y, startSize.x),
                _   => startSize
            };

            return newSize;
        }
        
        bool IsObjectSingleMultiRectangle()
        {
            if (IsObjectSquare) return false;
            return _footprintGridSize.x == 1 || _footprintGridSize.y == 1;
        }

        public Vector2Int RotatedSize => _rotatedFootprintGridSize;

        public GridObject GridObject = new GridObject(GridObjectState.Empty);
        public bool IsObjectSingleTile => _footprintGridSize.x == 1 && _footprintGridSize.y == 1;
        public bool IsObjectSquare => _footprintGridSize.x == _footprintGridSize.y;
        public bool IsObjectVertical => RotatedSize.x < RotatedSize.y;
        public FootprintOrientation FootprintOrientation => IsObjectVertical ? 
            FootprintOrientation.Vertical :
            FootprintOrientation.Horizontal;
    }
}