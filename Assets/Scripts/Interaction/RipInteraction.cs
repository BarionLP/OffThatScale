using Ametrin.KunstBLL.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class RipInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private UnityEvent OnRipped = new();
        [SerializeField] private Item CorrectItem;
        public void Interact(IInteractor interactor){
            if(interactor.MainHand.Item.Type != CorrectItem) return; //sniff sniff
            GetComponent<Rigidbody>().isKinematic = false;
            transform.SetParent(GameManager.Instance.WorldRoot);
            OnRipped.Invoke();
            Destroy(this);
        }

        public string GetDescription(IInteractor interactor){
            return interactor.MainHand.Item.Type == CorrectItem ? "Rip out" : $"requires {CorrectItem.DisplayName}";
        }
    }
}
