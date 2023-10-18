using Ametrin.KunstBLL.Input;
using Ametrin.KunstBLL.Interaction;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Controller{

    [RequireComponent(typeof(PlayerManager))]
    public sealed class PlayerController : EntityController{
        #pragma warning disable IDE0044
        [SerializeField] private float InteractionDistance = 1;
        [SerializeField] private LayerMask InteractionLayers;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Vector2 CameraClamp;
        [SerializeField] private float MouseSensitivity = 4;
        #pragma warning restore IDE0044

        public override Vector3 LookDirection => Camera.transform.forward;
        private float pitch = 0;
        private float yaw = 0;
        public Transform Camera {get; private set;}

        public void Start(){
            Camera = UnityEngine.Camera.main.transform;
            PlayerInput.OnInteract += Interact;
            PlayerInput.OnUse += UseMainHandItem;
        }

        protected override void Update(){
            base.Update();
            var deltaMouse = PlayerInput.DeltaMouse;
            yaw += deltaMouse.x * MouseSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch + (deltaMouse.y * MouseSensitivity * Time.deltaTime), CameraClamp.x, CameraClamp.y);

            Rotation = Quaternion.Euler(0, yaw, 0);
        }

        private void LateUpdate(){
            Camera.localEulerAngles = new(pitch, yaw, 0);
            Camera.position = CameraRoot.position;
        }

        private void Interact(){
            if (!Physics.Raycast(Camera.position, LookDirection, out var hit, InteractionDistance, InteractionLayers)) return;
            if (!hit.collider.TryGetComponent<IInteractable>(out var interactable)) return;

            interactable.Interact(Manager);
        }

        public override bool IsMoving => PlayerInput.Move != Vector2.zero;
        public override Vector2 Move => PlayerInput.Move;
        public override bool IsSprinting => PlayerInput.IsSprinting;
        public override bool ShouldJump => PlayerInput.ShouldJump;
        public override Vector3 Acceleration => PlayerInput.Acceleration;
        public override bool ShouldRoll => PlayerInput.ShouldRoll;
    }
}
