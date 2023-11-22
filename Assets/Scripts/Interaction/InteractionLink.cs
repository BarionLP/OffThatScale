using Ametrin.KunstBLL.Interaction;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class InteractionLink : MonoBehaviour, IInteractable{
        [SerializeField] private MonoBehaviour _Linked; // stupid Unity
        private IInteractable Linked;

        private void Awake(){
            if(_Linked == this){
                Debug.LogError("Interaction is linked to itself", this);
                return;
            }

            if(_Linked is not IInteractable linked){
                Debug.LogError("Interaction linked to non Interactable");
                return;
            }

            Linked = linked;
        }

        public void Interact(IInteractor interactor) => Linked?.Interact(interactor);
        public string GetDescription(IInteractor interactor) => Linked.GetDescription(interactor);
    }
}
