using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviour{
        private void Awake(){
            Physics.gravity = Vector3.zero;
        }
    }
}
