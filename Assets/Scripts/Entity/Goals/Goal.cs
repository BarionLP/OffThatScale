using Ametrin.KunstBLL.Entity.Controller;
using UnityEngine;

namespace Ametrin.KunstBLL.Entity.Goals{
    public abstract class Goal : MonoBehaviour, IGoal{
        protected abstract bool ShouldTick(EntityController entity);
        public abstract void Tick(EntityController entity);
        bool IGoal.ShouldTick(EntityController entity) => enabled && ShouldTick(entity);
    }

    public interface IGoal{
        public bool ShouldTick(EntityController entity);
        public void Tick(EntityController entity);
    }
}
