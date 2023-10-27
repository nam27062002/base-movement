using Game.Player.Movement.Data;
using UnityEngine;

namespace Game.Player.Movement.States.Airborne
{
    public class AirborneState : MovementState
    {
        protected AirborneState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        

        public override void Enter()
        {
            base.Enter();

            StartAnimation(AnimationData.AirborneParameterHash);
            
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(AnimationData.AirborneParameterHash);
        }

        protected override void OnContactWithGround(Collider collider)
        {
            StateMachine.ChangeMovementState(IsMovementStarted() ? StateNames.Running : StateNames.Idling);
        }
    }
}