using System;
using Ametrin.Utils.Unity;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class GameManager : MonoBehaviourSingleton<GameManager>{
        public static event Action OnGravityChange;
        public static bool IsZeroG => Physics.gravity == Vector3.zero;

        [SerializeField] private Vector3 InitalGravity;
        [field: SerializeField] public Transform WorldRoot { get; private set; }
        protected override void Awake(){
            Physics.gravity = InitalGravity;
            OnGravityChange?.Invoke();
        }
    }
}
