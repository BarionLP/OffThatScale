using System;
using Ametrin.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Ametrin.SpaceZombies{
    public sealed class HealthManager : MonoBehaviour{
        [field: SerializeField] public float MaxHealth {get; private set;}
        public UnityEvent OnDeath;
        public UnityEvent<ValueChangeArg<float>> AfterHealthChanged;
        public UnityEvent<float> AfterDamaged;
        public UnityEvent<float> AfterHealed;
        private float _CurrentHealth;
        public float CurrentHealth {
            get => _CurrentHealth;
            private set{
                if(_CurrentHealth == value) return;
                var old = _CurrentHealth;
                _CurrentHealth = Mathf.Clamp(value, 0, MaxHealth);
                if(old == _CurrentHealth) return;

                AfterHealthChanged.Invoke(new(old, _CurrentHealth));
                if (old > _CurrentHealth){
                    AfterDamaged.Invoke(old - _CurrentHealth);
                } else{
                    AfterHealed.Invoke(_CurrentHealth - old);
                }

                if (_CurrentHealth <= 0){
                    OnDeath.Invoke();
                }
            }
        }

        private void Awake(){
            CurrentHealth = MaxHealth;
        }

        public void Damage(float amount, DamageType type = DamageType.Generic){
            CurrentHealth -= amount;
        }
        public void Heal(float amount){
            CurrentHealth += amount;
        }
    }

    [Flags]
    public enum DamageType { 
        Generic     = 1 << 0, 
        Laser       = 1 << 1, 
        Plasma      = 1 << 2,
    }
}
