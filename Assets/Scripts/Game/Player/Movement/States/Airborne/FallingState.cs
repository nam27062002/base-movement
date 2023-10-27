namespace Game.Player.Movement.States.Airborne
{
    public class FallingState : AirborneState
    {
        public FallingState(MovementStateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();

            StartAnimation(AnimationData.FallParameterHash);
            
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(AnimationData.FallParameterHash);
        }
    }
}