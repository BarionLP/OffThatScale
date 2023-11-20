using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Movement{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class GravitylessMovementController : MonoBehaviour{
        [SerializeField] private float AccelerationMultiplier = 2;
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
            Animator.SetBool(animIDFloating, true);
        }

        private void Update(){
            transform.rotation = Input.Rotation;
            var acceleration = Input.CameraRotation * Input.Acceleration * AccelerationMultiplier;
            if(Input.ShouldSlowDown){
                // (Controller.velocity.sqrMagnitude > 1 ? Controller.velocity.normalized : Controller.velocity) //should not be neccessary
                acceleration -= AccelerationMultiplier * Rigidbody.velocity.normalized;
            }
            Rigidbody.AddForce(acceleration);
        }

        private void UpdateState(){
            enabled = GameManager.IsZeroG;
        }

        private void OnDestroy(){
            GameManager.OnGravityChange -= UpdateState;
        }

        private readonly int animIDFloating = Animator.StringToHash("Floating");
    }
}
