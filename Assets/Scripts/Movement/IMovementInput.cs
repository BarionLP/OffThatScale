using UnityEngine;

namespace Ametrin.KunstBLL.Movement{
    public interface IMovementInput{
        public bool IsMoving { get; }
        public Vector2 Move { get; }
        public bool IsSprinting { get; }
        public bool ShouldJump { get; }
        public Quaternion Rotation { get; }
        public Vector3 UpVector { get; }
    }
}