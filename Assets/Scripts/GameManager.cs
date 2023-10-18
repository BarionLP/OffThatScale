using System;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviour{
        public static event Action OnGravityChange;
        
        private void Awake(){
            Physics.gravity = Vector3.zero;
            OnGravityChange?.Invoke();
        }
    }
}
