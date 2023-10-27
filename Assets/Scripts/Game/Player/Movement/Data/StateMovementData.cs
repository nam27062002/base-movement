using UnityEngine;

namespace Game.Player.Movement.Data
{
    public class StateMovementData
    {
        public Vector2 MovementInput;
        public float MovementSpeedModifier;

        #region Rotate 
        private Vector3 _currentTargetRotation;
        private Vector3 _dampedTargetRotationPassedTime;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _timeToReachTargetRotation;
        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
        public ref Vector3 DampedTargetRotationPassedTime => ref _dampedTargetRotationPassedTime;
        public ref Vector3 DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        #endregion

        #region Jump

        public Vector3 CurrentJumpForce;

        #endregion
    }
}