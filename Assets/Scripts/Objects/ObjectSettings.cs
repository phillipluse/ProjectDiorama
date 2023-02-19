using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectSettings
    {
        public Vector2Int FootprintGridSize { get; } //x = width, y = length (Z axis)

        public Vector3 StartObjectSize { get; }

        Vector2Int _rotatedFootprintGridSize;
        
        public ObjectSettings(Vector3 objectSize)
        {
            StartObjectSize = objectSize;
            FootprintGridSize = CalculateFootprintGridSize();
            UpdateRotatedSize(RotationDirection.Up);
        }

        public void UpdateRotatedSize(RotationDirection dir)
        {
            _rotatedFootprintGridSize = SizePerRotation(dir);
        }

        public Vector3 ObjectOffset(RotationDirection dir)
        {
            var cellSize = GameWorld.ActiveGridCellSize;
            float height = 0;
            float halfX = IsObjectSingleTile ? (float)cellSize / 2 : (float)FootprintGridSize.x / 2 * cellSize;
            float halfY = IsObjectSingleTile ? (float)cellSize / 2 : (float)FootprintGridSize.y / 2 * cellSize;
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
            var width = Mathf.CeilToInt(Mathf.Round(StartObjectSize.x) / cellSize);
            var length = Mathf.CeilToInt(Mathf.Round(StartObjectSize.z) / cellSize);
            return new Vector2Int(width, length);
        }
        
        Vector2Int SizePerRotation(RotationDirection dir)
        {
            var startSize = FootprintGridSize;
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
        public bool IsObjectSingleTile => FootprintGridSize.x == 1 && FootprintGridSize.y == 1;
        public bool IsObjectSquare => FootprintGridSize.x == FootprintGridSize.y;
        public bool IsObjectVertical => RotatedSize.x < RotatedSize.y;
        public FootprintOrientation FootprintOrientation => IsObjectVertical ? 
            FootprintOrientation.Vertical :
            FootprintOrientation.Horizontal;
    }
}