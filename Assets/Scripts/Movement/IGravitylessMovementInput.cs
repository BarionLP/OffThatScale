using UnityEngine;

namespace Ametrin.KunstBLL.Movement
{
    public interface IGravitylessMovementInput{
        public Vector3 Acceleration { get; }
        public Quaternion Rotation { get; }
        public bool ShouldRoll { get; }
    }
}
