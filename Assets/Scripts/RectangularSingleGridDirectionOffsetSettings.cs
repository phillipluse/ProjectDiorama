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
            const float negDir = -1.0f;
            return dir switch
            {
                RotationDirection.Up    => new Vector3(size.x / 2, height, size.y / 2), //halfsize, halfsize
                RotationDirection.Right => new Vector3(0.0f,       height, size.y / 2), //0, halfsize
                RotationDirection.Down  => new Vector3(0.0f,       height, size.y / 2   * negDir), //0, -halfsize
                RotationDirection.Left  => new Vector3(size.x / 2, height, (size.y / 2) * negDir), //halfsize, -halfsize
                _   => new Vector3(0.0f, 0.0f)
            };
        }

        public override Vector3 SpritePositionToCursor(Vector2 size, FootprintOrientation footPrintOrientation, Vector2Int directions, int cellSize)
        {
            var halfTile = (float)cellSize / 2;
            return footPrintOrientation == FootprintOrientation.Vertical
                ? new Vector3(0.0f, (size.y / 2 - halfTile) * directions.y, 0.0f)  
                : new Vector3((size.x / 2 - halfTile) * directions.x, 0.0f, 0.0f);
        }
    }
}
