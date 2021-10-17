﻿using UnityEngine;

namespace WorkerAI
{
    public static class Vector3Extensions
    {
        public static float Heading(this Vector3 v)
        {
            return Mathf.Atan2(v.z, v.x);
        }
    }
}