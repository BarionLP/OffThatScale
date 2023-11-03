using UnityEngine;

namespace Ametrin.KunstBLL.VFX{
    public sealed class LightFlickering : MonoBehaviour{
        [SerializeField] private float minIntensity = 0.5f;
        [SerializeField] private float maxIntensity = 1f;
        [SerializeField] private float flickerSpeed = 0.2f;

        private Light lightSource;

        private void Start(){
            lightSource = GetComponent<Light>();
        }

        private void Update(){
            // feat. ChatGPT
            var noise = Mathf.PerlinNoise(Random.Range(-1, 1), Time.time * flickerSpeed);
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }

        private void OnDisable(){
            lightSource.intensity = maxIntensity;
        }
    }
}
