using UnityEngine;

namespace ProjectDiorama
{
    [CreateAssetMenu(fileName = "MultipleGridOffsetSettings", menuName = "Scriptable Objects / Offset Settings / Multiple Grid")]
    public class MultipleGridDirectionOffsetSettings : DirectionOffsetSettings
    {
        public override Vector2Int DirectionsPerRotation(RotationDirection dir)
        {
            return dir switch
            {
                 RotationDirection.Up    => new Vector2Int(1, 1),
                 RotationDirection.Right => new Vector2Int(1,  -1),
                 RotationDirection.Down  => new Vector2Int(-1, -1),
                 RotationDirection.Left  => new Vector2Int(-1, 1),
                _   => new Vector2Int(1, 1)
            };
        }

        public override Vector3 ObjectOffset(Vector2 size, float height, RotationDirection dir, Vector2Int directions, int cellSize)
        {
            const float negDir = -1.0f;
            return dir switch
            {
                 RotationDirection.Up    => new Vector3(size.x / 2, height, size.y / 2), //half, half
                 RotationDirection.Right => new Vector3(0.0f,       height, size.y / 2), //0, half
                 RotationDirection.Down  => new Vector3(0.0f,       height, 0.0f), //0, 0
                 RotationDirection.Left  => new Vector3((size.x /2),height, 0.0f), //half,0
                _   => new Vector3(0.0f, 0.0f)
            };
        }

        public override Vector3 SpritePositionToCursor(Vector2 size, FootprintOrientation footPrintOrientation, Vector2Int directions,
            int cellSize)
        {
            var halfTile = (float)cellSize / 2;
            return new Vector3((size.x / 2 - halfTile) * directions.x,
                (size.y / 2 - halfTile) * directions.y, 0.0f);
        }
    }
}
