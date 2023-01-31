using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectSettings
    {
        public Vector3 Offset { get; private set; }

        readonly Vector3 _startObjectSize;
        readonly Vector2Int _footprintGridSize; //x = width, y = length (Z axis)

        Vector2Int _rotatedFootprintGridSize;
        
        public ObjectSettings(Vector3 objectSize)
        {
            _startObjectSize = objectSize;
            _footprintGridSize = CalculateFootprintGridSize();
            
            SettingsUpdate(RotationDirection.Up);
        }

        public void SettingsUpdate(RotationDirection dir)
        {
            _rotatedFootprintGridSize = SizePerRotation(dir);
            Offset = ObjectOffset(dir);
        }

        Vector3 ObjectOffset(RotationDirection dir)
        {
            float height = _startObjectSize.y / 2;
            float halfX = (float)_footprintGridSize.x / 2;
            float halfY = (float)_footprintGridSize.y / 2;
            const int posDir = 1;
            const int negDir = -1;
            
            return dir switch
            {
                RotationDirection.Up    => new Vector3(halfX * posDir, height, halfY * posDir),
                RotationDirection.Right => new Vector3(halfX * negDir, height, halfY * posDir),
                RotationDirection.Down  => new Vector3(halfX * negDir, height, halfY * negDir),
                RotationDirection.Left  => new Vector3(halfX * posDir, height, halfY * negDir),
                _   => new Vector3(0.0f, 0.0f)
            };
        }

        Vector2Int CalculateFootprintGridSize()
        {
            var cellSize = GameWorld.ActiveGridCellSize;
            var width = Mathf.CeilToInt(_startObjectSize.x / cellSize);
            var length = Mathf.CeilToInt(_startObjectSize.z / cellSize);
            return new Vector2Int(width, length);
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

        public Vector2Int RotatedSize => _rotatedFootprintGridSize;
        public bool IsObjectSingleTile => _footprintGridSize.x == 1 && _footprintGridSize.y == 1;
        public bool IsObjectSquare => _footprintGridSize.x == _footprintGridSize.y;
        public bool IsObjectVertical => RotatedSize.x < RotatedSize.y;
        public FootprintOrientation FootprintOrientation => IsObjectVertical ? 
            FootprintOrientation.Vertical :
            FootprintOrientation.Horizontal;
    }
}