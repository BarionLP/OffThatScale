using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class ShipScreenController : MonoBehaviour{
        [SerializeField] private float MaxShipWeight;
        [SerializeField] private float CurrentShipWeight;
        
        [SerializeField] private TextMeshProUGUI Text;

        private void Awake(){
            Text.text = $"Caution\nShip Damaged\nReduce Weight\n{CurrentShipWeight}/{MaxShipWeight}";
        }
    }
}
