using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ametrin.KunstBLL.GUI{
    public sealed class ShipScreenController : MonoBehaviour{      
        [SerializeField] private Color OverScale;
        [SerializeField] private Color InScale;
        [SerializeField] private Canvas Canvas;
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private Image Background;

        private bool IsOn = true;

        public void UpdateText(float currentWeight, float maxWeight){
            if(!IsOn) return;
            
            if(currentWeight > maxWeight){
                Text.text = $"Caution\nShip Damaged\nReduce Weight\n{currentWeight:F2}/{maxWeight}";
                Background.color = OverScale;
            }else{
                Text.text = $"Weight adjusted\n{currentWeight:F2}";
                Background.color = InScale;
            }
        }

        public void TurnScreenOff(){
            Canvas.enabled = false;
        }
    }
}
