using System;
using System.Collections.Generic;
using Ametrin.KunstBLL.GUI;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class SpaceShipController : MonoBehaviour{
        public event Action OnEngineStarted;
        [field: SerializeField] public float MaxShipWeight {get; private set; } = 1.3f;
        [field: SerializeField] public float CurrentShipWeight { get; private set; } = 1f;
        public bool IsOverweight => CurrentShipWeight > MaxShipWeight;

        private ShipScreenController ScreenController;
        private readonly List<Collider> InShip = new();


        private void Awake(){
            ScreenController = GetComponentInChildren<ShipScreenController>();
        }

        private void Start(){
            ScreenController.UpdateText(CurrentShipWeight, MaxShipWeight);
        }

        public void UpdateText(){
            ScreenController.UpdateText(CurrentShipWeight, MaxShipWeight);
        }

        public void StartEngine(){
            OnEngineStarted?.Invoke();
            foreach(var collider in InShip){
                collider.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider enterned){
            InShip.Add(enterned);
            if(!enterned.TryGetComponent<WeightObject>(out var weightObject)) return;
            CurrentShipWeight += weightObject.Weight;
            UpdateText();
        }

        private void OnTriggerExit(Collider exited){
            InShip.Remove(exited);
            if(!exited.TryGetComponent<WeightObject>(out var weightObject)) return;
            CurrentShipWeight -= weightObject.Weight;
            UpdateText();
        }
    }
}
