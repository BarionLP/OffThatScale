using System;
using Ametrin.KunstBLL.Entity;
using Ametrin.KunstBLL.Entity.Controller;
using Ametrin.Utils.Unity;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using PlayerInput = Ametrin.KunstBLL.Input.PlayerInput;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviourSingleton<GameManager>{
        public static event Action OnGravityChange;
        public static event Action OnCutSceneFinished;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        [SerializeField] private Vector3 InitalGravity;
        [SerializeField] private PauseMenuController PauseMenu;
        [SerializeField] private InputAction PauseAction;
        [SerializeField] private PlayableDirector Director;
        [field: SerializeField] public Transform WorldRoot { get; private set; }

        protected override void Awake(){
            base.Awake();
            PauseMenu.Hide();
            PauseAction.performed += PauseToggle;
            Physics.gravity = InitalGravity;
            Invoke(nameof(OnCutSceneFinish), (float)Director.duration);
            // OnCutSceneFinish();
        }

        private void Start(){
            PlayerInput.Disable();
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            PauseAction.Enable();
            OnGravityChange?.Invoke();
        }

        private void OnCutSceneFinish(){
            OnCutSceneFinished.Invoke();
            PlayerInput.Enable();
            
            // Director.enabled = false;
        }

        public static void PauseToggle(InputAction.CallbackContext context = default){
            if(Time.timeScale == 0){ // sniff sniff
                Continue();
            }else{
                Pause();
            }
        }

        public static void Continue(){
            PlayerInput.Enable();
            Time.timeScale = 1;
            Instance.PauseMenu.Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public static void Pause(){
            PlayerInput.Disable();
            Time.timeScale = 0;
            Instance.PauseMenu.Show();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
