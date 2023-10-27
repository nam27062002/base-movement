using System;

namespace Game.Player.Movement.States.Grounded.Moving
{
    [Serializable]
    public class RunningState : GroundedState
    {
        public RunningState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(AnimationData.RunParameterHash);
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation(AnimationData.RunParameterHash);
        }
    }
}