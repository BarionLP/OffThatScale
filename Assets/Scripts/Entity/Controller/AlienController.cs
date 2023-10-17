using System.Linq;
using Ametrin.KunstBLL.Entity.Goals;
using Ametrin.Utils.Unity;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Controller{
    [RequireComponent(typeof(AlienManager))]
    public sealed class AlienController : EntityController{
        [SerializeField] private Vector2 StoppingDistance = new (2, 4);
        
        public override Vector3 LookDirection => transform.forward;
        private IGoal[] Goals;

        protected override void Awake(){
            base.Awake();
            Goals = GetComponents<IGoal>();
        }

        protected override void Update(){
            base.Update();
            UpdateTarget();
            if(ShouldShoot){
                UseMainHandItem();
            }
        }

        private void FixedUpdate(){
            Goals.FirstOrDefault(goal => goal.ShouldTick(this))?.Tick(this);
        }

        private void UpdateTarget(){
            if(TargetPosition is not Vector3 targetPosition) return;

            if(IsMoving && targetPosition.Approximately(transform.position, StoppingDistance.x)){
                IsMoving = false;
                return;
            }

            if(!IsMoving && targetPosition.Approximately(transform.position, StoppingDistance.y)) return;

            var direction = (targetPosition - transform.position).normalized;
            IsMoving = true;
            Move = new(direction.x, direction.z);
            Rotation = Quaternion.LookRotation(direction, transform.up);
        }

        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(TargetPosition.Value, 0.5f);
            Gizmos.DrawRay(transform.position, new(Move.x, 0, Move.y));
        }
    }
}
