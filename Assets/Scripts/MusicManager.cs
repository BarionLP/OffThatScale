using UnityEngine;

namespace Ametrin.KunstBLL{
    public sealed class MusicManager : MonoBehaviour{
        private AudioSource MusicSource;
        
        private void Awake(){
            MusicSource = GetComponent<AudioSource>();
        }

        private void Start(){
            MusicSource.Play();
        }
    }
}
