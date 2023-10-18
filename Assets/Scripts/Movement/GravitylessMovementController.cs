using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Movement{

    public sealed class GravitylessMovementController : MonoBehaviour{
        [SerializeField] private float AccelerationMultiplier = 2;
        private Vector3 velocity;

        private Animator Animator;
        private CharacterController Controller;
        private IGravitylessMovementInput Input;

        private void Awake(){
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            if (!TryGetComponent(out Input)) Debug.LogWarning($"{name} has no GravitylessMovementInput");
            GameManager.OnGravityChange += UpdateState;
        }

        private void OnEnable(){
            Animator.SetBool(animIDFreeFall, true);
        }

        private void Update(){
            transform.rotation = Input.Rotation;
            velocity += transform.rotation * Input.Acceleration * Time.deltaTime;
            Controller.Move(velocity * Time.deltaTime);
        }

        private void UpdateState(){
            enabled = PlayerInput.IsZeroG;
        }

        ~GravitylessMovementController(){
            GameManager.OnGravityChange -= UpdateState;
        }

        private readonly int animIDFreeFall = Animator.StringToHash("FreeFall");
    }
}
