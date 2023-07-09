using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public float fadeInDuration;
        [SerializeField] 
        private string exposedVolumeName;
        [SerializeField]
        private int maxVolumeIndex;

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
                    SettingsManager.Instance.maxVolumes[maxVolumeIndex]
                )
            );
        }
        
        public void SetVolume(float volume)
        {
            SettingsManager.Instance.maxVolumes[maxVolumeIndex] = volume;
            AudioManager.Instance.audioMixer.SetFloat(exposedVolumeName, Mathf.Log10(volume) * 20);
        }
    }
}
