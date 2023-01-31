using System;

namespace ProjectDiorama
{
    public static class EnumExtensions
    {
        public static T Next<T>(this T source) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            int j = Array.IndexOf(values, source) + 1;
            return (values.Length == j) ? values[0] : values[j];
        }

        public static int RotationAngle(this RotationDirection rotationDirection)
        {
            return rotationDirection switch
            {
                RotationDirection.Up    => 0,
                RotationDirection.Right => 90,
                RotationDirection.Down  => 180,
                RotationDirection.Left  => 270,
                _ => throw new ArgumentOutOfRangeException(nameof(rotationDirection), rotationDirection, null)
            };
        }
    }
}