using System;
using Ametrin.KunstBLL.Entity;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class WorldManager : MonoBehaviour{
        public static event Action OnPlayerLeftWorld; 
        private void OnTriggerExit(Collider collider){
            if(collider.TryGetComponent<PlayerManager>(out _)){
                OnPlayerLeftWorld?.Invoke();
                return;
            }

            // Destroy(collider.gameObject);
            // Debug.Log("Destroyed Object", collider);
        }        
    }
}
