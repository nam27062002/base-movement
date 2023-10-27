using UnityEngine;

namespace Game.StateMachines
{
    public  abstract class StateMachine
    {
        public IState CurrentState;

        protected void ChangeState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void PhysicsUpdate()
        {
            CurrentState?.PhysicsUpdate();
        }

        public void OnTriggerEnter(Collider collider)
        {
            CurrentState?.OnTriggerEnter(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            CurrentState?.OnTriggerExit(collider);
        }

    }
}