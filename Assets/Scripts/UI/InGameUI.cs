using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        public static InGameUI Instance;

        [Header("Transition")] 
        [SerializeField]
        private float transitionDuration = 1.0f;
        [SerializeField] 
        private RectTransform[] paintBrushes;
        [SerializeField] 
        private Vector2[] paintBrushStartLocations;
        [SerializeField] 
        private Vector2[] paintBrushEndLocations;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            if (paintBrushStartLocations.Length != paintBrushEndLocations.Length)
            {
                Debug.LogWarning("You done goofed! Arrays mismatch");
            }
            
            for (var i = 0; i < paintBrushes.Length; i++)
            {
                var paintBrush = paintBrushes[i];
                paintBrush.anchoredPosition = paintBrushStartLocations[i];
                paintBrush.DOAnchorPos(paintBrushEndLocations[i], transitionDuration);
            }
        }
        
        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        public void AnimateTransition(string sceneToLoad)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            asyncOperation.allowSceneActivation = false;
            
            AudioManager.Instance.FadeOutAll(transitionDuration);
            
            RectTransform paintBrush;
            for (var i = 0; i < paintBrushes.Length - 1; i++)
            {
                paintBrush = paintBrushes[i];
                paintBrush.DOAnchorPos(paintBrushStartLocations[i], transitionDuration);
            }
            
            paintBrush = paintBrushes[^1];
            paintBrush.DOAnchorPos(paintBrushStartLocations[^1], transitionDuration)
                .OnComplete(() => asyncOperation.allowSceneActivation = true);
        }
    }
}
