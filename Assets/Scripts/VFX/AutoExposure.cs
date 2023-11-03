using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Ametrin.KunstBLL.VFX{
    public sealed class AutoExposure : MonoBehaviour{
        [SerializeField] private Volume GlobalVolume;

        private ColorAdjustments adjustments;
        private void Start(){
            adjustments = GlobalVolume.profile.components.Where(c => c is ColorAdjustments).First() as ColorAdjustments;
        }

        private void Update(){
            adjustments.postExposure.value = 2;
        }
    }
}
