using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Movement{
    public sealed class GravitylessMovementController : MonoBehaviour{
        private float animationBlend;
        private Vector3 velocity;

        private Animator Animator;
        private CharacterController Controller;
        private IGravitylessMovementInput Input;

        private void Awake(){
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            if (!TryGetComponent(out Input)) Debug.LogWarning($"{name} has no MovementInput");
        }

        private void Start(){
            if(!PlayerInput.IsZeroG) enabled = false; 
        }
    }

    public interface IGravitylessMovementInput{
        public Vector3 Acceleration { get; }
        public bool ShouldRoll { get; }
    }
}
