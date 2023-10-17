using Ametrin.KunstBLL.Entity.Controller;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Goals{
    public sealed class AttackPlayerGoal : Goal{
        [SerializeField] private float LookDistance;
        [SerializeField] private LayerMask DetectionLayers;
        private PlayerManager Target;

        protected override bool ShouldTick(EntityController entity){
            if(Target == null){
                if(!FindTarget(entity)){
                    entity.ShouldShoot = false;
                    return false;
                }
            }

            return true;
        }

        public override void Tick(EntityController entity){
            entity.ShouldShoot = true;
            entity.TargetPosition = Target.transform.position;
        }

        private bool FindTarget(EntityController entity){
            var hits = Physics.OverlapSphere(entity.transform.position, LookDistance, DetectionLayers);
            foreach(var hit in hits){
                if(hit.TryGetComponent(out Target)){
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, LookDistance);
        }
    }
}