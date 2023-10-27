using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        private InputActions.PlayerActions _playerActions;
        
        #region Monobehaviour
        private void Awake()
        {
            var inputActions = new InputActions();
            _playerActions = inputActions.Player;
        }
        private void OnEnable()
        {
            _playerActions.Enable();
        }

        private void OnDisable()
        {
            _playerActions.Disable();
        }
        #endregion
        
        #region Actual API

        public Vector2 DirectionMovement => _playerActions.Move.ReadValue<Vector2>();
        public InputAction Movement => _playerActions.Move;
        public InputAction Jump => _playerActions.Jump;

        #endregion
    }
}