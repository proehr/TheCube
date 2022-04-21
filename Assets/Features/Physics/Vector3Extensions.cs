using UnityEngine;

namespace Features.Physics
{
    public static class Vector3Extensions
    {
        public static float Heading(this Vector3 v)
        {
            return Mathf.Atan2(v.z, v.x);
        }

        /// <summary>
        /// Compares this vector with another vector. If the length difference is smaller or equal to the
        /// acceptableDelta, this method returns true.
        /// </summary>
        public static bool EqualEnough(this Vector3 v, Vector3 other, float acceptableDelta)
        {
            float num1 = v.x - other.x;
            float num2 = v.y - other.y;
            float num3 = v.z - other.z;
            return (double) num1 * (double) num1 + (double) num2 * (double) num2 + (double) num3 * (double) num3 <
                   acceptableDelta * acceptableDelta;
        }
    }
}