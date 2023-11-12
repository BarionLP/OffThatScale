using Ametrin.KunstBLL.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class Interactable : MonoBehaviour, IInteractable{
        public UnityEvent<IInteractor> OnInteract = new();

        public void Interact(IInteractor interactor){
            OnInteract.Invoke(interactor);
        }
    }

    public interface IInteractor{
        void PickUp(Item item);
    }

    public interface IInteractable{
        public void Interact(IInteractor interactor);
    }
}
