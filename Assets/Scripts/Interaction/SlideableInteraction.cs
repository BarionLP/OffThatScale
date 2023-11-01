using System.Collections;
using UnityEngine;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class SlideableInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private Transform target;
        [SerializeField] private bool IsOpen = false;
        [SerializeField] private float Duration = 1;
        [SerializeField, Setable(nameof(SetClosedState))] private Vector3 ClosedState;
        [SerializeField, Setable(nameof(SetOpenState))] private Vector3 OpenState;

        private void Start(){
            StartCoroutine(Animate());
        }

        public void Interact(IInteractor interactor){
            IsOpen = !IsOpen;
            StartCoroutine(Animate());
        }

        private IEnumerator Animate(){
            var startRotation = target.localPosition;
            var endRotation = IsOpen ? OpenState : ClosedState;

            var elapsedTime = 0f;
            while (elapsedTime < Duration){
                target.localPosition = Vector3.Lerp(startRotation, endRotation, SmoothStep(elapsedTime / Duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.localPosition = endRotation;

            static float SmoothStep(float t) => Mathf.Clamp01(t * t * (3f - 2f * t));
        }

        private void SetClosedState(){
            ClosedState = target.localPosition;
        }

        public void SetOpenState(){
            OpenState = target.localPosition;
        }
    }
}
