using System;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviour{
        public static event Action OnGravityChange;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        private void Awake(){
            Physics.gravity = Vector3.zero;
            OnGravityChange?.Invoke();
        }
    }
}
