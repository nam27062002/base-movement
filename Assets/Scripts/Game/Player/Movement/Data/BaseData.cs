using System;
using Game.Player.Movement.Data.Rotation;
using UnityEngine;

namespace Game.Player.Movement.Data
{
    [Serializable]
    public class BaseData
    {
        [Range(0f, 5f)] public float speedModifier;
        public PlayerRotationData rotationData;
    }
}