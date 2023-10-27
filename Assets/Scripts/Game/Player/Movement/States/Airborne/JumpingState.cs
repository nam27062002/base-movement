using UnityEngine;
using Utility;

namespace Game.Player.Movement.States.Airborne
{
    public class JumpingState : AirborneState
    {
        public JumpingState(MovementStateMachine stateMachine) : base(stateMachine) { }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            Jump();
        }
        
        #endregion

        #region Main Methods

        private void Jump()
        {
            var jumpForce = MovementData.CurrentJumpForce;
            var jumpDirection = PlayerTransform.forward;
            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;
            ResetVelocity(); 
            Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
        #endregion
    }
}