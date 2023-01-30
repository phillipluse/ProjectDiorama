using UnityEngine;

namespace ProjectDiorama
{
    [CreateAssetMenu(fileName = "SingleSquareGridOffsetSettings", menuName = "Scriptable Objects / Offset Settings / Single Square Grid")]
    public class SquareSingleGridDirectionOffsetSettings : DirectionOffsetSettings
    {
        public override Vector2Int DirectionsPerRotation(RotationDirection dir)
        {
            return new Vector2Int(1, 1);
        }

        public override Vector3 ObjectOffset(Vector2 size, float height, RotationDirection dir, Vector2Int directions, int cellSize)
        {
            return new Vector3(size.x / 2, height, size.y / 2);
        }

        public override Vector3 SpritePositionToCursor(Vector2 size, FootprintOrientation footPrintOrientation,
            Vector2Int directions, int cellSize)

        {
            var halfTile = (float)cellSize / 2;
            return new Vector3(size.x / 2 - halfTile, size.y / 2 - halfTile);
        }
    }
}
