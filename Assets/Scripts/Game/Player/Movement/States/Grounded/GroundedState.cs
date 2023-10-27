using System;
using Game.Player.Movement.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Movement.States.Grounded
{
    public class GroundedState : MovementState
    {
        protected GroundedState(MovementStateMachine stateMachine) : base(stateMachine) { }
        
        #region IState

        public override void Enter()
        {
            base.Enter();
            StartAnimation(AnimationData.GroundedParameterHash);
            SetJumpForce(this);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(AnimationData.GroundedParameterHash);
        }

        #endregion
        
        #region Main Methods
        
        private void SetJumpForce(MovementState state)
        {
            var name = StateMachine.GetMovementStateName(state);
           
            switch (name)
            {
                case StateNames.Idling:
                    MovementData.CurrentJumpForce = PlayerDataConfig.playerAirborneData.jumpData.stationaryForce;
                    break;
                case StateNames.Running:
                    MovementData.CurrentJumpForce = PlayerDataConfig.playerAirborneData.jumpData.runForce;
                    break;
                case StateNames.Jumping:
                    break;
                case StateNames.Falling:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                
            }
        }
        #endregion 
        
        #region Resuable Methods
        protected override void AddInputActionsCallbacks()
        {
            PlayerInput.Movement.started += OnMovementStarted;
            PlayerInput.Movement.canceled += OnMovementCanceled;
            PlayerInput.Jump.started += OnJumpStarted;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            PlayerInput.Movement.started -= OnMovementStarted;
            PlayerInput.Movement.canceled -= OnMovementCanceled;
            PlayerInput.Jump.started -= OnJumpStarted;
        }
        #endregion

        protected override void OnContactWithGroundExited(Collider collider)
        {
            base.OnContactWithGroundExited(collider);
            var bounds = CapsuleCollider.bounds;
            var capsuleColliderCenterInWorldSpace = bounds.center;
            var colliderVerticalExtents = bounds.extents;
            var downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - colliderVerticalExtents, Vector3.down);
            
            if (!Physics.Raycast(
                    downwardsRayFromCapsuleBottom,
                    out _,
                    PlayerDataConfig.playerGroundedData.groundToFallRayDistance,
                    PlayerDataConfig.playerLayerData.groundLayer,
                    QueryTriggerInteraction.Ignore))
            {
               
            }
            
        }

        protected virtual void OnFall()
        {
            StateMachine.ChangeMovementState(StateNames.Falling);
        }
        #region Input Methods

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            StateMachine.ChangeMovementState(StateNames.Running);
        }
        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            StateMachine.ChangeMovementState(StateNames.Idling);
        }
        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            StateMachine.ChangeMovementState(StateNames.Jumping);
        }

        #endregion
    }
}