using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Movement{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class GravitylessMovementController : MonoBehaviour{
        [SerializeField] private float AccelerationMultiplier = 2;
        // private Vector3 velocity;
        private Animator Animator;
        private Rigidbody Rigidbody;
        private IGravitylessMovementInput Input;

        private void Awake(){
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            if (!TryGetComponent(out Input)) Debug.LogWarning($"{name} has no GravitylessMovementInput");
            GameManager.OnGravityChange += UpdateState;
        }

        private void OnEnable(){
            // Animator.SetBool(animIDFreeFall, true);
        }

        private void Update(){
            transform.rotation = Input.Rotation;
            var acceleration = transform.rotation * Input.Acceleration * AccelerationMultiplier;// * Time.deltaTime;
            if(Input.ShouldSlowDown){
                // (Controller.velocity.sqrMagnitude > 1 ? Controller.velocity.normalized : Controller.velocity) //should not be neccessary
                acceleration -= AccelerationMultiplier * Time.deltaTime * Rigidbody.velocity.normalized;
            }
            // var velocity = Rigidbody.velocity + acceleration;
            Rigidbody.AddForce(acceleration);
        }

        private void UpdateState(){
            enabled = GameManager.IsZeroG;
        }

        ~GravitylessMovementController(){
            GameManager.OnGravityChange -= UpdateState;
        }

        private readonly int animIDFreeFall = Animator.StringToHash("FreeFall");
    }
}
