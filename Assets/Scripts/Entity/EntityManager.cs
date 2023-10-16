using Ametrin.SpaceZombies.Entity.Controller;
using Ametrin.SpaceZombies.Movement;
using UnityEngine;

namespace Ametrin.SpaceZombies.Entity{
    public abstract class EntityManager : MonoBehaviour, IEntity{
        public EntityController Controller { get; protected set; }
        public MovementController MovementController { get; protected set; }
        public HealthManager Health { get; protected set; }

        protected virtual void Awake(){
            Controller = GetComponent<EntityController>();
            MovementController = GetComponent<MovementController>();
            Health = GetComponent<HealthManager>();
        }

        private void OnFootstep() { }
        private void OnLand() { }
    }
}
