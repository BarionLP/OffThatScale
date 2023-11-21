using Ametrin.KunstBLL.Entity;
using Ametrin.KunstBLL.Interaction;
using UnityEngine;
using UnityEngine.Timeline;

namespace Ametrin.KunstBLL.Timeline{
    [TrackBindingType(typeof(OpenableInteraction))]
    [TrackClipType(typeof(InteractionClip))]
    public sealed class InteractionTrack : TrackAsset{
        // [SerializeField] private EntityManager Interactor;
    }
}
