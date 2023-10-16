using Ametrin.SpaceZombies.Interaction;
using Ametrin.SpaceZombies.Items;
using UnityEngine;

namespace Ametrin.SpaceZombies.Entity{
    public interface IEntity : IItemUser, IInteractor{
        public Transform transform { get; }
        public HealthManager Health { get; }

        Vector3 IItemUser.Position => transform.position;
    }
}
