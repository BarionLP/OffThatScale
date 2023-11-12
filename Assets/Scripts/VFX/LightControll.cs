using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class LightControll : MonoBehaviour{
        [SerializeField] private Light[] Lights;

        private void Awake(){
            
        }

        public void TurnOff(){
            foreach(var light in Lights){
                light.enabled = false;
            }
        }        
        
        public void TurnOn(){
            foreach(var light in Lights){
                light.enabled = true;
            }
        }

        public void RunOut(){
            StartCoroutine(RunOutInternal());
        }

        private IEnumerator RunOutInternal(){
            const float flickerSpeed = 0.2f;
            var timer = 4f;
            var startIntensities = Lights.Select(l => l.intensity).ToArray();
            var intensities = new float[startIntensities.Length];
            Array.Copy(startIntensities, intensities, startIntensities.Length);
            
            while(timer > 0){
                timer -= Time.deltaTime;
                // feat. ChatGPT
                var noise = Mathf.PerlinNoise(UnityEngine.Random.Range(-1, 1), Time.time * flickerSpeed);
                for(int i = 0; i < Lights.Length; i++){
                    intensities[i] -= Time.deltaTime*0.25f;
                    Lights[i].intensity = Mathf.Lerp(0, intensities[i], noise);
                }

                yield return new WaitForEndOfFrame();
            }

            TurnOff();
            yield return null;
        }
    }
}