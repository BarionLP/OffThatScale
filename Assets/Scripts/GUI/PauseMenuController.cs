using UnityEngine;
using UnityEngine.UIElements;

namespace Ametrin.KunstBLL{
    [RequireComponent(typeof(UIDocument))]
    public sealed class PauseMenuController : MonoBehaviour{
        private VisualElement RootElement;
        
        private void Awake(){
            RootElement = GetComponent<UIDocument>().rootVisualElement;
            
            var continueButton = RootElement.Q<Button>("ContinueButton");
            continueButton.RegisterCallback<ClickEvent>(e=>{
                GameManager.PauseToggle();
            });

            var settingsButton = RootElement.Q<Button>("SettingsButton");

            var exitButton = RootElement.Q<Button>("ExitButton");
            exitButton.RegisterCallback<ClickEvent>(e=>{
                SceneManager.LoadScene(Scene.Start);
            });

            var quitButton = RootElement.Q<Button>("QuitButton");
            quitButton.RegisterCallback<ClickEvent>(e=>{
                Application.Quit();
            });
        }

        public void Show(){
            RootElement.style.display = DisplayStyle.Flex;
        }

        public void Hide(){
            RootElement.style.display = DisplayStyle.None;
        }
    }
}
