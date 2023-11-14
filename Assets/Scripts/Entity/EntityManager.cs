using Ametrin.KunstBLL.Entity.Controller;
using Ametrin.KunstBLL.Items;
using Ametrin.KunstBLL.Movement;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity{
    public abstract class EntityManager : MonoBehaviour, IEntity{
        public EntityController Controller { get; protected set; }
        public MovementController MovementController { get; protected set; }
        public HealthManager Health { get; protected set; }

        public void PickUp(ItemStack item) {
            Controller.MainHand.Item = item;
        }

        protected virtual void Awake(){
            Controller = GetComponent<EntityController>();
            MovementController = GetComponent<MovementController>();
            Health = GetComponent<HealthManager>();
        }

        private void OnFootstep() { }
        private void OnLand() { }
    }
}
