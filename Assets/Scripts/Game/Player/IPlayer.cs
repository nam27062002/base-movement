using Game.Inputs;
using Game.Player.Movement.Data.Animations;
using Game.Player.SO;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        Animator Animator { get; }
        Transform PlayerTransform { get; }
        PlayerInput PlayerInput { get; }
        Rigidbody Rigidbody { get; }
        PlayerSo MovementDataConfig { get; }
        Transform MainCameraTransform { get; }
        CapsuleCollider CapsuleCollider { get; }
        PlayerAnimationData AnimationData { get; }
    }
}