using System.Collections;
using UnityEngine;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class OpenableInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private Transform target;
        [SerializeField] private bool IsOpen = false;
        [SerializeField] private float Duration = 1;
        [SerializeField, Setable(nameof(SetClosedState))] private Quaternion ClosedState;
        [SerializeField, Setable(nameof(SetOpenState))] private Quaternion OpenState;

        private AudioSource SFX;

        private void Awake(){
            TryGetComponent(out SFX);
        }

        private void Start(){
            StartCoroutine(Animate());
        }

        public void Interact(IInteractor interactor){
            IsOpen = !IsOpen;
            StopCoroutine(nameof(Animate));
            StartCoroutine(Animate());
            SFX?.Play();
        }

        private IEnumerator Animate(){
            var startRotation = target.localRotation;
            var endRotation = IsOpen ? OpenState : ClosedState;

            var elapsedTime = 0f;
            while (elapsedTime < Duration){
                target.localRotation = Quaternion.Slerp(startRotation, endRotation, SmoothStep(elapsedTime / Duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.localRotation = endRotation;

            static float SmoothStep(float t) => Mathf.Clamp01(t * t * (3f - 2f * t));
        }

        private void SetClosedState(){
            ClosedState = target.localRotation;
        }
        
        public void SetOpenState(){
            OpenState = target.localRotation;
        }
    }
}
