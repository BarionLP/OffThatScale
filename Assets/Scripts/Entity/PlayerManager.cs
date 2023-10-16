using Ametrin.Console;
using Ametrin.SpaceZombies.Input;
using UnityEngine;

namespace Ametrin.SpaceZombies.Entity{
    public sealed class PlayerManager : EntityManager{
        public static PlayerManager Instance;

        protected override void Awake(){
            base.Awake();
            if(Instance != null){
                Debug.LogError("Having Two Players...");
                DestroyImmediate(Instance.gameObject);
            }
            Instance = this;
            
            // Controller = base.Controller as PlayerController;
            Health.AfterDamaged.AddListener(amount => Debug.Log($"Damaged by {amount}"));
            Health.AfterHealed.AddListener(amount => Debug.Log($"Healed by {amount}"));
            Health.OnDeath.AddListener(() => Debug.Log($"You Died"));
        }

        private void Start(){

            Cursor.lockState = CursorLockMode.Locked;
            if(!ConsoleManager.IsVisible) PlayerInput.Enable();
            ConsoleManager.OnHide += PlayerInput.Enable;
            ConsoleManager.OnShow += PlayerInput.Disable;
        }
    }
}
