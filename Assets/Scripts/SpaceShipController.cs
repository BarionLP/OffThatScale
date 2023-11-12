using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class SpaceShipController : MonoBehaviour{
        [SerializeField] private float MaxShipWeight;
        [SerializeField] private float CurrentShipWeight;

        private ShipScreenController ScreenController;

        private void Awake(){
            ScreenController = GetComponentInChildren<ShipScreenController>();
            // var trigger = GetComponent<BoxCollider>();
        }

        private void Start(){
            ScreenController.UpdateText(CurrentShipWeight, MaxShipWeight);
        }

        public void UpdateText(){
            ScreenController.UpdateText(CurrentShipWeight, MaxShipWeight);
        }

        private void OnTriggerEnter(Collider enterned){
            if(!enterned.TryGetComponent<WeightObject>(out var weightObject)) return;
            CurrentShipWeight += weightObject.Weight;
            UpdateText();
        }

        private void OnTriggerExit(Collider enterned){
            if(!enterned.TryGetComponent<WeightObject>(out var weightObject)) return;
            CurrentShipWeight -= weightObject.Weight;
            UpdateText();
        }
    }
}
