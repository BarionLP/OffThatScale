using UnityEngine;
using UnityEngine.SceneManagement;
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

            var startButton = Document.rootVisualElement.Q<Button>("StartButton");
            startButton.RegisterCallback<ClickEvent>(e => {
                SceneManager.LoadScene(Scene.Game);
            });
        }
    }
}
