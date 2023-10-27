using System;
using UnityEngine;

namespace Utility
{
    public static class MathUtility
    {
        public static bool IsClose(double x, double y, double tolerance = 0.001f)
        {
            return Math.Abs(x - y) <= tolerance;
        }

        public static float GetAngleOfDirection(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return (angle < 0f) ? angle + 360f : angle;
        }
        public static Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }
}