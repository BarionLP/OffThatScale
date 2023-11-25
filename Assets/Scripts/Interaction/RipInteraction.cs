using Ametrin.KunstBLL.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class RipInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private UnityEvent OnRipped = new();
        [SerializeField] private Item CorrectItem;
        public void Interact(IInteractor interactor){
            if(!CanInteract(interactor)) return; //sniff sniff
            GetComponent<Rigidbody>().isKinematic = false;
            transform.SetParent(GameManager.Instance.WorldRoot);
            OnRipped.Invoke();
            Destroy(this);
        }

        public bool CanInteract(IInteractor interactor) => interactor.MainHand.Item.Type == CorrectItem;

        public string GetDescription(IInteractor interactor, bool canInteract){
            return canInteract ? "Rip out" : $"requires {CorrectItem.DisplayName}";
        }

    }
}
