using DG.Tweening;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] 
        private float fadeInDuration;
        [SerializeField] 
        private string exposedVolumeName;
        
        private float _maxVolume;
        
        void Awake()
        {
            _maxVolume = PlayerPrefs.GetFloat(exposedVolumeName, Mathf.Log10(0.994f) * 20);
        }

        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.audioMixer.SetFloat(exposedVolumeName, Mathf.Log10(0.0001f) * 20);
            FadeIn(fadeInDuration);
        }

        public void FadeOut(float duration)
        {
            AudioManager.Instance.audioMixer.DOSetFloat(
                exposedVolumeName,
                Mathf.Log10(0.0001f) * 20,
                duration
            ).SetUpdate(true);
        }

        public void FadeIn(float duration)
        {
            AudioManager.Instance.audioMixer.DOSetFloat(
                exposedVolumeName,
                _maxVolume,
                duration
            );
        }
    }
}
