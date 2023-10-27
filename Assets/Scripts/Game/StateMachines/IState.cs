using UnityEngine;

namespace Game.StateMachines
{
    public interface IState
    {
        void Enter();
        void Exit();
        void HandleInput();
        void Update();
        void PhysicsUpdate();
        void OnTriggerEnter(Collider collider);
        void OnTriggerExit(Collider collider);
    }
}