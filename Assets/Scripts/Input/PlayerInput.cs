using System;
using UnityEngine;

namespace Ametrin.KunstBLL.Input{
    public static class PlayerInput{
        private static readonly PlayerInputActions InputActions = new();

        public static event Action OnInteract;
        public static event Action OnUse;

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
        public static bool ShouldSlowDown => InputActions.Gravityless.Stop.IsPressed();


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
            if(GameManager.IsZeroG){
                EnableGravityless();
            }else{
                EnableGravity();
            }
            GameManager.OnGravityChange += OnGravityChange;
        }

        public static void EnableGravity() => InputActions.Gravity.Enable();
        public static void EnableGravityless() => InputActions.Gravityless.Enable();
        public static void EnableGeneral() => InputActions.General.Enable();
        public static void Disable(){
            InputActions.Disable();
            GameManager.OnGravityChange -= OnGravityChange;
        }

        public static void DisableGravity() => InputActions.Gravity.Disable();
        public static void DisableGravityless() => InputActions.Gravityless.Disable();
        public static void DisableGeneral() => InputActions.General.Disable();

        private static void OnGravityChange(){
            if (GameManager.IsZeroG){
                EnableGravityless();
            } else{
                EnableGravity();
            }
        }
    }
}
