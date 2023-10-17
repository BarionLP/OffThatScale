using Ametrin.KunstBLL.Interaction;
using Ametrin.KunstBLL.Items;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity{
    public interface IEntity : IItemUser, IInteractor{
        public Transform transform { get; }
        public HealthManager Health { get; }

        Vector3 IItemUser.Position => transform.position;
    }
}
