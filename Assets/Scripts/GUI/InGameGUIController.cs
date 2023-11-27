using System.Collections;
using Ametrin.KunstBLL.Entity;
using Ametrin.KunstBLL.Entity.Controller;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL{
    public sealed class InGameGUIController : MonoBehaviour{
        [SerializeField] private float FadeMultiplier = 1.2f;
        private VisualElement RootElement;
        private VisualElement EndingDisplay;

        private void Awake(){
            RootElement = GetComponent<UIDocument>().rootVisualElement;
            EndingDisplay = RootElement.Q<VisualElement>("Ending");
            EndingDisplay.style.display = DisplayStyle.None;
            WorldManager.OnPlayerLeftWorld += () => StartCoroutine(FadeEndIn("You've wandered to far<br>There is no way back"));
            GameManager.OnGameCompleted += () => StartCoroutine(FadeEndIn("You managed to get on the Planet.<br>Will you figure out what happend?"));
        }

        private void Start(){
            var interactionHint = RootElement.Q<Label>("InteractionHint");
            (PlayerManager.Instance.Controller as PlayerController).UpdateInteractionHint += hint => interactionHint.text = hint;
        }

        private IEnumerator FadeEndIn(string text){
            EndingDisplay.Q<Label>().text = text;
            var opacity = 0f;
            EndingDisplay.style.display = DisplayStyle.Flex;
            while (opacity < 1){
                yield return new WaitForEndOfFrame();
                opacity += Time.deltaTime * FadeMultiplier;
                EndingDisplay.style.opacity = opacity;
            }
        }
    }
}
