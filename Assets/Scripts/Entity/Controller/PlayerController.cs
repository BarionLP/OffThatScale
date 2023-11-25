using System;
using Ametrin.Utils.Unity;
using Ametrin.KunstBLL.Input;
using Ametrin.KunstBLL.Interaction;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Controller{

    [RequireComponent(typeof(PlayerManager))]
    public sealed class PlayerController : EntityController{
        #pragma warning disable IDE0044
        [SerializeField] private float InteractionRadius = 0.5f;
        [SerializeField] private float InteractionOffset = 0.5f;
        [SerializeField] private LayerMask InteractionLayers;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Vector2 CameraClamp;
        [SerializeField] private float MouseSensitivity = 4;
        #pragma warning restore IDE0044

        public event Action<string> UpdateInteractionHint;
        public override Vector3 LookDirection => Camera.transform.forward;
        private float pitch = 0;
        [field: SerializeField] public Transform Camera {get; private set;}
        private IInteractable SelectedInteractable;

        protected override void Awake(){
            base.Awake();
            GameManager.OnCutSceneFinished += ()=>{
                UpdateInteractionHint.Invoke("WASD - Accelerate");
            };
        }

        public void Start(){
            Camera = UnityEngine.Camera.main.transform;
            PlayerInput.OnInteract += Interact;
            PlayerInput.OnUse += UseMainHandItem;
            PlayerInput.OnThrow += ThrowMainHandItem;
        }

        protected override void Update(){
            base.Update();

            var deltaMouse = PlayerInput.DeltaMouse;
            var deltaYaw = deltaMouse.x * MouseSensitivity * Time.deltaTime;
            var deltaPitch = deltaMouse.y * MouseSensitivity * Time.deltaTime;

            Rotation = transform.localRotation * (PlayerInput.ShouldRoll ? Quaternion.Euler(0, 0, deltaYaw) : Quaternion.Euler(0, deltaYaw, 0));
            if(PlayerInput.ShouldRoll){
                Rotation *= Quaternion.Euler(deltaPitch, 0, 0);
            }else{
                pitch = Mathf.Clamp(pitch + deltaPitch, CameraClamp.x, CameraClamp.y);
            }
        }

        private void LateUpdate(){
            Camera.position = CameraRoot.position;
            Camera.localRotation = transform.rotation * Quaternion.Euler(pitch, 0, 0);
        }

        private void FixedUpdate(){
            CheckInteraction();

            void CheckInteraction(){
                var colliders = Physics.OverlapSphere(Camera.position + Camera.forward * InteractionOffset, InteractionRadius, InteractionLayers);
                if (colliders.Length > 0 && colliders[0].TryGetComponent<IInteractable>(out var interactable)){
                    if (SelectedInteractable == interactable) return;
                    SelectedInteractable = interactable;
                }else{
                    if (SelectedInteractable is null) return;
                    SelectedInteractable = null;
                }
                UpdateInteractionHint?.Invoke(GetDescription());

                string GetDescription(){
                    if(SelectedInteractable is null) return string.Empty;

                    var canInteract = SelectedInteractable.CanInteract(Manager);
                    var hint = SelectedInteractable.GetDescription(Manager, canInteract);
                    return canInteract ? $"E - {hint}" : hint;
                }
            }
        }

        private void Interact(){
            SelectedInteractable?.Interact(Manager);
        }

        public override bool IsMoving => PlayerInput.Move != Vector2.zero;
        public override Vector2 Move => PlayerInput.Move;
        public override bool IsSprinting => PlayerInput.IsSprinting;
        public override bool ShouldJump => PlayerInput.ShouldJump;
        public override Vector3 Acceleration => PlayerInput.Acceleration;
        public override bool ShouldRoll => PlayerInput.ShouldRoll;
        public override bool ShouldSlowDown => PlayerInput.ShouldSlowDown;
        public override Quaternion CameraRotation => Camera.rotation;

        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Camera.position + Camera.forward * InteractionOffset, InteractionRadius);
        }
    }
}
