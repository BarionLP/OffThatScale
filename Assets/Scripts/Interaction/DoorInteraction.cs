using UnityEngine;

namespace Ametrin.SpaceZombies.Interaction{
    [RequireComponent(typeof(Animator))]
    public sealed class DoorInteraction : MonoBehaviour{
        [SerializeField] private bool IsOpen = false;
        private Animator Animator;
        
        private void Awake(){
            Animator = GetComponent<Animator>();
        }

        public void OnInteract(IInteractor interactor){
            IsOpen = !IsOpen;
            UpdateAnimation();
        }

        private void UpdateAnimation(){
            if (IsOpen){
                Animator.SetTrigger(OpenTriggerID);
            }else {
                Animator.SetTrigger(CloseTriggerID);
            }
        }

        private static readonly int OpenTriggerID = Animator.StringToHash("Open");
        private static readonly int CloseTriggerID = Animator.StringToHash("Close");
    }
}
