using UnityEngine;

namespace Features.ExtendedRandom
{
    /**
     * Additional methods to randomize numbers.
     */
    public static class XRandom
    {
        /**
         * Vector2Int.x = minInclusive
         * Vector2Int.y = maxInclusive (different behavior than Random.Range!)
         */
        public static int Range(Vector2Int range)
        {
            return Random.Range(range.x, range.y + 1);
        }
    }
}
