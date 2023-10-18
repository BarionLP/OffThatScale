using System;
using UnityEngine;

namespace Ametrin.KunstBLL.Input{
    public static class PlayerInput{
        private static readonly PlayerInputActions InputActions = new();

        public static event Action OnInteract;
        public static event Action OnUse;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        static PlayerInput(){
            InputActions.General.Interact.performed += (_)=> OnInteract?.Invoke();
            InputActions.General.Use.performed += (_)=> OnUse?.Invoke();
        }

        public static Vector2 Move => InputActions.Gravity.Move.ReadValue<Vector2>();
        public static bool IsSprinting => InputActions.Gravity.Sprint.IsPressed();
        public static bool ShouldJump => InputActions.Gravity.Jump.IsPressed();
        public static Vector2 DeltaMouse => InputActions.General.Look.ReadValue<Vector2>();
        public static Vector3 Acceleration => InputActions.Gravityless.Move.ReadValue<Vector3>();
        public static bool ShouldRoll => InputActions.Gravityless.Roll.IsPressed();


        public static void SwitchToGravity(){
            DisableGravityless();
            EnableGravity();
        }
        public static void SwitchToGravityless(){
            DisableGravity();
            EnableGravityless();
        }

        public static void Enable(){
            EnableGeneral();
            if(IsZeroG){
                Debug.Log("as");
                EnableGravityless();
            }else{
                EnableGravity();
            }
        }

        public static void EnableGravity() => InputActions.Gravity.Enable();
        public static void EnableGravityless() => InputActions.Gravityless.Enable();
        public static void EnableGeneral() => InputActions.General.Enable();
        public static void Disable() => InputActions.Disable();
        public static void DisableGravity() => InputActions.Gravity.Disable();
        public static void DisableGravityless() => InputActions.Gravityless.Disable();
        public static void DisableGeneral() => InputActions.General.Disable();
    }
}
