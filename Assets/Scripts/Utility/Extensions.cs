using UnityEngine;

namespace ProjectDiorama
{
    public static class Extensions
    {
        #region Vectors

        /// <summary> Compares the component values of two vectors </summary>
        public static bool IsApproximateTo(this Vector3 vector, Vector3 compareVector)
        {
            return Mathf.Approximately(vector.x, compareVector.x) && 
                   Mathf.Approximately(vector.y, compareVector.y) && 
                   Mathf.Approximately(vector.z, compareVector.z);
        }
        
        /// <summary> Compares the component values of two vectors </summary>
        public static bool IsApproximateTo(this Vector2 vector, Vector2 compareVector)
        {
            return Mathf.Approximately(vector.x, compareVector.x) && 
                   Mathf.Approximately(vector.y, compareVector.y);
        }
        
        #endregion

        #region Floats
        /// <summary> Compares the values of two floats </summary>
        public static bool IsApproximateTo(this float value, float compareValue) => Mathf.Approximately(value, compareValue);
        public static float Abs(this float value) => Mathf.Abs(value);
        public static float Sign(this float value) => Mathf.Sign(value);
        public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);
        public static bool Between(this float value, float min, float max) => value >= min && value <= max;
        public static bool BetweenWithTolerance(this float value, float min, float max, float tolerance) => value.Between(min - tolerance, max + tolerance);
        public static bool IsEqualWithTolerance(this float value, float compareValue, float tolerance)
        {
            return value.Between(compareValue, compareValue + tolerance) ||
                   value.Between(compareValue - tolerance, compareValue);
        }

        #endregion

        #region Quaternions

        /// <summary>Transforms a rotation from local space to world space</summary>
        /// <param name="tf">The transform to use</param>
        /// <param name="quat">The local space rotation</param>
        public static Quaternion TransformRotation( this Transform tf, Quaternion quat ) => tf.rotation * quat;

        /// <summary>Transforms a rotation from world space to local space</summary>
        /// <param name="tf">The transform to use</param>
        /// <param name="quat">The world space rotation</param>
        public static Quaternion InverseTransformRotation( this Transform tf, Quaternion quat ) => tf.rotation * quat;

        /// <summary>Returns a Quaternion with Euler Angles of (0, 0, 0)</summary>
        /// <param name="quat"></param>
        /// <returns></returns>
        public static Quaternion Zero(this Quaternion quat) =>  Quaternion.Euler(Vector3.zero);

        #endregion
    }
}