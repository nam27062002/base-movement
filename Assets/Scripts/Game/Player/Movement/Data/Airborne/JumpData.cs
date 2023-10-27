using System;
using Game.Player.Movement.Data.Rotation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Player.Movement.Data.Airborne
{
    [Serializable]  
    public class JumpData : BaseData
    {
        public Vector3 stationaryForce;
        public Vector3 runForce;
    }
}