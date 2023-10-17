using Ametrin.Utils.Unity;
using UnityEngine;
using UnityEngine.VFX;

namespace Ametrin.KunstBLL.Items{
    public sealed class ItemHolder : MonoBehaviour{
        [SerializeField, InlineEditor] private Item _Item;
        // public UnityEvent OnInteract;
        private GameObject ItemInstance;
        public VisualEffect Effect {get; private set;}
        public Item Item {
            get => _Item;
            set{
                _Item = value;
                if (ItemInstance != null) Destroy(ItemInstance);
                
                if (_Item == null) return;
                if (_Item.MeshPrefab == null) return;

                ItemInstance = Instantiate(_Item.MeshPrefab, transform);
                
                if(ItemInstance.TryGetComponent<VisualEffect>(out var effect)){
                    Effect = effect;
                }
            }
        }
        private void Awake(){
            Item = _Item; //Trigger update;
        }
    
        public void Interact(IItemUser user, Vector3 lookDirection){
            Item.OnUse(new(user, this, transform.position, lookDirection));
        }
    }
}
