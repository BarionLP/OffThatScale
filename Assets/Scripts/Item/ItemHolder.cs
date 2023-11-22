using Ametrin.Utils.Unity;
using UnityEngine;
using UnityEngine.VFX;

namespace Ametrin.KunstBLL.Items{
    public sealed class ItemHolder : MonoBehaviour{
        private const int HOLDING_LAYER = 8;
        private const int DEFAULT_LAYER = 6;
        [SerializeField, InlineEditor] private ItemStack _Item = ItemStack.Empty();
        public VisualEffect Effect {get; private set;}
        public ItemStack Item {
            get => _Item;
            set{
                DismountItem();

                _Item = value;
                
                if (_Item.IsEmpty) return;
                _Item.Object.transform.SetParent(transform);
                _Item.Object.transform.localPosition = Vector3.zero;
                _Item.Object.layer = HOLDING_LAYER;
                _Item.Object.TryGetComponent<Rigidbody>().Resolve(collider => collider.isKinematic = true);
                _Item.Object.TryGetComponent<VisualEffect>().Resolve(effect => Effect = effect, ()=> Effect = null);
            }
        }
        private void Awake(){
            Item = _Item; //Trigger an update;
        }
    
        public void Interact(IItemUser user, Vector3 lookDirection){
            Item.Type.OnUse(new(user, this, transform.position, lookDirection, Item));
        }

        public void DismountItem(){
            if (_Item.IsEmpty) return;

            _Item.Object.transform.SetParent(GameManager.Instance.WorldRoot, true);
            _Item.Object.layer = DEFAULT_LAYER;
            _Item.Object.TryGetComponent<Rigidbody>().Resolve(collider => collider.isKinematic = false);
        }
    }
}
