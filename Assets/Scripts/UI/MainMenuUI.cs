using Audio;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public static MainMenuUI Instance;
        
        [Header("Cursor")] 
        [SerializeField] 
        private Texture2D defaultCursor;

        [Header("Settings")] 
        [SerializeField] 
        private RectTransform settingsBackground;
        [SerializeField] 
        private CanvasGroup settingsMenuGroup;
        [SerializeField] 
        private Slider musicSlider;
        [SerializeField] 
        private Slider sfxSlider;
        [Space]
        [SerializeField] 
        private float transitionTime;
        
        [SerializeField] 
        private Vector2 settingsOnPos;
        private Vector2 _settingsDefaultPos;
        

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
            
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
            _settingsDefaultPos = settingsBackground.anchoredPosition;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.994f);
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.994f);
        }
        
        // Update is called once per frame
        // void Update()
        // {
        //     
        // }

        public void LoadScene(string sceneToLoad)
        {
            SceneManager.MoveGameObjectToScene(AudioManager.Instance.gameObject, SceneManager.GetActiveScene());
            AudioManager.Instance.FadeOutAll(1f);
            
            SceneManager.LoadScene(sceneToLoad);
        }

        public void ToggleSettings()
        {
            var currentPos = settingsBackground.anchoredPosition;
            if (Vector2.Distance(currentPos, _settingsDefaultPos) < Vector2.Distance(currentPos, settingsOnPos))
            {
                settingsBackground.DOAnchorPos(settingsOnPos, transitionTime);
                settingsMenuGroup.DOFade(1f, transitionTime / 2).SetDelay(transitionTime / 2);
            }
            else
            {
                settingsBackground.DOAnchorPos(_settingsDefaultPos, transitionTime);
                settingsMenuGroup.DOFade(0f, transitionTime / 2);
            }
        }
        
        public void Quit()
        {
            #if (UNITY_EDITOR)
            EditorApplication.ExitPlaymode();
            #elif (UNITY_STANDALONE) 
            Application.Quit();
            #elif (UNITY_WEBGL)
            Application.OpenURL("https://micapic.itch.io/kunst");
            #endif
        }
    }
}
