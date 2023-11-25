using System.Collections;
using Ametrin.KunstBLL.Entity;
using Ametrin.KunstBLL.Entity.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL{
    public sealed class InGameGUIController : MonoBehaviour{
        [SerializeField] private float FadeMultiplier = 1.2f;
        private VisualElement RootElement;
        private VisualElement OffWorldDisplay;

        private void Awake(){
            RootElement = GetComponent<UIDocument>().rootVisualElement;
            OffWorldDisplay = RootElement.Q<VisualElement>("OffWorld");
            OffWorldDisplay.style.display = DisplayStyle.None;
            WorldManager.OnPlayerLeftWorld += () => StartCoroutine(FadeOffWorldIn());
        }

        private void Start(){
            var interactionHint = RootElement.Q<Label>("InteractionHint");
            (PlayerManager.Instance.Controller as PlayerController).UpdateInteractionHint += hint => interactionHint.text = hint;
        }

        private IEnumerator FadeOffWorldIn(){
            var opacity = 0f;
            OffWorldDisplay.style.display = DisplayStyle.Flex;
            while(opacity < 1){
                yield return new WaitForEndOfFrame();
                opacity += Time.deltaTime * FadeMultiplier;
                OffWorldDisplay.style.opacity = opacity;
            }
        }
    }
}
