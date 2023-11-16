using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL.GUI{
    [RequireComponent(typeof(UIDocument))]
    public sealed class MainMenuController : MonoBehaviour{
        private UIDocument Document;
        private void Awake(){
            Document = GetComponent<UIDocument>(); 
            var quitButton = Document.rootVisualElement.Q<Button>("QuitButton");
            quitButton.RegisterCallback<ClickEvent>(e => {
                Application.Quit();
            });
        }
    }
}
