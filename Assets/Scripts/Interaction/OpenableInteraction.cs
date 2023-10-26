using System.Collections;
using UnityEngine;
using Ametrin.Utils;
using Ametrin.Utils.Unity;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class OpenableInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private Transform target;
        [SerializeField] private bool IsOpen = false;
        [SerializeField] private float Duration = 1;
        [SerializeField, Setable(nameof(SetClosedState))] private Quaternion ClosedState;
        [SerializeField, Setable(nameof(SetOpenState))] private Quaternion OpenState;
        // [SerializeField] private Vector3 LocalAxis = Vector3.up;
        // [SerializeField] private float OpenedAngle = 90;
        // [SerializeField] private float ClosedAngle = 0;
        // // private float currentAngle = 0f;

        private void Start(){
            StartCoroutine(Animate());
        }

        public void Interact(IInteractor interactor){
            IsOpen = !IsOpen;
            StartCoroutine(Animate());
        }

        private IEnumerator Animate(){
            var startRotation = target.localRotation;
            // var currentAngle = GetRotationAroundAxis(startRotation, LocalAxis);

            // var targetAngle = (IsOpen ? OpenedAngle : ClosedAngle);
            // var angle = targetAngle - currentAngle;
            // var endRotation = startRotation * Quaternion.AngleAxis(angle, LocalAxis);
            var endRotation = IsOpen ? OpenState : ClosedState;
            // print($"Current: {currentAngle} Target: {targetAngle} Rotating {angle}");

            // var elapsedTime = 0f;
            // while (elapsedTime < Duration){
            //     target.localRotation = Quaternion.Slerp(startRotation, endRotation, SmoothStep(elapsedTime / Duration));
            //     elapsedTime += Time.deltaTime;
            //     yield return null;
            // }
            // target.localRotation = endRotation;

            var elapsedTime = 0f;
            while (elapsedTime < Duration){
                target.localRotation = Quaternion.Slerp(startRotation, endRotation, SmoothStep(elapsedTime / Duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.localRotation = endRotation;

            static float SmoothStep(float t) => Mathf.Clamp01(t * t * (3f - 2f * t));
        }

        // public static float GetRotationAroundAxis(Quaternion quaternion, Vector3 axis){
        //     var difference = Quaternion.Inverse(Quaternion.identity) * quaternion;
        //     difference.ToAngleAxis(out var angle, out var rotationAxis);

        //     if (Vector3.Dot(rotationAxis, axis) < 0){
        //         angle = -angle;
        //     }

        //     return angle;
        // }

        #if UNITY_EDITOR
        private void SetClosedState(){
            ClosedState = target.localRotation;
        }
        
        public void SetOpenState(){
            OpenState = target.localRotation;
        }
        #endif
    }
}
