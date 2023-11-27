using UnityEngine;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class ShipControllInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private SpaceShipController shipController;
        
        public string GetDescription(IInteractor interactor, bool canInteract){
            return canInteract ? "Start" : "Reduce Weight";
        }

        public bool CanInteract(IInteractor interactor) => !shipController.IsOverweight;

        public void Interact(IInteractor interactor) {
            if(shipController.IsOverweight) return;

            shipController.StartEngine();
        }
    }
}
