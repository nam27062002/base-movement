using System;
using UnityEngine;

namespace Game.Player.Movement.Data.Layers
{
    [Serializable]
    public class PlayerLayerData
    {
        public LayerMask groundLayer;

        public bool ContainsLayer(LayerMask layerMask, int layer)
        {
            return (1 << layer & layerMask) != 0;
        }

        public bool IsGroundLayer(int layer)
        {
            return ContainsLayer(groundLayer, layer);
        }
    }
}