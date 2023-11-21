using Ametrin.Console;
using Ametrin.KunstBLL.Input;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity{
    public sealed class PlayerManager : EntityManager{
        public static PlayerManager Instance;

        protected override void Awake(){
            base.Awake();
            if(Instance != null){
                Debug.LogError("Two Players...");
                DestroyImmediate(Instance.gameObject);
            }
            Instance = this;            
        }

        private void Start(){
            Health.AfterDamaged.AddListener(amount => Debug.Log($"Damaged by {amount}"));
            Health.AfterHealed.AddListener(amount => Debug.Log($"Healed by {amount}"));
            Health.OnDeath.AddListener(() => Debug.Log($"You Died"));

            ConsoleManager.OnHide += PlayerInput.Enable;
            ConsoleManager.OnShow += PlayerInput.Disable;
        }
    }
}
