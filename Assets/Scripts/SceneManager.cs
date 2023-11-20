using System;
using System.Threading;

namespace Ametrin.KunstBLL{
    public static class SceneManager{
        private const int TRANSITION_SCENE = 1;
        public static void LoadScene(Scene scene){
            UnityEngine.SceneManagement.SceneManager.LoadScene((int) scene);
            // UnityEngine.SceneManagement.SceneManager.LoadScene(TRANSITION_SCENE);
            // Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }

    public enum Scene{
        Start = 0,
        Game = 2,
    }
}
