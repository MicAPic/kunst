using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // [ExecuteInEditMode]
    [RequireComponent(typeof(RawImage))]
    public class BackgroundController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] 
        private RectTransform rectTransform;
        [SerializeField] 
        private RectTransform parentRectTransform;
        [SerializeField] 
        private RawImage image;
        [Header("Settings")]
        [SerializeField] 
        private Vector2 repeatCount;
        [SerializeField] 
        private Vector2 scroll;
        [SerializeField] 
        private Vector2 offset;

        private void Awake()
        {
            if (!image) image = GetComponent<RawImage>();

            image.uvRect = new Rect(offset, repeatCount);
        }

        // Start is called before the first frame update
        private void Start()
        {
            if (!rectTransform) rectTransform = GetComponent<RectTransform>();
            if (!parentRectTransform) parentRectTransform = GetComponentInParent<RectTransform>();

            SetScale();
        }

        // Update is called once per frame
        private void Update()
        {
#if UNITY_EDITOR
            // Only done in the Unity editor since later it is unlikely that your screensize changes
            SetScale();
#endif
            offset += scroll * Time.deltaTime;
            image.uvRect = new Rect(offset, repeatCount);
        }

        private void SetScale()
        {
            // get the diagonal size of the screen since the parent is the Canvas with
            // ScreenSpace overlay it is always fiting the screensize
            var parentCorners = new Vector3[4];
            parentRectTransform.GetLocalCorners(parentCorners);
            var diagonal = Vector3.Distance(parentCorners[0], parentCorners[2]);

            // set width and height to at least the diagonal
            rectTransform.sizeDelta = new Vector2(diagonal, diagonal);
        }
    }
}