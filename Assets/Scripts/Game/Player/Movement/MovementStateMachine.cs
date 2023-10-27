using System;
using System.Collections.Generic;
using System.Linq;
using Game.Player.Movement.Data;
using Game.Player.Movement.Data.Grounded;
using Game.Player.Movement.States;
using Game.Player.Movement.States.Airborne;
using Game.Player.Movement.States.Grounded.Moving;
using Game.Player.SO;
using Game.StateMachines;

namespace Game.Player.Movement
{
    public class MovementStateMachine : StateMachine
    {
        private readonly IdlingState _idleState;
        private readonly RunningState _runState;
        private readonly JumpingState _jumpingState;
        private readonly FallingState _fallingState;
        
        private Dictionary<StateNames, MovementState> _movementStates;
        private Dictionary<MovementState, BaseData> _movementBaseData;
        public readonly IPlayer PlayerInstance;
        private readonly PlayerSo _playerDataConfig;
        public readonly StateMovementData MovementData;
        public MovementStateMachine(IPlayer player)
        { 
            PlayerInstance = player;
            _playerDataConfig = player.MovementDataConfig;
            MovementData = new StateMovementData();
            
            _idleState = new IdlingState(this);
            _runState = new RunningState(this);
            _jumpingState = new JumpingState(this);
            _fallingState = new FallingState(this);
            
            InitializeStates();
        }

        #region Main Methods

        private void InitializeStates()
        {
            _movementStates = new Dictionary<StateNames, MovementState>()
            {
                { StateNames.Idling ,_idleState},
                {StateNames.Running, _runState},
                { StateNames.Jumping, _jumpingState},
                { StateNames.Falling, _fallingState}
            };
            
            _movementBaseData = new Dictionary<MovementState, BaseData>()
            {
                {_idleState, _playerDataConfig.playerGroundedData.idleData},
                {_runState, _playerDataConfig.playerGroundedData.runData},
                {_jumpingState, _playerDataConfig.playerAirborneData.jumpData},
                {_fallingState, _playerDataConfig.playerAirborneData.failData}
            };
        }
        #endregion

        #region Actual API

        public void ChangeMovementState(StateNames stateName)
        {
            ChangeState(_movementStates[stateName]);
        }

        public StateNames GetMovementStateName(MovementState state)
        {
            foreach (var pair in _movementStates.Where(pair => pair.Value.Equals(state)))
            {
                return pair.Key;
            }

            throw new Exception("Movement state not found");
        }

        public BaseData GetBaseData(MovementState state)
        {
            return _movementBaseData[state];
        }
        #endregion
    }
}
