using System;
using UnityEngine;

namespace Ametrin.KunstBLL.Input{
    public static class PlayerInput{
        private static readonly PlayerInputActions InputActions = new();

        public static event Action OnInteract;
        public static event Action OnUse;

        static PlayerInput(){
            InputActions.Player.Interact.performed += (_)=> OnInteract?.Invoke();
            InputActions.Player.Use.performed += (_)=> OnUse?.Invoke();
        }

        public static Vector2 Move => InputActions.Player.Move.ReadValue<Vector2>();
        public static bool IsSprinting => InputActions.Player.Sprint.IsPressed();
        public static bool ShouldJump => InputActions.Player.Jump.IsPressed();
        public static Vector2 DeltaMouse => InputActions.Player.Look.ReadValue<Vector2>();

        public static void Enable() => InputActions.Enable();
        public static void Disable() => InputActions.Disable();
    }
}
