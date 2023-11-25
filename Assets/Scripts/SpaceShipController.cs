using Ametrin.KunstBLL.GUI;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class SpaceShipController : MonoBehaviour{
        [field: SerializeField] public float MaxShipWeight {get; private set; } = 1.3f;
        [field: SerializeField] public float CurrentShipWeight { get; private set; } = 1f;
        public bool IsOverweight => CurrentShipWeight > MaxShipWeight;

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
