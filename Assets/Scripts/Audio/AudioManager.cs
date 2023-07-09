using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public AudioMixer audioMixer;

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;
        
        [Header("Audio PLayers")]
        private AudioPlayer _musicPlayer;
        private AudioPlayer _sfxPlayer;
        
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            musicSource = transform.GetChild(0).GetComponent<AudioSource>();
            _musicPlayer = transform.GetChild(0).GetComponent<AudioPlayer>();
            _musicPlayer.FadeIn(_musicPlayer.fadeInDuration);
            
            sfxSource = transform.GetChild(1).GetComponent<AudioSource>();
            _sfxPlayer = transform.GetChild(1).GetComponent<AudioPlayer>();
            _sfxPlayer.FadeIn(_sfxPlayer.fadeInDuration);
        }

        public void FadeOutAll(float transitionDuration)
        {
            _musicPlayer.FadeOut(transitionDuration);
            _sfxPlayer.FadeOut(transitionDuration);
        }
    }
}
