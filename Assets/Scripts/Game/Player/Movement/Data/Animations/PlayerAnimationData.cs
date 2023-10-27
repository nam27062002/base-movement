using System;
using UnityEngine;

namespace Game.Player.Movement.Data.Animations
{
    [Serializable]
    public class PlayerAnimationData
    {
        [Header("State Group Parameter Names")]
        [SerializeField] private string groundedParameterName = "Grounded";
        [SerializeField] private string airborneParameterName = "Airborne";
        
        [Header("Grounded Parameter Names")]
        [SerializeField] private string idleParameterName = "isIdling";
        [SerializeField] private string runParameterName = "isRunning";
        [Header("Airborne Parameter Names")]
        [SerializeField] private string fallParameterName = "isFalling";
        
        public int GroundedParameterHash { get; private set; }
        public int AirborneParameterHash { get; private set; }
        public int IdleParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int FallParameterHash { get; private set; }
        
        public void Initialize()
        {
            GroundedParameterHash = Animator.StringToHash(groundedParameterName);
            AirborneParameterHash = Animator.StringToHash(airborneParameterName);

            IdleParameterHash = Animator.StringToHash(idleParameterName);
            RunParameterHash = Animator.StringToHash(runParameterName);
            
            FallParameterHash = Animator.StringToHash(fallParameterName);
            
        }
    }
}