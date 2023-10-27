using System;
using Game.Player.Movement.Data.Rotation;
using UnityEngine;

namespace Game.Player.Movement.Data.Grounded
{
    [Serializable]
    public class PlayerGroundedData
    {
        [Range(0f,5f)] public float groundToFallRayDistance = 1f;
        public IdleData idleData;
        public RunData runData;
    }
}