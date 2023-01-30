using UnityEngine;

namespace ProjectDiorama
{
    public abstract class DirectionOffsetSettings : ScriptableObject
    {
        public abstract Vector2Int DirectionsPerRotation(RotationDirection dir);

        public abstract Vector3 ObjectOffset(Vector2 size, float height, RotationDirection dir, Vector2Int directions, int cellSize);
    }
}
