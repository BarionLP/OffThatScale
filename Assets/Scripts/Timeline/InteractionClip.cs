using Ametrin.KunstBLL.Interaction;
using Ametrin.KunstBLL;
using UnityEngine;
using UnityEngine.Playables;

namespace Ametrin.KunstBLL.Timeline{
    public sealed class InteractionClip : PlayableAsset{
        // [SerializeField] private OpenableInteraction Interactable;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner){
            var playable = ScriptPlayable<InteractionPlayable>.Create(graph);
            // var behavior = playable.GetBehaviour();
            // behavior.Interactable = Interactable;

            // Interactable.TryGetComponent<IInteractable>().Resolve(interactable => behavior.Interactable = interactable);
            return playable;
        }
    }
}
