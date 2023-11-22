using Ametrin.KunstBLL.Entity;
using Ametrin.KunstBLL.Entity.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL{
    public sealed class InGameGUIController : MonoBehaviour{
        private VisualElement RootElement;

        private void Awake(){
            RootElement = GetComponent<UIDocument>().rootVisualElement;
        }

        private void Start(){
            var interactionHint = RootElement.Q<Label>("InteractionHint");
            (PlayerManager.Instance.Controller as PlayerController).UpdateInteractionHint += hint => interactionHint.text = hint;
        }
    }
}
