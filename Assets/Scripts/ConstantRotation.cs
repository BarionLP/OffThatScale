using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class ConstantRotation : MonoBehaviour{
        [SerializeField] private Vector3 Rotation;
        
        private void Update(){
            transform.Rotate(Rotation * Time.deltaTime);
        }
    }
}
