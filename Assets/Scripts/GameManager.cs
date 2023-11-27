using System;
using Ametrin.Utils.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using PlayerInput = Ametrin.KunstBLL.Input.PlayerInput;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviourSingleton<GameManager>{
        public static event Action OnGravityChange;
        public static event Action OnCutSceneFinished;
        public static event Action OnGameCompleted;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;
        public static bool EndPlaying = false;

        [SerializeField] private Vector3 InitalGravity;
        [SerializeField] private PauseMenuController PauseMenu;
        [SerializeField] private InputAction PauseAction;
        [SerializeField] private PlayableDirector IntroDirector;
        [SerializeField] private PlayableDirector OutroDirector;
        [SerializeField] private SpaceShipController SpaceShip;
        [field: SerializeField] public Transform WorldRoot { get; private set; }

        protected override void Awake(){
            base.Awake();
            EndPlaying = false;
            PauseMenu.Hide();
            PauseAction.performed += PauseToggle;
            Physics.gravity = InitalGravity;
            SpaceShip.OnEngineStarted += ShipStarted;
        }

        private void Start(){
            PlayerInput.Disable();
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            PauseAction.Enable();
            OnGravityChange?.Invoke();
            Invoke(nameof(OnCutSceneFinish), (float)IntroDirector.duration);
        }

        private void OnCutSceneFinish(){
            OnCutSceneFinished.Invoke();
            PlayerInput.Enable();    
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

        private void ShipStarted(){
            PlayerInput.Disable();
            EndPlaying = true;
            OutroDirector.Play();
            Invoke(nameof(FireEndEvent), (float) OutroDirector.duration);
        }

        private void FireEndEvent(){
            OnGameCompleted?.Invoke();
        }
    }
}
