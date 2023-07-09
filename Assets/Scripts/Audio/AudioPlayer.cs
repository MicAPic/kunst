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
            PlayerPrefs.SetFloat(exposedVolumeName, Mathf.Log10(0.994f) * 20);
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
            StartCoroutine(FadeMixerGroup.StartFade
                (
                    AudioManager.Instance.audioMixer, 
                    exposedVolumeName, 
                    duration, 
                    0.0001f
                )
            );
        }

        public void FadeIn(float duration)
        {
            StartCoroutine(FadeMixerGroup.StartFade
                (
                    AudioManager.Instance.audioMixer, 
                    exposedVolumeName, 
                    duration, 
                    _maxVolume
                )
            );
        }
        
        public void SetVolume(float volume)
        {
            PlayerPrefs.SetFloat(exposedVolumeName, volume);
            AudioManager.Instance.audioMixer.SetFloat(exposedVolumeName, Mathf.Log10(volume) * 20);
        }
    }
}
