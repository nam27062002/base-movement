using System;
using Game.Inputs;
using Game.Player.Movement.Data;
using Game.Player.Movement.Data.Animations;
using Game.Player.SO;
using Game.StateMachines;
using Utility;
using UnityEngine;

namespace Game.Player.Movement.States
{
    public class MovementState : IState
    {
        protected readonly PlayerInput PlayerInput;
        protected readonly MovementStateMachine StateMachine;
       
        protected readonly Rigidbody Rigidbody;
        protected readonly Transform PlayerTransform;
        protected readonly PlayerSo PlayerDataConfig;
        protected readonly StateMovementData MovementData;
        protected readonly CapsuleCollider CapsuleCollider;
        protected readonly PlayerAnimationData AnimationData;
        protected readonly Animator Animator;
        private readonly Transform _cameraTransform;
        protected MovementState(MovementStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            PlayerInput = stateMachine.PlayerInstance.PlayerInput;
            Rigidbody = stateMachine.PlayerInstance.Rigidbody;
            _cameraTransform = stateMachine.PlayerInstance.MainCameraTransform;
            PlayerTransform = stateMachine.PlayerInstance.PlayerTransform;
            PlayerDataConfig = stateMachine.PlayerInstance.MovementDataConfig;
            CapsuleCollider = stateMachine.PlayerInstance.CapsuleCollider;
            AnimationData = stateMachine.PlayerInstance.AnimationData;
            Animator = stateMachine.PlayerInstance.Animator;
            MovementData = stateMachine.MovementData;
            
        }
        
        #region IState Methods
       
        public virtual void Enter()
        {
            // Debug.Log($"State: {GetType().Name}");
            SetSpeedModifier(this);
            SetRotationTime(this);
            AddInputActionsCallbacks();
        }
        
        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }
        
        public virtual void HandleInput()
        {
            MovementData.MovementInput = PlayerInput.DirectionMovement;
        }

        public virtual void Update()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (!PlayerDataConfig.playerLayerData.IsGroundLayer(collider.gameObject.layer)) return;
            OnContactWithGround(collider);
        }

        public virtual void OnTriggerExit(Collider collider)
        {
            if (!PlayerDataConfig.playerLayerData.IsGroundLayer(collider.gameObject.layer)) return;
            OnContactWithGroundExited(collider);
        }
        


        #endregion

        #region Main Methods

        private void Move()
        {
            if (!CanMove()) return;
            var movementDirection = GetMovementInputDirection();
            var targetRotationYAngle = Rotate(movementDirection);
            var targetRotationDirection = MathUtility.GetTargetRotationDirection(targetRotationYAngle);
            var movementSpeed = GetMovementSpeed();
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,ForceMode.VelocityChange);
        }
        private float Rotate(Vector3 direction)
        {
            var directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            return directionAngle;
        }
        
        
        private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            var directionAngle = MathUtility.GetAngleOfDirection(direction);
            if (shouldConsiderCameraRotation)
            {
                directionAngle = AdjustAngleWithCameraRotation(directionAngle);
            }

            if (!MathUtility.IsClose(directionAngle, MovementData.CurrentTargetRotation.y))
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            MovementData.CurrentTargetRotation.y = targetAngle;
            MovementData.DampedTargetRotationPassedTime.y = 0f;
        }
        
        private float AdjustAngleWithCameraRotation(float angle)
        {
            angle += GetCameraEulerAngles().y;
            return (angle > 360f) ? angle - 360f : angle;
        }
        

        private Vector3 GetCameraEulerAngles()
        {
            return _cameraTransform.eulerAngles;
        }
        
        #endregion
        
        #region Reusable Methods
        
        protected bool IsMovementStarted()
        {
            return MovementData.MovementInput != Vector2.zero;
        }

        protected void ResetVelocity()
        {
            Rigidbody.velocity = Vector3.zero;
        }
        
        private bool CanMove()
        {
            return IsMovementStarted() && MovementData.MovementSpeedModifier != 0f;
        }

        private Vector3 GetMovementInputDirection()
        {
            return new Vector3(MovementData.MovementInput.x, 0f, MovementData.MovementInput.y);
        }

        private float GetMovementSpeed()
        {
            return PlayerDataConfig.baseSpeed * MovementData.MovementSpeedModifier;
        }

        private Vector3 GetPlayerHorizontalVelocity()
        {
            var velocity = Rigidbody.velocity;
            return new Vector3(velocity.x,0f,velocity.z);
        }
        
        protected virtual void OnContactWithGround(Collider collider)
        {
           
        }
        protected virtual void OnContactWithGroundExited(Collider collider)
        {
           
        }
        
        private void SetSpeedModifier(MovementState state)
        {
            MovementData.MovementSpeedModifier = StateMachine.GetBaseData(state).speedModifier;
        }

        private void SetRotationTime(MovementState state)
        {
            MovementData.TimeToReachTargetRotation = StateMachine.GetBaseData(state).rotationData.targetRotationReachTime;
        }
        private void RotateTowardsTargetRotation()
        {
            var currentYAngle = Rigidbody.rotation.eulerAngles.y;
            if (MathUtility.IsClose(currentYAngle, MovementData.CurrentTargetRotation.y)) return;
            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, MovementData.CurrentTargetRotation.y,
                ref MovementData.DampedTargetRotationCurrentVelocity.y, 
                MovementData.TimeToReachTargetRotation.y - MovementData.DampedTargetRotationPassedTime.y);
            MovementData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0f,smoothedYAngle,0f);
            Rigidbody.MoveRotation(targetRotation);
            
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            
        }
        
        protected virtual void RemoveInputActionsCallbacks()
        {
            
        }
        protected void StartAnimation(int animationHash)
        {
            Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            Animator.SetBool(animationHash, false);
        }
        #endregion
    }
}