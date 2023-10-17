using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Movement{

    [RequireComponent(typeof(CharacterController))]
    public sealed class MovementController : MonoBehaviour{
#pragma warning disable IDE0044
        [Header("Stats")]
        [SerializeField] private float MoveSpeed = 2.0f;
        [SerializeField] private float SprintSpeed = 5.335f;
        [SerializeField] private float SpeedChangeRate = 10.0f;
        [SerializeField] private float JumpForce = 1.2f;
        [SerializeField] private float FallTimeout = 0.15f;

        [Header("Ground Checks")]
        [SerializeField] private float GroundedOffset = -0.14f;
        [SerializeField] private float GroundedRadius = 0.28f;
        [SerializeField] private LayerMask GroundLayers;
#pragma warning restore IDE0044

        private bool grounded = true;
        private float currentSpeed;
        private float animationBlend;
        private Vector3 velocity;

        private float fallTimeoutDelta;

        private Animator Animator;
        private CharacterController Controller;
        private IMovementInput Input;

        private void Awake(){
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            if(!TryGetComponent(out Input)) Debug.LogWarning($"{name} has no MovementInput");
        }
        private void Start(){
            fallTimeoutDelta = FallTimeout;
            if (!PlayerInput.IsZeroG) enabled = false;
        }

        private void Update(){
            GroundedCheck();
            Gravity();
            Jump();
            Move();
        }

        private void Move(){
            const float speedOffset = 0.1f;
            var targetSpeed = Input.IsMoving ? Input.IsSprinting ? SprintSpeed : MoveSpeed : 0f;
            var currentHorizontalSpeed = new Vector3(Controller.velocity.x, 0f, Controller.velocity.z).magnitude;
            var inputMagnitude = Input.Move.magnitude;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset){
                currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            } else{
                currentSpeed = targetSpeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;

            float targetAngle = Mathf.Atan2(Input.Move.x, Input.Move.y) * Mathf.Rad2Deg;
            var inputDirection = Quaternion.Euler(0, targetAngle, 0) * transform.forward;
            Controller.Move(inputDirection * (currentSpeed * Time.deltaTime) + velocity * Time.deltaTime);

            transform.rotation = Input.Rotation;

            Animator.SetFloat(animIDSpeed, animationBlend);
            Animator.SetFloat(animIDMotionSpeed, inputMagnitude);
        }

        private void Jump(){
            if(!(grounded && Input.ShouldJump)) return;

            velocity.y = JumpForce;
            Animator.SetBool(animIDJump, true);
        }

        private void Gravity(){
            if (grounded){
                if(velocity.sqrMagnitude < 0){
                    velocity = Vector3.zero;
                }
                fallTimeoutDelta = FallTimeout;
                Animator.SetBool(animIDJump, false);
                Animator.SetBool(animIDFreeFall, false);
            }
            else{
                if (fallTimeoutDelta >= 0.0f){
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else{
                    Animator.SetBool(animIDFreeFall, true);
                }
                velocity += Physics.gravity * Time.deltaTime;
            }
        }

        private void GroundedCheck(){
            var spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
            Animator.SetBool(animIDGrounded, grounded);
        }

        private void OnDrawGizmosSelected(){
            Gizmos.color = grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private readonly int animIDSpeed = Animator.StringToHash("Speed");
        private readonly int animIDJump = Animator.StringToHash("Jump");
        private readonly int animIDGrounded = Animator.StringToHash("Grounded");
        private readonly int animIDFreeFall = Animator.StringToHash("FreeFall");
        private readonly int animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
}