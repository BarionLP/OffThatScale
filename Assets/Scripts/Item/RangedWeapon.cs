using Ametrin.KunstBLL.Items;
using UnityEngine;

namespace Ametrin.KunstBLL
{

    [CreateAssetMenu(menuName = "Item/Ranged Weapon")]
    public sealed class RangedWeapon : Item{
#pragma warning disable IDE0044
        [SerializeField] private float MaxShootDistance = 10;
        [SerializeField] private float Damage = 5;
        [SerializeField] private DamageType DamageType; 
        [SerializeField] private LayerMask HittableLayers;
        private readonly int VFXHitPosition = Shader.PropertyToID("TargetPoint_position");
        private readonly int VFXUseEvent = Shader.PropertyToID("OnUse");
#pragma warning restore IDE0044

        public override void OnUse(ItemUseContext context){
            var position = context.ItemPositon;
            var shootVector = context.LookDirection * MaxShootDistance;
            var isHit = Physics.Raycast(position, shootVector, out var hit, HittableLayers);
            context.Holder.Effect.SetVector3(VFXHitPosition, isHit ? hit.point : position + shootVector);
            context.Holder.Effect.SendEvent(VFXUseEvent);
            if(!isHit) return;
            if(!hit.collider.TryGetComponent<HealthManager>(out var healthManager)) return;
            healthManager.Damage(Damage, DamageType);

        } 
    }
}
