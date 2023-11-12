using Ametrin.KunstBLL.Items;
using UnityEngine;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class PickUpInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private Item Item;
        
        public void Interact(IInteractor interactor){
            interactor.PickUp(Item);
        }
    }
}
