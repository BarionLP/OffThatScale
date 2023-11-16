using TMPro;
using UnityEngine;

namespace Ametrin.KunstBLL.GUI{
    public sealed class ShipScreenController : MonoBehaviour{      
        [SerializeField] private TextMeshProUGUI Text;

        public void UpdateText(float currentWeight, float maxWeight){
            Text.text = $"Caution\nShip Damaged\nReduce Weight\n{currentWeight}/{maxWeight}";
        }
    }
}
