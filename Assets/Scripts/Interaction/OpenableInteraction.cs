using System.Collections;
using UnityEngine;
using Ametrin.Utils;
using Ametrin.Utils.Unity;

namespace Ametrin.KunstBLL.Interaction{
    public sealed class OpenableInteraction : MonoBehaviour, IInteractable{
        [SerializeField] private Transform target;
        [SerializeField] private bool IsOpen = false;
        [SerializeField] private float Duration = 1;
        [SerializeField] private Vector3 LocalAxis = Vector3.up;
        [SerializeField] private float TargetAngle = 90;
        private float currentAngle = 0f;

        private void Start(){
            StartCoroutine(Animate());
        }

        public void Interact(IInteractor interactor){
            IsOpen = !IsOpen;
            StartCoroutine(Animate());
        }

        private IEnumerator Animate(){
            var targetAngle = IsOpen ? 0 : TargetAngle;
            var angle = targetAngle - currentAngle;
            print(angle);
            var rotationSpeed = angle / Duration;
            print(rotationSpeed);
            var timer = Duration;
            while(timer > 0){
                var step = rotationSpeed * Time.deltaTime;
                print(step);
                // step = Mathf.Clamp(step, -Mathf.Abs(angle), Mathf.Abs(angle));
                target.Rotate(LocalAxis, step, Space.Self);
                currentAngle += step;
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }


            // var startRotation = target.localRotation;
            // var worldAxis = target.TransformDirection(LocalAxis);
            // var endRotation = Quaternion.AngleAxis(TargetAngle, worldAxis) * startRotation;
            // var elapsedTime = 0f;
            // print(startRotation.eulerAngles);
            // print(endRotation.eulerAngles);
            // print(target.localEulerAngles);
            // print(target.eulerAngles);

            // while (elapsedTime < Duration){
            //     target.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / Duration);
            //     elapsedTime += Time.deltaTime;
            //     yield return null;
            // }

            // target.localRotation = endRotation;
        }
    }
}
