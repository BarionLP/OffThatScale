using System;
using Ametrin.KunstBLL.Input;
using Ametrin.Utils.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Ametrin.KunstBLL.Input.PlayerInput;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviourSingleton<GameManager>{
        public static event Action OnGravityChange;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        [SerializeField] private Vector3 InitalGravity;
        [SerializeField] private PauseMenuController PauseMenu;
        [SerializeField] private InputAction PauseAction;
        [field: SerializeField] public Transform WorldRoot { get; private set; }

        protected override void Awake(){
            Physics.gravity = InitalGravity;
            OnGravityChange?.Invoke();
            PauseAction.performed += PauseToggle;
        }

        private void Start(){
            PauseAction.Enable();
            Continue();
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
