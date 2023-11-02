using System;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviour{
        public static event Action OnGravityChange;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        [SerializeField] private Vector3 InitalGravity;

        private void Awake(){
            Physics.gravity = InitalGravity;
            OnGravityChange?.Invoke();
        }
    }
}
