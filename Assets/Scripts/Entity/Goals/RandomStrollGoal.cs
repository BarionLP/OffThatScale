using Ametrin.SpaceZombies.Entity.Controller;
using UnityEngine;

namespace Ametrin.SpaceZombies.Entity.Goals{
    public sealed class RandomStrollGoal : Goal{
        [SerializeField, Range(0, 1)] private float NewTargetPropability = 0.1f;
        [SerializeField, Range(5, 50)] private float Range = 0.1f;

        protected override bool ShouldTick(EntityController entity){
            return Random.Range(0f, 1f) <= NewTargetPropability;
        }

        public override void Tick(EntityController entity){
            entity.TargetPosition = entity.transform.position + new Vector3(Random.Range(-Range, Range), 0, Random.Range(-Range, Range));
        }

        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}
