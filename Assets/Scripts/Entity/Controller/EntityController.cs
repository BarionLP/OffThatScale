using Ametrin.KunstBLL.Items;
using Ametrin.KunstBLL.Movement;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Controller{

    [RequireComponent(typeof(EntityManager))]
    public abstract class EntityController : MonoBehaviour, IMovementInput, IGravitylessMovementInput{
        public bool ShouldShoot { get; set; }
        public Vector3? TargetPosition { get; set; } = null;
        public abstract Vector3 LookDirection { get; }

        protected EntityManager Manager;
        public ItemHolder MainHand {get; private set;}
        private float ItemCooldown;

        protected virtual void Awake(){
            Manager = GetComponent<EntityManager>();
            MainHand = GetComponentInChildren<ItemHolder>();
            GameManager.OnGravityChange += OnGravityChange;
        }

        protected virtual void Update(){
            if(ItemCooldown > 0) ItemCooldown -= Time.deltaTime;
        }

        protected void UseMainHandItem(){
            if(ItemCooldown > 0 || MainHand.Item.IsEmpty) return;
            ItemCooldown = MainHand.Item.Type.UsageCooldown;
            MainHand.Interact(Manager, LookDirection);
        }

        protected void ThrowMainHandItem(){
            if(MainHand.Item.IsEmpty) return;
            MainHand.DismountItem();
        }

        protected virtual void OnGravityChange(){
            if(!GameManager.IsZeroG){
                UpVector = -Physics.gravity.normalized;
            }
        }

        public virtual bool IsMoving { get; protected set; }
        public virtual Vector2 Move { get; protected set; }
        public virtual bool IsSprinting { get; protected set; }
        public virtual bool ShouldJump { get; protected set; }
        public virtual Quaternion Rotation { get; protected set; }
        
        public virtual Vector3 Acceleration { get; protected set; }
        public virtual bool ShouldRoll { get; protected set; }
        public virtual bool ShouldSlowDown { get; protected set; }
        public virtual Vector3 UpVector { 
            get => transform.up; 
            set => transform.up = value; 
        }
        public virtual Quaternion CameraRotation => Rotation;
    }
}
