using Ametrin.KunstBLL.Interaction;
using Ametrin.KunstBLL.Items;
using UnityEngine.Playables;

namespace Ametrin.KunstBLL.Timeline{
    // sniff sniff sniff
    public sealed class InteractionPlayable : PlayableBehaviour{
        private bool done = false;

        public override void OnBehaviourPlay(Playable playable, FrameData info)        {
            done = false;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData){
            if(done) return;
            (playerData as IInteractable).Interact(new DummyInteractor());
            done = true;
        }

        private sealed class DummyInteractor : IInteractor{
            public void PickUp(ItemStack item) {}
        }
    }
}
