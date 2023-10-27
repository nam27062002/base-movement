using Game.Player.Movement.Data.Airborne;
using Game.Player.Movement.Data.Grounded;
using Game.Player.Movement.Data.Layers;
using UnityEngine;

namespace Game.Player.SO
{
    public class PlayerSo : ScriptableObject
    {
        [Range(0f,10f)] public float baseSpeed = 5f;
        public PlayerGroundedData playerGroundedData;
        public PlayerAirborneData playerAirborneData;
        public PlayerLayerData playerLayerData;
    }
}