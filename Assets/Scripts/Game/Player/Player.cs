using Game.Inputs;
using Game.Player.Movement;
using Game.Player.Movement.Data;
using Game.Player.Movement.Data.Animations;
using Game.Player.SO;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour,IPlayer
    {
        [field: Header("References")]
        [SerializeField] private PlayerSo movementDataConfig;

        [field: Header("Animations")] 
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerAnimationData animationData;
        [SerializeField] private CapsuleCollider capsuleCollider;  
        private MovementStateMachine _movementStateMachine;
    
        #region Implement Interface

        public Animator Animator => animator;
        public Transform MainCameraTransform { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public PlayerAnimationData AnimationData => animationData;
        public PlayerSo MovementDataConfig => movementDataConfig;
        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Transform PlayerTransform => transform;
        
        #endregion
        
        #region MonoBehaviour
        private void Awake()
        {
            PlayerInput = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();
            animationData.Initialize();
            if (UnityEngine.Camera.main != null) MainCameraTransform = UnityEngine.Camera.main.transform;
            _movementStateMachine = new MovementStateMachine(this);
        }

        private void Start()
        {
            _movementStateMachine.ChangeMovementState(StateNames.Idling);   
        }

        private void Update()
        {
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementStateMachine.PhysicsUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            _movementStateMachine.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _movementStateMachine.OnTriggerExit(other);
        }

        #endregion



    }
}