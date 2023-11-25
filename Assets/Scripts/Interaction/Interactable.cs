using Ametrin.KunstBLL.Items;

namespace Ametrin.KunstBLL.Interaction{
    public interface IInteractable{
        public void Interact(IInteractor interactor);    
        public string GetDescription(IInteractor interactor, bool canInteract);
        public string GetDescription(IInteractor interactor) => GetDescription(interactor, CanInteract(interactor)); 
        public bool CanInteract(IInteractor interactor) => true;
    }
    
    public interface IInteractor{
        ItemHolder MainHand {get;}
        // void PickUp(ItemStack item);
    }
}
