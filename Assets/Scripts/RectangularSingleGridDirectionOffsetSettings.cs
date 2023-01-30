using UnityEngine;

namespace ProjectDiorama
{
    [CreateAssetMenu(fileName = "SingleRectangularGridOffsetSettings", menuName = "Scriptable Objects / Offset Settings / Single Rectangular Grid")]
    public class RectangularSingleGridDirectionOffsetSettings : DirectionOffsetSettings
    {
        public override Vector2Int DirectionsPerRotation(RotationDirection dir)
        {
            return dir switch
            {
                RotationDirection.Up     => new Vector2Int(1, 1),
                RotationDirection.Right  => new Vector2Int(1, -1),
                RotationDirection.Down   => new Vector2Int(-1, 1),
                RotationDirection.Left   => new Vector2Int(1, 1),
                _   => new Vector2Int(1, 1)
            };
        }

        public override Vector3 ObjectOffset(Vector2 size, float height, RotationDirection dir, Vector2Int directions, int cellSize)
        {
            float halfX = size.x / 2;
            float halfY = size.y / 2;
            return dir switch
            {
                RotationDirection.Up    => new Vector3(halfX,              height, halfY),
                RotationDirection.Right => new Vector3(halfX - cellSize, height, halfY),
                RotationDirection.Down  => new Vector3(halfX - cellSize, height, halfY - cellSize),
                RotationDirection.Left  => new Vector3(halfX,              height, halfY - cellSize),
                _   => new Vector3(0.0f, 0.0f)
            };
        }
    }
}
