using Ametrin.KunstBLL.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class Interactable : MonoBehaviour, IInteractable{
        public UnityEvent<IInteractor> OnInteract = new();

        public string GetDescription(IInteractor interactor) => "Interact";

        public void Interact(IInteractor interactor){
            OnInteract.Invoke(interactor);
        }
    }

    public interface IInteractor{
        ItemHolder MainHand {get;}
        // void PickUp(ItemStack item);
    }

    public interface IInteractable{
        public void Interact(IInteractor interactor);    
        public string GetDescription(IInteractor interactor);
    }
}
