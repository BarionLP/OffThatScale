using UnityEngine;

namespace Ametrin.KunstBLL.Entity{
    [RequireComponent(typeof(HealthManager))]
    public sealed class AlienManager : EntityManager{
        public Vector3 TargetPosition { get; set; }

        protected override void Awake(){
            Health.OnDeath.AddListener(OnDeath);
        }

        private void OnDeath(){
            // NetworkObject.Despawn();
        }
    }
}
