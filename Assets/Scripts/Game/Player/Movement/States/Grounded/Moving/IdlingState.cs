using System;

namespace Game.Player.Movement.States.Grounded.Moving
{
    [Serializable]
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            StartAnimation(AnimationData.IdleParameterHash);
            ResetVelocity();
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation(AnimationData.IdleParameterHash);
        }

        #endregion
       
    }
}