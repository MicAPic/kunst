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
        
        [SerializeField]
        private Color nextLevelColor;
        [SerializeField]
        private RectTransform[] paintBrushes;
        [SerializeField] 
        private Vector2[] paintBrushStartLocations;
        [SerializeField] 
        private Vector2[] paintBrushEndLocations;
        [SerializeField]
        private Image brush1;
        [SerializeField]
        private Image brush2;
        
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
            
            if (paintBrushStartLocations.Length != paintBrushEndLocations.Length)
            {
                Debug.LogWarning("You done goofed! Arrays mismatch");
            }
            
            for (var i = 0; i < paintBrushes.Length; i++)
            {
                var paintBrush = paintBrushes[i];
                paintBrush.anchoredPosition = paintBrushStartLocations[i];
                paintBrush.DOAnchorPos(paintBrushEndLocations[i], transitionTime);
            }
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
            
            brush1.color = nextLevelColor;
            brush2.color = nextLevelColor;
            
            var asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            asyncOperation.allowSceneActivation = false;

            var paintBrush = paintBrushes[0];
            paintBrush.GetComponent<Shadow>().enabled = false;
            paintBrush.anchoredPosition = paintBrushEndLocations[0];
            paintBrush.DOAnchorPos(paintBrushStartLocations[0], transitionTime).SetUpdate(true);
            
            paintBrushes[^1].DOAnchorPos(paintBrushStartLocations[^1], transitionTime)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    Time.timeScale = 1.0f;
                    asyncOperation.allowSceneActivation = true;
                });
        }

        public void ToggleSettings()
        {
            var currentPos = settingsBackground.anchoredPosition;
            if (Vector2.Distance(currentPos, _settingsDefaultPos) < Vector2.Distance(currentPos, settingsOnPos))
            {
                settingsBackground.DOAnchorPos(settingsOnPos, transitionTime);
                settingsMenuGroup.interactable = true;
                settingsMenuGroup.blocksRaycasts = true;
                settingsMenuGroup.DOFade(1f, transitionTime / 2).SetDelay(transitionTime / 2);
            }
            else
            {
                settingsBackground.DOAnchorPos(_settingsDefaultPos, transitionTime);
                settingsMenuGroup.interactable = false;
                settingsMenuGroup.blocksRaycasts = false;
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
