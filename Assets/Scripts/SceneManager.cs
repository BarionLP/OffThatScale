using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public static class SceneManager{
        private const int TRANSITION_SCENE = 1;
        public static void LoadScene(Scene scene){
            UnityEngine.SceneManagement.SceneManager.LoadScene((int) scene);
            // UnityEngine.SceneManagement.SceneManager.LoadScene(TRANSITION_SCENE);
            // Thread.Sleep(TimeSpan.FromSeconds(1));
            _ = scene switch{
                Scene.Start => Cursor.lockState = CursorLockMode.None,
                Scene.Game => Cursor.lockState = CursorLockMode.Locked,
                _ => Cursor.lockState = CursorLockMode.None,
                // _ => throw new ArgumentException("Tried loading unknown scene", nameof(scene)),
            };
        }

        public static async void LoadScene(Scene scene, float delay){
            await Task.Delay(TimeSpan.FromSeconds(delay));
            LoadScene(scene);
        }
    }

    public enum Scene{
        Start = 0,
        Game = 2,
    }
}
