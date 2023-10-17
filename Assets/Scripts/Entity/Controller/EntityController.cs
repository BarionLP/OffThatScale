using Ametrin.KunstBLL.Items;
using Ametrin.KunstBLL.Movement;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Controller{

    [RequireComponent(typeof(EntityManager))]
    public abstract class EntityController : MonoBehaviour, IMovementInput{
        public const int INVALID_TARGET_Y = -100;
        public bool ShouldShoot { get; set; }
        public Vector3 TargetPosition { get; set; } = new (0, INVALID_TARGET_Y, 0);
        public abstract Vector3 LookDirection { get; }

        protected EntityManager Manager;
        private ItemHolder MainHand;
        private float ItemCooldown;

        protected virtual void Awake(){
            Manager = GetComponent<EntityManager>();
            MainHand = GetComponentInChildren<ItemHolder>();
        }

        protected virtual void Update(){
            if(ItemCooldown > 0) ItemCooldown -= Time.deltaTime;
        }

        protected void UseMainHandItem(){
            if(ItemCooldown > 0 || MainHand.Item == null) return;
            ItemCooldown = MainHand.Item.UsageCooldown;
            MainHand.Interact(Manager, LookDirection);
        }

        public virtual bool IsMoving { get; protected set; }
        public virtual Vector2 Move { get; protected set; }
        public virtual bool IsSprinting { get; protected set; }
        public virtual bool ShouldJump { get; protected set; }
        public virtual Quaternion Rotation { get; protected set; }
    }
}
