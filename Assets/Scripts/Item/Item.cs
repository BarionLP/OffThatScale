using UnityEngine;

namespace Ametrin.KunstBLL.Items{
    [CreateAssetMenu(menuName = "Item/Simple")]
    public class Item : ScriptableObject{
        [field: SerializeField] public GameObject MeshPrefab {get; private set;}
        [field: SerializeField] public Holding Holding {get; private set;}
        [field: SerializeField] public string DisplayName {get; private set;}
        [field: SerializeField] public float UsageCooldown {get; private set;}
        
        public virtual void OnUse(ItemUseContext context) {
            Debug.Log($"Used {name}");
        }

    }

    public enum Holding {Push, InHand, Front}

    public interface IItemUser{
        public Vector3 Position {get;}
    }

    public record ItemUseContext{
        public IItemUser User { get; }
        public Vector3 ItemPositon { get;}
        public Vector3 LookDirection { get; }
        public ItemHolder Holder { get; }
        public ItemStack Stack { get; }

        public ItemUseContext(IItemUser user, ItemHolder holder, Vector3 itemPositon, Vector3 lookingDirection, ItemStack stack){
            User = user;
            ItemPositon = itemPositon;
            LookDirection = lookingDirection;
            Holder = holder;
            Stack = stack;
        }
    }
}